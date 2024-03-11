using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Exam.Business.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(RequestDelegate Next)
        {
            next = Next;
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

                    await context.Response.WriteAsync(json);
                }
            }
        }
    }
}
