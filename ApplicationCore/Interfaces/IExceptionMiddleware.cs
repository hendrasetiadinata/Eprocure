using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IExceptionMiddleware
    {
        Task InvokeAsync(HttpContext httpContext);
    }
}