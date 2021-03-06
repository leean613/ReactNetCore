﻿using Common.Exceptions;
using Common.Extentions;
using Infrastructure.ApiResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Infrastructure.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            _logger.LogError(
                new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            ApiErrorResult apiErrorResult;

            if (context.Exception.GetType() == typeof(BusinessException) || context.Exception.GetType().BaseType == typeof(BusinessException))
            {
                // handle bussiness exception
                apiErrorResult = new ApiErrorResult
                {
                    Success = false,
                    ErrorCode = context.Exception.GetType().Name,
                    ErrorMessage = context.Exception.Message,
                };

                context.Result = new BadRequestObjectResult(apiErrorResult);
                context.WithNotification("error", string.Empty, apiErrorResult.ErrorMessage);

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                // if it's not one of the expected exception, set it to 500
                var code = HttpStatusCode.InternalServerError;

                //TODO:Mapping if (exception is NotFoundExe) code = HttpStatusCode.NotFound;
                switch (exception)
                {
                    case EntityNotFoundException _:
                        code = HttpStatusCode.NotFound;
                        break;
                    case ArgumentNullException _:
                        code = HttpStatusCode.BadRequest;
                        break;
                    case InvalidArgumentException _:
                        code = HttpStatusCode.BadRequest;
                        break;
                    case HttpRequestException _:
                        code = HttpStatusCode.BadRequest;
                        break;
                    case UnauthorizedAccessException _:
                        code = HttpStatusCode.Unauthorized;
                        break;
                }

                apiErrorResult = new ApiErrorResult
                {
                    Success = false,
                    ErrorCode = code.ToString(),
                    ErrorMessage = exception.InnerException != null ? exception.InnerException.Message : exception.Message,
                    TechnicalLog = exception.GetExceptionTechnicalInfo(),
                };

                context.Result = new ObjectResult(apiErrorResult);

                context.WithNotification("error", string.Empty, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");

                context.HttpContext.Response.StatusCode = (int)code;
            }


            context.ExceptionHandled = true;
        }
    }
}
