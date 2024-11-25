using System.Net;

namespace Errors;

public class BaseApplicationException : Exception
{
    public HttpStatusCode StatusCode { get; private set; }
    public new object? Data { get; protected set; } = null;
    
    public BaseApplicationException(string? message) : base(message)
    {
        StatusCode = HttpStatusCode.BadRequest;
    }
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
    public FieldValidationException(string? message, object? data) : base(message, HttpStatusCode.BadRequest)
    {
        this.Data = data;
    }

    public FieldValidationException(string? message, object? data, Exception? innerException) : base(message, HttpStatusCode.BadRequest, innerException)
    {
        this.Data = data;
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

public class OutOfStockException : BaseApplicationException
{
    public OutOfStockException(string? message) : base(message, HttpStatusCode.BadRequest)
    {
    }

    public OutOfStockException(string? message, Exception? innerException) : base(message, HttpStatusCode.BadRequest, innerException)
    {
    }
}