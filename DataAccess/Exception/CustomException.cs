using System.Net;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DataAccess.Exception;

public class ExceptionHandlingMiddleware
{
    public RequestDelegate requestDelegate;
    public ExceptionHandlingMiddleware(RequestDelegate requestDelegate)
    {
        this.requestDelegate = requestDelegate;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await requestDelegate(context);
        }
        catch (System.Exception ex)
        {
            await HandleException(context, ex);
        }
    }
    private static Task HandleException(HttpContext context, System.Exception ex)
    {
        
        //var errorMessage = JsonConvert.SerializeObject(new { Message = ex.Message, Code = "GE" });
        var errorMessage = JsonConvert.SerializeObject(new
        {
            Sources=ex.Source,
            Exception="Error",
            ErrorMessage = ex.Message, 
            ErrorId=Guid.NewGuid(),
            StatusCode=HttpStatusCode.InternalServerError
        });
 
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
 
        return context.Response.WriteAsync(errorMessage);
    }

    

}