﻿using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi_eProcure.ActionFilter.ActionFilterAttributes
{
    public class UserFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }
    }
}
