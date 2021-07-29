using System;
using System.Web;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Task1.Filters
{
    public class MyExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            string exceptionMessage = context.Exception.Message;
            string request = context.HttpContext.Request.Host.Host + context.HttpContext.Request.Host.Port + context.HttpContext.Request.Path;
            context.Result = new ContentResult
            {
                Content = $"Возникло исключение: \n {exceptionMessage}"
            };
            Log.Error("{exception} {URL}" , exceptionMessage, request);
            context.ExceptionHandled = true;
        }

    }
}
