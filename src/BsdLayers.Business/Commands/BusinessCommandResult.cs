using System;
using System.Linq;
using System.Collections.Generic;

namespace BsdLayers.Business.Commands
{
    /// <summary>
    /// Represents a command execution result 
    /// </summary>
    public class BusinessCommandResult : ICloneable
    {
        /// <summary>
        /// Result identification
        /// </summary>
        public Guid ResultId { get; private set; }

        /// <summary>
        /// Result status
        /// </summary>
        public BusinessResultStatus Status { get; private set; }

        /// <summary>
        /// Result message list
        /// </summary>
        public List<string> Messages { get; private set; }

        /// <summary>
        /// Full constructor
        /// </summary>
        /// <param name="resultId"></param>
        /// <param name="status"></param>
        /// <param name="messages"></param>
        public BusinessCommandResult(Guid resultId, BusinessResultStatus status, IEnumerable<string> messages)
        {
            ResultId = resultId;
            Status = status;
            Messages = messages.ToList();
        }

        /// <summary>
        /// Constructor with default resultId
        /// </summary>
        /// <param name="status"></param>
        /// <param name="messages"></param>
        public BusinessCommandResult(BusinessResultStatus status, IEnumerable<string> messages) : this(Guid.NewGuid(), status, messages)
        {
        }

        /// <summary>
        /// Constructor for results with default ResultId and single message
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        public BusinessCommandResult(BusinessResultStatus status, string message) : this(Guid.NewGuid(), status, new string[] { message })
        {
        }

        /// <summary>
        /// Constructor with default ResultId and empty message list
        /// </summary>
        /// <param name="status"></param>
        public BusinessCommandResult(BusinessResultStatus status) : this(Guid.NewGuid(), status, new string[0])
        {
        }

        /// <summary>
        /// Object deep clone
        /// </summary>
        /// <returns></returns>
        public virtual object Clone()
        {
            return new BusinessCommandResult
            (
                resultId: ResultId,
                status: Status,
                messages: Messages
            );
        }
    }

    /// <summary>
    /// Represents a command execution result that returns a content
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BusinessCommandResult<T> : BusinessCommandResult
    {
        /// <summary>
        /// Content returned by result
        /// </summary>
        public T Content { get; private set; }

        /// <summary>
        /// Full constructor
        /// </summary>
        /// <param name="resultId"></param>
        /// <param name="status"></param>
        /// <param name="messages"></param>
        /// <param name="content"></param>
        public BusinessCommandResult(Guid resultId, BusinessResultStatus status, IEnumerable<string> messages, T content) : base(resultId, status, messages)
        {
            Content = content;
        }

        /// <summary>
        /// Constructor with default resultId
        /// </summary>
        /// <param name="status"></param>
        /// <param name="messages"></param>
        /// <param name="content"></param>
        public BusinessCommandResult(BusinessResultStatus status, IEnumerable<string> messages, T content) : this(Guid.NewGuid(), status, messages, content)
        {
        }

        /// <summary>
        /// Constructor for results with default ResultId and single message
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="content"></param>
        public BusinessCommandResult(BusinessResultStatus status, string message, T content) : this(Guid.NewGuid(), status, new string[] { message }, content)
        {
        }

        /// <summary>
        /// Constructor with default ResultId and empty message list
        /// </summary>
        /// <param name="status"></param>
        /// <param name="content"></param>
        public BusinessCommandResult(BusinessResultStatus status, T content) : this(Guid.NewGuid(), status, new string[0], content)
        {
        }

        /// <summary>
        /// Object deep clone
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new BusinessCommandResult<T>
            (
                resultId: ResultId,
                status: Status,

                messages: Messages,
                content: Content is ICloneable ? (T)((ICloneable)Content)?.Clone() : Content
            );
        }
    }
}
