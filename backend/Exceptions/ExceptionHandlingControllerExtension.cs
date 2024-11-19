using Microsoft.AspNetCore.Mvc;

namespace Errors;

public static class ExceptionHandlingControllerExtension
{
    public static async Task<IActionResult> HandleRequestAsync(this ControllerBase controller,
        Func<Task<IActionResult>> action, IEnumerable<IExceptionHandler> exceptionHandlers)
    {
        try
        {
            return await action();
        }
        catch (Exception e)
        {
            var exceptionHandler = exceptionHandlers.FirstOrDefault(handler => handler.CanHandleException(e));
            return exceptionHandler!.HandleException(e, controller);
        }
    }
}