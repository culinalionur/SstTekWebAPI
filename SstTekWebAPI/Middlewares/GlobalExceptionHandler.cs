using System.Net;

namespace SstTekWebAPI.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _request;

        public GlobalExceptionHandler(RequestDelegate request)
        {
            _request = request;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                Console.WriteLine("I am a middleware");
                await _request(httpContext);
            }
            catch (Exception e)
            {

               await  HandleOurException(httpContext, e);
            }
        }

        private async Task HandleOurException(HttpContext context,Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new Error
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString()); 
        }

        public class Error
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }

        }
    }
}
