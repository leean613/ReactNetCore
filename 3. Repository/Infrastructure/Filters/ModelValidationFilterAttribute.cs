using Infrastructure.ApiResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Filters
{
    public class ModelValidationFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var error = context.ModelState
                    .FirstOrDefault(x => x.Value.Errors.Any())
                    .Value
                    .Errors.FirstOrDefault();

                context.Result = new BadRequestObjectResult(
                    new ApiErrorResult
                    {
                        Success = false,
                        ErrorCode = ApiErrorCodes.InvalidParamters.ToString(),
                        ErrorMessage = error?.ErrorMessage != null && string.IsNullOrWhiteSpace(error.ErrorMessage) ? error.Exception.Message : error?.ErrorMessage,
                    });

                return;
            }

            await next();
        }
    }
}
