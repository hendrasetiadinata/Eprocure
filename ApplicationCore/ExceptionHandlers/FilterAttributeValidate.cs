using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.ExceptionHandlers
{
    public class FilterAttributeValidate : ObjectResult
    {
        public FilterAttributeValidate(string errors)
            : base(new ExceptionResult()
            {
                Description = errors
            })
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}
