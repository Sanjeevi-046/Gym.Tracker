using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Tracker.Common.Extensions
{
    /// <summary>
    /// Not Allowed exception
    /// </summary>
    public class AccessDeniedException : Exception
    {
        /// <summary>
        /// Custom exception
        /// </summary>
        public AccessDeniedException() : base() { }

        /// <summary>
        /// Custom exception  with custom message
        /// </summary>
        /// <param name="message"></param>
        public AccessDeniedException(string message) : base(message) { }

    }
}
