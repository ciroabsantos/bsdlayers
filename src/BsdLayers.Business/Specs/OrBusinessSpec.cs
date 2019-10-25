using System;
using System.Linq;
using System.Threading.Tasks;

namespace BsdLayers.Business.Specs
{
    /// <summary>
    /// Creates composed spec wirh OR logic
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OrBusinessSpec<T> : IBusinessSpec<T>
    {
        private readonly IBusinessSpec<T> _first;
        private readonly IBusinessSpec<T> _second;

        public OrBusinessSpec(IBusinessSpec<T> first, IBusinessSpec<T> second)
        {
            _first = first ?? throw new ArgumentNullException(nameof(first));
            _second = second ?? throw new ArgumentNullException(nameof(second));
        }

        public OrBusinessSpec<T> Push(IBusinessSpec<T> spec)
        {
            return new OrBusinessSpec<T>(this, spec);
        }

        public BusinessSpecResult IsSatisfiedBy(T bo)
        {
            if (bo == null)
            {
                throw new ArgumentNullException(nameof(bo));
            }

            var taskFactory = new TaskFactory();
            var firstSpecTask = taskFactory.StartNew(() => { return _first.IsSatisfiedBy(bo); });

            var secondSpecTask = taskFactory.StartNew(() => { return _second.IsSatisfiedBy(bo); });
            var firsResult = firstSpecTask.Result;

            var secondResult = secondSpecTask.Result;
            var statusList = new BusinessResultStatus[] { firsResult.Status, secondResult.Status };

            var isSatisfied = firsResult.IsSatisfied || secondResult.IsSatisfied;
            var status = BusinessResultStatus.Success;

            if (isSatisfied)
            {
                status = statusList
                    .Where(s => s < BusinessResultStatus.InvalidInputs)
                    .OrderBy(s => s)
                    .First();
            }
            else
            {
                status = statusList
                    .Where(s => s >= BusinessResultStatus.InvalidInputs)
                    .OrderBy(s => s)
                    .First();
            }

            return new BusinessSpecResult
            (
                isSatisfied: firsResult.IsSatisfied || secondResult.IsSatisfied,
                status: status,
                messages: firsResult.Messages.Concat(secondResult.Messages)
            );
        }
    }
}
