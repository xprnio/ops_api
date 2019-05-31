namespace OPS_API.Domain.Services.Communication
{
    public class Response
    {
        public readonly bool Success;
        public readonly string Message;

        public Response(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}