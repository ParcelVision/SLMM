using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SLMM.Api.Middleware
{
    public class ProblemDetailsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ProblemDetailsMiddleware> _logger;

        public ProblemDetailsMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory?.CreateLogger<ProblemDetailsMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = (int?) HttpStatusCode.InternalServerError, Title = "SLMM Api Error", Detail = ex.Message,
                };


                switch (ex)
                {
                    case InvalidOperationException _:
                        problemDetails.Status = (int?) HttpStatusCode.BadRequest;
                        break;
                    case ArgumentOutOfRangeException _:
                        problemDetails.Status = (int?) HttpStatusCode.BadRequest;
                        break;
                    case TaskCanceledException _:
                        problemDetails.Status = (int?) HttpStatusCode.Conflict;
                        break;
                }

                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started. Skipping error middleware.");
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = (int) problemDetails.Status;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
            }
        }
    }

    public static class ProblemDetailsMiddlewareExtensions
    {
        public static IApplicationBuilder UseProblemDetailsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ProblemDetailsMiddleware>();
        }
    }
}