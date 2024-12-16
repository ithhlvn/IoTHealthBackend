using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Loggings
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log Request
            context.Request.EnableBuffering(); // Cho phép đọc lại nội dung request
            var requestBody = await ReadRequestBody(context.Request);
            _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path} {requestBody}");

            // Đảm bảo stream có thể được đọc lại
            context.Request.Body.Seek(0, SeekOrigin.Begin);

            // Log Response
            var originalBodyStream = context.Response.Body;
            using (var responseBodyStream = new MemoryStream())
            {
                context.Response.Body = responseBodyStream;

                await _next(context); // Thực thi tiếp các middleware tiếp theo

                // Log Response
                var responseBody = await ReadResponseBody(context.Response);
                _logger.LogInformation($"Response: {context.Response.StatusCode} {responseBody}");

                // Copy lại response body vào stream gốc
                await responseBodyStream.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.Body.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true))
            {
                return await reader.ReadToEndAsync();
            }
        }

        private async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(response.Body, Encoding.UTF8, leaveOpen: true))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }

    /*
    public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestResponseLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        // Lấy thông tin request
        var request = httpContext.Request;
        var requestBody = await ReadRequestBodyAsync(request);

        // Ghi log thông tin request
        Log.Information("Request: {Method} {Path} {QueryString} {RequestBody}",
            request.Method, request.Path, request.QueryString, requestBody);

        // Chạy middleware tiếp theo
        var originalResponseBodyStream = httpContext.Response.Body;
        using (var responseBodyStream = new MemoryStream())
        {
            httpContext.Response.Body = responseBodyStream;

            // Tiếp tục xử lý request
            await _next(httpContext);

            // Lấy thông tin response
            var response = httpContext.Response;
            var responseBody = await ReadResponseBodyAsync(responseBodyStream);

            // Ghi log thông tin response
            Log.Information("Response: {StatusCode} {ResponseBody}", response.StatusCode, responseBody);

            // Copy response body ra stream gốc
            await responseBodyStream.CopyToAsync(originalResponseBodyStream);
        }
    }

    private async Task<string> ReadRequestBodyAsync(HttpRequest request)
    {
        request.EnableBuffering(); // Cho phép đọc lại body của request
        using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true))
        {
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0; // Đặt lại vị trí của stream về đầu
            return body;
        }
    }

    private async Task<string> ReadResponseBodyAsync(MemoryStream responseBodyStream)
    {
        responseBodyStream.Seek(0, SeekOrigin.Begin);
        using (var reader = new StreamReader(responseBodyStream))
        {
            return await reader.ReadToEndAsync();
        }
    }
} 
     */
}
