using Microsoft.AspNetCore.Builder;

namespace ApplicationCore.ExceptionHandlers
{
    public class ConfigureErrorHandling
    {
        public static void Init(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
