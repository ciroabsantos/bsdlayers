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
        public object Clone()
        {
            return new ServiceResponse
            (
                statusCode: StatusCode,
                messages: new List<string>(Messages)
            );
        }
    }
}