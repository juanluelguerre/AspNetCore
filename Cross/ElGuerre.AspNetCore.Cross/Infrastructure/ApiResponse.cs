namespace ElGuerre.AspNetCore.Cross.Exception.Infrastructure
{
    public class ApiResponse<T>
    {
        public ApiResponse() { }

        public ApiResponse(T data) => Data = data;

        public bool IsValid { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }

    public class ApiResponse
    {
        public ApiResponse() { }
        public ApiResponse(object data) => Data = data;
        
        public bool IsValid { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
