using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Net;
using System.Threading.Tasks;

namespace Tests.Infrastructure
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public ErrorHandlingMiddleware(RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode;
            object? errors = default;

            if (exception is ErrorException re)
            {
                statusCode = (int)re.Code;

                if (re.Message is string)
                {
                    errors = new[] { re.Message };
                }

                if (re.Message is IEnumerable)
                {
                    errors = re.Message;
                }
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;

                if (_environment.IsDevelopment())
                {
                    errors = exception.Message;
                } 
                else
                {
                    errors = "An internal server error has occurred.";
                }
            }

            _logger.LogError($"{errors} - {exception.Source} - {exception.Message} - {exception.StackTrace} - {exception.TargetSite?.Name}");

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                errors
            }));
        }
    }
}