using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Gym.Tracker.Common.Extensions;
using Gym.Tracker.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Gym.Tracker.Common.Helper
{
    public class ErrorHandler
    {
        /// <summary>
        /// Request delegate
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Logger for errorhandler
        /// </summary>
        private readonly ILogger<ErrorHandler> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ErrorHandler(RequestDelegate next, ILogger<ErrorHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invoke  
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                await HandleException(context, error);
            }
        }

        /// <summary>
        /// Handle exception
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private Task HandleException(HttpContext context, Exception ex)
        {
            ErrorResultModel jsonresult = new ErrorResultModel();
            jsonresult.Result = false;
            if (ex != null)
            {
                jsonresult.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            jsonresult.ErrorCode = ex?.HResult;
            var errorMessage = JsonConvert.SerializeObject(jsonresult);
            if (ex != null)
                _logger.LogError(new Exception(), ex.StackTrace ?? ex.Message);
            else
                _logger.LogError(new Exception(), "Exception Throw");
            context.Response.ContentType = "application/json";
            if (typeof(ValidationException) == ex.GetType() || typeof(System.ComponentModel.DataAnnotations.ValidationException) == ex.GetType())
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            }
            else if (typeof(UnauthorizedAccessException) == ex.GetType())
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else if (typeof(AccessDeniedException) == ex.GetType())
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
            else
            {
                context.Response.StatusCode = jsonresult.Message == "Unauthorized" ? (int)HttpStatusCode.NotFound : (int)HttpStatusCode.InternalServerError;
            }
            return context.Response.WriteAsync(errorMessage);
        }
    }
}
