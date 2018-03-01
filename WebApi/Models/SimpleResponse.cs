using System.Net;

namespace WebApi.Models {
    public class SimpleResponse
    {
        public bool IsSuccess { get; set; }
        public string ErrorText { get; set; }
        public HttpStatusCode Code { get; set; }
        public static SimpleResponse Success()
        {
            return new SimpleResponse { IsSuccess = true };
        }
        public static SimpleResponse<T> Success<T>(T data)
        {
            return new SimpleResponse<T> { IsSuccess = true, Data = data, Code =HttpStatusCode.OK  };
        }
        public static SimpleResponse Error(HttpStatusCode code, string errorText)
        {
            return new SimpleResponse { ErrorText = errorText, Code = code};
        }
        public static SimpleResponse Error(string errorText)
        {
            return new SimpleResponse { ErrorText = errorText };
        }
        public static SimpleResponse<T> Error<T>(string errorText, T data)
        {
            return new SimpleResponse<T> { ErrorText = errorText, Data = data };
        }
    }

    public class SimpleResponse<T> : SimpleResponse
    {
        public T Data { get; set; }
    }
}