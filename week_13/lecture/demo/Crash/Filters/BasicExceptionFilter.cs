using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

public class BasicExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        // 1. Tell MVC: "I got this!"
        context.ExceptionHandled = true;

        // 2. Show something
        context.Result = new ContentResult
        {
            Content = "<h1>Something went wrong (filter)</h1>",
            ContentType = "text/html"
        };
    }
}