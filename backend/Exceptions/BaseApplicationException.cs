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

public class FieldConflictException : BaseApplicationException
{
    public FieldConflictException(string? message) : base(message, HttpStatusCode.Conflict)
    {
    }

    public FieldConflictException(string? message, Exception? innerException) : base(message, HttpStatusCode.Conflict, innerException)
    {
    }
}

public class NotFoundException : BaseApplicationException
{
    public NotFoundException(string? message) : base(message, HttpStatusCode.NotFound)
    {
    }

    public NotFoundException(string? message, Exception? innerException) : base(message, HttpStatusCode.NotFound, innerException)
    {
    }
}