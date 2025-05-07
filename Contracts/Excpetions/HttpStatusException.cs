namespace Excpetions
{
    public class HttpStatusCodeException(int statusCode, string message) : Exception(message)
    {
        public int StatusCode { get; } = statusCode;
    }

    public class NotFoundException(string message) : HttpStatusCodeException(404, message)
    {
    }

    public class UnauthorizedException(string message) : HttpStatusCodeException(401, message)
    {
    }

    public class BadRequestException(string message) : HttpStatusCodeException(400, message)
    {
    }

    public class ConflictException(string message) : HttpStatusCodeException(409, message)
    {
    }

}
