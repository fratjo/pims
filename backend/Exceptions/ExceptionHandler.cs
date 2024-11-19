using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Errors;

public interface IExceptionHandler
{
    bool CanHandleException(Exception exception);
    IActionResult HandleException(Exception exception, ControllerBase controller);
}

public static class ExceptionHandlerServiceExtensions
{
    public static void AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddSingleton<IExceptionHandler, NotFoundExceptionHandler>();
        services.AddSingleton<IExceptionHandler, FieldValidationExceptionHandler>();
        services.AddSingleton<IExceptionHandler, FieldConflictExceptionHandler>();
        services.AddSingleton<IExceptionHandler, BaseApplicationExceptionHandler>();
        services.AddSingleton<IExceptionHandler, GeneralExceptionHandler>();
    }
}

public class GeneralExceptionHandler : IExceptionHandler
{
    public bool CanHandleException(Exception exception)
    {
        return true;
    }

    public IActionResult HandleException(Exception exception, ControllerBase controller)
    {
        return controller.StatusCode(500, $"Internal Server Error: {(exception.Message ?? "An error occurred")}");
    }
}

public class BaseApplicationExceptionHandler : IExceptionHandler
{
    public bool CanHandleException(Exception exception)
    {
        return exception is BaseApplicationException;
    }

    public IActionResult HandleException(Exception exception, ControllerBase controller)
    {
        var baseException = exception as BaseApplicationException;
        return controller.StatusCode(500, baseException!.Message);
    }
}

public class FieldValidationExceptionHandler : IExceptionHandler
{
    public bool CanHandleException(Exception exception)
    {
        return exception is FieldValidationException;
    }

    public IActionResult HandleException(Exception exception, ControllerBase controller)
    {
        var fieldValidationException = exception as FieldValidationException;
        return controller.BadRequest(new { message = fieldValidationException!.Message });
    }
}

public class FieldConflictExceptionHandler : IExceptionHandler
{
    public bool CanHandleException(Exception exception)
    {
        return exception is FieldConflictException;
    }

    public IActionResult HandleException(Exception exception, ControllerBase controller)
    {
        var fieldConflictException = exception as FieldConflictException;
        return controller.BadRequest(new { message = fieldConflictException!.Message });
    }
}

public class NotFoundExceptionHandler : IExceptionHandler
{
    public bool CanHandleException(Exception exception)
    {
        return exception is NotFoundException;
    }

    public IActionResult HandleException(Exception exception, ControllerBase controller)
    {
        var notFoundException = exception as NotFoundException;
        return controller.NotFound(new { message = notFoundException!.Message });
    }
}