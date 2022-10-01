using System.Net;

namespace NextFap.Application.Common.Models
{
    public class ApiResponse
    {
        public ApiResponse(bool succeeded, HttpStatusCode statusCode, string messages, object? data = null)
        {
            Succeeded = succeeded;
            StatusCode = statusCode;
            Messages = messages;
            Data = data;
        }

        public bool Succeeded { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string? Messages { get; set; }
        public object? Data { get; set; }


        public static ApiResponse Success(object? data)
        {
            return new ApiResponse(true, HttpStatusCode.Accepted, String.Empty, data);
        }

        public static ApiResponse Failure(HttpStatusCode statusCode, string errors)
        {
            return new ApiResponse(false, statusCode, String.Empty, errors);
        }
    }
}
