using ApplicationCore.Entities;
using ApplicationCore.ExceptionHandlers;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace WebApi_eProcure.ActionFilter.ActionFilterAttributes
{
    public class AddTenderFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.Any(x => x.Key == "value"))
            {
                var request = context.ActionArguments["value"] as Tender;
                var error = string.Empty;

                if (string.IsNullOrEmpty(request.Name)) error = "Tender name is required";
                else if (string.IsNullOrEmpty(request.RefNumber)) error = "Reference number is required";
                else if (string.IsNullOrEmpty(request.CreatorId)) error = "The Creator id is required";
                else if (request.ReleaseDate.Subtract(DateTime.Now).Days <= 0) error = "Release date should be on future date";
                else if (request.ClosingDate.Subtract(DateTime.Now).Days <= 0) error = "Closing date should be on future date";
                else if (request.ClosingDate.Subtract(request.ReleaseDate).Days <= 0) error = "Closing date should be greater than Release Date";

                if (!string.IsNullOrEmpty(error))
                {
                    context.Result = new FilterAttributeValidate(error);
                    return;
                }
            }
        }
    }

    public class UpdateTenderFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.Any(x => x.Key == "value"))
            {
                var request = context.ActionArguments["value"] as Tender;
                var tenderSvc = context.HttpContext.RequestServices.GetRequiredService<ITender>();
                var error = string.Empty;

                if (string.IsNullOrEmpty(request.Id)) error = "The Tender id is required";
                else if (string.IsNullOrEmpty(request.Name)) error = "The Tender name is required";
                else if (string.IsNullOrEmpty(request.RefNumber)) error = "The Reference number is required";
                else if (string.IsNullOrEmpty(request.LastUpdatedBy)) error = "The Last updated user is required";
                else if (request.ReleaseDate.Subtract(DateTime.Now).Days <= 0) error = "The Release date should be on future date";
                else if (request.ClosingDate.Subtract(DateTime.Now).Days <= 0) error = "The Closing date should be on future date";
                else if (request.ClosingDate.Subtract(request.ReleaseDate).Days <= 0) error = "The Closing date should be greater than the release date";
                else
                {
                    var tender = tenderSvc.GetTender(request.Id).GetAwaiter().GetResult();
                    if (tender == null) error = "Tender data does not exist";
                }

                if (!string.IsNullOrEmpty(error))
                {
                    context.Result = new FilterAttributeValidate(error);
                    return;
                }
            }
        }
    }

    public class DeleteTenderFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var paths = context.HttpContext.Request.Path.Value.Split('/').ToList();
            var tenderSvc = context.HttpContext.RequestServices.GetRequiredService<ITender>();
            var tender = tenderSvc.GetTender(paths.Last()).GetAwaiter().GetResult();

            if (tender == null)
            {
                context.Result = new FilterAttributeValidate("Tender data does not exist");
                return;
            }
        }
    }
}
