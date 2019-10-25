using BsdLayers.Business.Commands;
using System;

namespace BsdLayers.Business.Specs
{
    /// <summary>
    /// Validation message from BusinessSpecification
    /// </summary>
    public class BusinessSpecMessage : ICloneable
    {
        /// <summary>
        /// Result Status caused by the message
        /// </summary>
        public BusinessResultStatus Status { get; private set; }

        /// <summary>
        /// Message description
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Full constructor
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        public BusinessSpecMessage(BusinessResultStatus status, string message)
        {
            Status = status;
            Message = message;
        }

        /// <summary>
        /// Object deep clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new BusinessSpecMessage
            (
                status: Status,
                message: Message
            );
        }
    }
}
