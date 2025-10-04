using ParkingLotAPP.Application.Exceptions;
using ParkingLotAPP.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;

namespace ParkingLotAPP.API.Filters
{
    public class APIGlobalExceptionFilter : IAsyncExceptionFilter
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ILogger<APIGlobalExceptionFilter> _logger;
        private readonly ProblemDetailsFactory _problemDetailsFactory;

        public APIGlobalExceptionFilter(IHostEnvironment hostEnvironment, ILogger<APIGlobalExceptionFilter> logger, ProblemDetailsFactory problemDetailsFactory)
        {
            _hostEnvironment = hostEnvironment;
            _logger = logger;
            _problemDetailsFactory = problemDetailsFactory;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            var ex = context.Exception;
            var httpContext = context.HttpContext;

            var (status, title, typeUri) = ex switch
            {
                EntityValidationException => ((int)HttpStatusCode.UnprocessableEntity,
                                              "Validation failed",
                                              "https://api.seudominio.com/errors/unprocessable-entity"),
                NotFoundException => ((int)HttpStatusCode.NotFound,
                                      "Resource not found",
                                      "https://api.seudominio.com/errors/not-found"),
                RelatedEntityException => ((int)HttpStatusCode.UnprocessableEntity,
                                              "Invalid related aggregate",
                                              "https://api.seudominio.com/errors/conflict"),
                _ => ((int)HttpStatusCode.InternalServerError,
                      "An unexpected error occurred",
                      "https://api.seudominio.com/errors/internal-server-error")
            };

            var detail =
            status == (int)HttpStatusCode.InternalServerError && !_hostEnvironment.IsDevelopment()
                ? "Unexpected server error. Please contact support if the problem persists."
                : ex.Message;

            var problem = _problemDetailsFactory.CreateProblemDetails(
                httpContext,
                statusCode: status,
                title: title,
                type: typeUri,
                detail: detail,
                instance: httpContext.Request.Path
            );

            problem.Extensions["traceId"] = httpContext.TraceIdentifier;


            if (_hostEnvironment.IsDevelopment())
            {
                // ToString() inclui stack + inner exceptions
                problem.Extensions["stackTrace"] = ex.ToString();
            }


            _logger.LogError(ex, "Unhandled exception. TraceId: {TraceId}", httpContext.TraceIdentifier);

            // Resultado
            context.Result = new ObjectResult(problem) { StatusCode = problem.Status };
            context.ExceptionHandled = true;

            return Task.CompletedTask;
        }
    }
}
