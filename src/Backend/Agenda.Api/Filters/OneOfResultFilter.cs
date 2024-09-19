using System.Net;
using Agenda.Error;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Agenda.Api.Filters;

public class OneOfResultFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is ObjectResult { Value: AppError error })
        {
            if (error.ErrorType is ErrorType.Validation or ErrorType.BusinessRule)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new BadRequestObjectResult(error);
            }
            else
            {
                context.Result = new ObjectResult("Internal server error")
                {
                    StatusCode = 500
                };
            }
        }

        await next();
    }
}