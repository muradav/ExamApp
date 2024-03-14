using log4net;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace Exam.Business.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILog _logger;

        public ExceptionHandlerMiddleware(RequestDelegate Next, ILog logger)
        {
            next = Next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = MediaTypeNames.Application.Json;

                var errorMessage = $"Error Message: {ex.Message}";

                if (errorMessage != null)
                {
                    var json = JsonSerializer.Serialize(new
                    {
                        Title = ex.GetType().ToString(),
                        StatusCode = context.Response.StatusCode,
                        Message = errorMessage,
                    });

                    _logger.Error(errorMessage);

                    await context.Response.WriteAsync(json);
                }
            }
        }
    }
}
