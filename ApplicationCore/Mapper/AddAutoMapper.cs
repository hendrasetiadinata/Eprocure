using ApplicationCore.Utility;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;

namespace ApplicationCore.Mapper
{
    public class ConfigureMapper
    {
        public static void Init(IServiceCollection services, Assembly executeAssembly)
        {
            var assembly = new Assembly[] {
                executeAssembly,
                Miscellaneous.GetAssemblyByName("WebApi_eProcure")
            };
            services.AddAutoMapper(assembly);
        }
    }
}
