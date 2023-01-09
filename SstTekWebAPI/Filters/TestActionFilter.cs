using Microsoft.AspNetCore.Mvc.Filters;

namespace SstTekWebAPI.Filters
{
    public class TestActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Request.Headers.Add("test", "1");
            
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }
        

    }
    public class Response
    {
        public string Message { get; set; }
    }
}
