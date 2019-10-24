using System;
using System.Collections.Generic;
using System.Linq;

namespace BsdLayers.Business.Specs
{
    /// <summary>
    /// Validation Result from BusinessSpecification
    /// </summary>
    public class BusinessSpecResult : ICloneable
    {
        /// <summary>
        /// Indicates if specification was fulfilled
        /// </summary>
        public bool IsSatisfied { get; private set; }

        /// <summary>
        /// Business result status caused by spec validation result
        /// </summary>
        public BusinessResultStatus Status { get; private set; }

        /// <summary>
        /// Messages produced by spec validation
        /// </summary>
        public IEnumerable<BusinessSpecMessage> Messages { get; private set; }

        /// <summary>
        /// Full constructor
        /// </summary>
        /// <param name="isSatisfied"></param>
        /// <param name="status"></param>
        /// <param name="messages"></param>
        public BusinessSpecResult(bool isSatisfied, BusinessResultStatus status, IEnumerable<BusinessSpecMessage> messages)
        {
            IsSatisfied = isSatisfied;
            Status = status;
            Messages = messages.ToList();
        }

        /// <summary>
        /// Constructor with empty messages
        /// </summary>
        /// <param name="isSatisfied"></param>
        /// <param name="status"></param>
        public BusinessSpecResult(bool isSatisfied, BusinessResultStatus status) : this(isSatisfied, status, new BusinessSpecMessage[0])
        {
        }

        /// <summary>
        /// Constructor with status resolved from messages
        /// </summary>
        /// <param name="isSatisfied"></param>
        /// <param name="messages"></param>
        public BusinessSpecResult(bool isSatisfied, IEnumerable<BusinessSpecMessage> messages)
        {
            IsSatisfied = isSatisfied;
            Messages = messages.ToList();

            if (isSatisfied)
            {
                Status = BusinessResultStatus.Success;
                return;
            }

            Status = Messages
                .Where(m => m.Status != BusinessResultStatus.Success)
                .OrderBy(m => (int)m.Status)
                .Select(m => m.Status)
                .First();
        }

        /// <summary>
        /// Object deep clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new BusinessSpecResult
            (
                isSatisfied: IsSatisfied,
                status: Status,
                messages: Messages.Select(m => (BusinessSpecMessage)m.Clone())
            );
        }
    }
}
