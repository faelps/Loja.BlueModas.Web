namespace BlueModas.Web.Models
{
    public class Request
    {
        public Request(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; private set; }
        public string Message { get; private set; }
    }
}
