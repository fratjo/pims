using System.Net;

namespace Errors;

public class ValidationException(string message, HttpStatusCode statusCode) : Exception(message);