using Microsoft.AspNetCore.Http;

namespace WebApiSharedParts.Extensions
{
    public static class HttpRequestExtension
    {
        public static string GetIp(this HttpRequest request)
        {
            return request.HttpContext.Connection.RemoteIpAddress.ToString();
        }
        
        public static string GetUserAgent(this HttpRequest request)
        {
            return request.Headers["User-Agent"];
        }
    }
}