using Newtonsoft.Json;

namespace ElGuerre.AspNetCore.Cross.Exception.Infrastructure
{
    public class ApiResponse<T>
    {
        public ApiResponse() { }

        public ApiResponse(T data) => Data = data;

        public bool IsValid { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this,
                 new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
        }
    }

    public class ApiResponse : ApiResponse<object>
    {
    }
}
