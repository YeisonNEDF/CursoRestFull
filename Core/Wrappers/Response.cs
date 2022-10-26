namespace Core.Wrappers
{
    public class Response<T>
    {
        public Response()
        {

        }

        public Response(T data, string? message = null)
        {
            Suceeded = true;
            Data = data;
            Message = message;

        }
        public Response(string message)
        {
            Suceeded = false;
            Message = message;
        }

        public bool Suceeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
