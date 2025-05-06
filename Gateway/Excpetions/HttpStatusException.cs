namespace moveo.Excpetions
{
    public class HttpStatusCodeException(int statusCode, string message) : Exception(message)
    {
        public int StatusCode { get; } = statusCode;
    }

    public class NotFoundException(string message) : HttpStatusCodeException(StatusCodes.Status404NotFound, message)
    {
    }

    public class BadRequestException(string message) : HttpStatusCodeException(StatusCodes.Status400BadRequest, message)
    {
    }
}
