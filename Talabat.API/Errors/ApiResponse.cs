namespace Talabat.API.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode, string message = null) 
        { 
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(int statusCode);
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
            => statusCode switch
            {
                400 => "A Bad Requset, You Have Made",
                401 => "Authorized, You Are Not",
                404 => "Resourse Was Not Found",
                500 => "Server Error",
                _ => null
            };
    }
}
