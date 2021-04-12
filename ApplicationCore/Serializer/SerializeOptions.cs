using Microsoft.Extensions.DependencyInjection;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Serializer
{
    public class SerializeOptions
    {
        public static void Init(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(opt => {
                    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
        }

        public static void Init(IServiceCollection services, Action<MvcNewtonsoftJsonOptions> action)
            => services.AddControllers().AddNewtonsoftJson(action);
    }
}
