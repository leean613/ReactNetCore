﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Common.Extentions
{
    public static class ActionContextExtensions
    {
        public static void WithNotification(this ActionContext context, string type, string title, string body)
        {
            context.HttpContext.Response.Headers.Add("x-notification-type", type);
            context.HttpContext.Response.Headers.Add("x-notification-title", WebUtility.UrlEncode(title));
            context.HttpContext.Response.Headers.Add("x-notification-body", WebUtility.UrlEncode(body));
        }
    }
}
