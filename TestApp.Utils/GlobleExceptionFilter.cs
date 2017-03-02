using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace TestApp.Utils
{
    public class GlobleExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public GlobleExceptionFilter(ILoggerFactory logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            _logger = logger.CreateLogger("GlobleExceptionFilter");
        }

        public void OnException(ExceptionContext context)
        {
            var response = GetResponse(context);

            context.Result = new ObjectResult(response)
            {
                StatusCode = 500,
                DeclaredType = typeof(ErrorResponse)
            };

            _logger.LogError($"GlobleExceptionFilter : {context.Exception}");
        }

        private ErrorResponse GetResponse(ExceptionContext context)
        {
            return new ErrorResponse()
            {
                Message = context.Exception.Message,
                StackTrace = context.Exception.StackTrace
            };
        }
    }

    public class ErrorResponse
    {
        public ErrorResponse()
        {
        }

        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
