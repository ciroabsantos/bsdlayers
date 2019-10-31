using System;
using System.Collections.Generic;

namespace BsdLayers.Services.Model
{
    /// <summary>
    /// Default response message for services
    /// </summary>
    public class ServiceResponse : ICloneable
    {
        /// <summary>
        /// Service execution status code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Service execution messages
        /// </summary>
        public List<string> Messages { get; private set; }

        /// <summary>
        /// Full constructor
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="messages"></param>
        public ServiceResponse(int statusCode, IEnumerable<string> messages)
        {
            StatusCode = statusCode;
            Messages = new List<string>(messages ?? throw new ArgumentNullException(nameof(messages)));
        }

        /// <summary>
        /// Constructor with single message
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        public ServiceResponse(int statusCode, string message) : this(statusCode: statusCode, messages: new List<string>() { message })
        {
        }

        /// <summary>
        /// Constructor with empty messages
        /// </summary>
        /// <param name="statusCode"></param>
        public ServiceResponse(int statusCode) : this(statusCode: statusCode, messages: new List<string>())
        {
        }

        /// <summary>
        /// Object's deep clone
        /// </summary>
        /// <returns></returns>
        public virtual object Clone()
        {
            return new ServiceResponse
            (
                statusCode: StatusCode,
                messages: new List<string>(Messages)
            );
        }
    }

    /// <summary>
    /// Default response message with specific content 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResponse<T> : ServiceResponse
    {
        /// <summary>
        /// Service response specific content
        /// </summary>
        public T Content { get; set; }

        /// <summary>
        /// Full constructor
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="messages"></param>
        /// <param name="content"></param>
        public ServiceResponse(int statusCode, IEnumerable<string> messages, T content)
            :
            base(statusCode: statusCode, messages: messages)
        {
            Content = content;
        }

        /// <summary>
        /// Constructor with single message
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <param name="content"></param>
        public ServiceResponse(int statusCode, string message, T content)
            :
            this(statusCode: statusCode, messages: new string[] { message }, content: content)
        {
        }

        /// <summary>
        /// Constructor with empty message
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="content"></param>
        public ServiceResponse(int statusCode, T content)
            :
            this(statusCode: statusCode, messages: new string[0], content: content)
        {
        }

        /// <summary>
        /// Object's deep clone
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new ServiceResponse<T>
            (
                statusCode: StatusCode,
                messages: new List<string>(Messages),
                content: Content is ICloneable ? (T)((ICloneable)Content)?.Clone() : Content
            );
        }
    }
}