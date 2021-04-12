using ApplicationCore.Entities;
using ApplicationCore.ExceptionHandlers;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace WebApi_eProcure.ActionFilter.ActionFilterAttributes
{
    public class AuthenticateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.Any(x => x.Key == "value"))
            {
                var request = context.ActionArguments["value"] as AuthenticateRequest;
                var userSvc = context.HttpContext.RequestServices.GetRequiredService<IUser>();
                var error = string.Empty;

                if (string.IsNullOrEmpty(request.Username)) error = "The Username is required";
                else if (string.IsNullOrEmpty(request.Password)) error = "The Password is required";
                else
                {
                    var userData = userSvc.GetUserByUsername(request.Username).GetAwaiter().GetResult();
                    if (userData == null) error = "The Username does not exist";
                    else
                    {
                        userData = userSvc.GetUserByLogin(request.Username, request.Password).GetAwaiter().GetResult();
                        if (userData == null) error = "Invalid password";
                    }
                }

                if (!string.IsNullOrEmpty(error))
                {
                    context.Result = new FilterAttributeValidate(error);
                    return;
                }
            }
        }
    }
}
