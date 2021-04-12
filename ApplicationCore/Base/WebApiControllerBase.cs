using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Primitives;
using ApplicationCore.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ApplicationCore.Models;
using ApplicationCore.Utility;
using System.Linq;
using System.Security.Claims;

namespace ApplicationCore.Base
{
    public class WebApiControllerBase : ControllerBase, IActionFilter
    {
        public DataContext DataContext { get; private set; }

        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            var tokenHeader = new StringValues();
            var error = string.Empty;

            if (context.HttpContext.Request.Headers.TryGetValue("App-Token", out tokenHeader))
            {
                string xToken = tokenHeader;
                var userSvc = Request.HttpContext.RequestServices.GetRequiredService<IUser>();
                var jwtSvc = Request.HttpContext.RequestServices.GetRequiredService<IJwtManager>();
                var optSvc = Request.HttpContext.RequestServices.GetRequiredService<ConfigOptions>();

                try
                {
                    var tokenData = jwtSvc.GetPrincipal(optSvc.JwtOptions, xToken, true);

                    if (tokenData != null && tokenData.Claims.Any())
                    {
                        var userId = tokenData.Claims.FirstOrDefault(y => y.Type == ClaimTypes.Sid).Value;
                        var userData = userSvc.GetUser(userId).GetAwaiter().GetResult();
                        DataContext = new DataContext()
                        {
                            EntryTime = DateTime.Now,
                            IpAddress = context.HttpContext.Connection.RemoteIpAddress.ToString(),
                            User = userData
                        };
                    }
                }
                catch (Exception ex)
                {
                    if (Miscellaneous.GetException(ex).ToLower().Contains("the token is expired"))
                    {
                        context.Result = new OkObjectResult(new ExceptionResult("200", "Lifetime validation failed. The token is expired"));
                        return;
                    }
                    else
                    {
                        context.Result = new OkObjectResult(new ExceptionResult("200", Miscellaneous.GetException(ex)));
                        return;
                    }
                }
            }
            else
            {
                context.Result = new OkObjectResult(new ExceptionResult("200", "The token is null or empty"));
                return;
            }
        }

    }
}
