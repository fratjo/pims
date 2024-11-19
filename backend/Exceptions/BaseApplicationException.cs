using System.Net;

namespace Errors;

public class BaseApplicationException : Exception
{
    public HttpStatusCode StatusCode { get; private set; }

    public BaseApplicationException(string? message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public BaseApplicationException(string? message, HttpStatusCode statusCode, Exception? innerException)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}

public class FieldValidationException : BaseApplicationException
{
    public FieldValidationException(string? message) : base(message, HttpStatusCode.BadRequest)
    {
    }

    public FieldValidationException(string? message, Exception? innerException) : base(message, HttpStatusCode.BadRequest, innerException)
    {
    }
}