using System;
using System.Linq;

namespace BsdLayers.Business.Specs
{
    /// <summary>
    /// Creates a composed spec with AND logic
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AndBusinessSpec<T> : IBusinessSpec<T>
    {
        private readonly IBusinessSpec<T> _first;
        private readonly IBusinessSpec<T> _second;

        public AndBusinessSpec(IBusinessSpec<T> first, IBusinessSpec<T> second)
        {
            _first = first ?? throw new ArgumentNullException(nameof(first));
            _second = second ?? throw new ArgumentNullException(nameof(second));
        }

        public AndBusinessSpec<T> Push(IBusinessSpec<T> spec)
        {
            return new AndBusinessSpec<T>(this, spec);
        }

        public BusinessSpecResult IsSatisfiedBy(T bo)
        {
            if (bo == null)
            {
                throw new ArgumentNullException(nameof(bo));
            }

            var firstResult = _first.IsSatisfiedBy(bo);

            if (firstResult.IsSatisfied == false)
            {
                return firstResult;
            }

            var secondResult = _second.IsSatisfiedBy(bo);
            var messages = firstResult.Messages.Concat(secondResult.Messages);

            return new BusinessSpecResult
            (
                isSatisfied: secondResult.IsSatisfied,
                status: secondResult.Status,
                messages: messages
            );
        }
    }
}
