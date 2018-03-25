//
// We have tu use a Filter instead of Middleware, because Swagger does not works properly
//
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ElGuerre.AspNetCore.Cross.Filter
{
    public class JsonResponseFilter : IResultFilter
    { 
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!(context.ActionDescriptor is ControllerActionDescriptor))
            {
                return;
            }

            var objectResult = context.Result as ObjectResult;
            if (objectResult == null)
            {
                return;
            }

            if (!(objectResult.Value is ApiResponse))
            {
                objectResult.Value = new ApiResponse()
                {
                    IsValid = true,
                    Data = objectResult.Value,
                    Message = null,
                }.ToJson();
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

    }
}
