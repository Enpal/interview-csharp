using UrlShortenerService.Application.Common.Exceptions;

namespace UrlShortenerService.Api.Middlewares;

/// <summary>
/// Middleware to handle exceptions that occur while processing http requests.
/// </summary>
public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly IDictionary<Type, Action<HttpContext, Exception>> _exceptionHandlers;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="env">Instance of WebHostEnvironment.</param>
    public ExceptionHandlingMiddleware(IWebHostEnvironment env)
    {
        Environment = env;
        _exceptionHandlers = new Dictionary<Type, Action<HttpContext, Exception>>
        {
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(ValidationException), HandleValidationException },
            { typeof(NotImplementedException), HandleNotImplementedException },
        };
    }

    /// <summary>
    /// Is invoked by the previous middleware in the pipeline.
    /// </summary>
    /// <param name="context">The http context.</param>
    /// <param name="next">RequestDelegate to access the next middleware in the pipeline.</param>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            HandleExceptionAsync(context, e);
        }
    }

    /// <summary>
    /// Invokes exception handlers based on the type of exception that has been raised.
    /// </summary>
    /// <param name="context">The http context.</param>
    /// <param name="exception">The raised exception.</param>
    private void HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var type = exception.GetType();
        if (_exceptionHandlers.TryGetValue(type, out var value))
        {
            value.Invoke(context, exception);
            return;
        }

        NoHandlerForException(context, exception);
    }

    /// <summary>
    /// Handler for NotFoundException.
    /// </summary>
    /// <param name="context">The http context.</param>
    /// <param name="exception">The raised exception.</param>
    private void HandleNotFoundException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        context.Response
            .WriteAsJsonAsync((exception as NotFoundException)!.Message)
            .GetAwaiter()
            .GetResult();
    }

    /// <summary>
    /// Handler for NotImplementedException.
    /// </summary>
    /// <param name="context">The http context.</param>
    /// <param name="exception">The raised exception.</param>
    private void HandleNotImplementedException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status501NotImplemented;
        context.Response
            .WriteAsJsonAsync((exception as NotImplementedException)!.Message)
            .GetAwaiter()
            .GetResult();
    }

    /// <summary>
    /// Handler for ValidationException.
    /// </summary>
    /// <param name="context">The http context.</param>
    /// <param name="exception">The raised exception.</param>
    private void HandleValidationException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response
            .WriteAsJsonAsync((exception as ValidationException)!.Errors)
            .GetAwaiter()
            .GetResult();
    }

    /// <summary>
    /// Fallback when the passed exception does not have a handler.
    /// </summary>
    /// <param name="context">The http context.</param>
    /// <param name="exception">The raised exception.</param>
    private void NoHandlerForException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response
            .WriteAsJsonAsync(new
            {
                Message = "An unexpected error occurred.",
                Exception = Environment.IsDevelopment() ? exception : null
            })
            .GetAwaiter()
            .GetResult();
    }

    /// <summary>
    /// The application environment.
    /// </summary>
    private IWebHostEnvironment Environment { get; init; }
}
