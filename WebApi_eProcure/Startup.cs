using ApplicationCore.DatabaseContext;
using ApplicationCore.ExceptionHandlers;
using ApplicationCore.Interfaces;
using ApplicationCore.Jwt;
using ApplicationCore.Mapper;
using ApplicationCore.Models;
using ApplicationCore.Serializer;
using ApplicationCore.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;
using System.Reflection;
using WebApi_eProcure.ActionFilter;
using WebApi_eProcure.ActionFilter.ActionFilterAttributes;


namespace WebApi_eProcure
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi_eProcure", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.OperationFilter<AddRequiredHeaderParameter>();
            });

            var configOptions = Configuration.GetSection("ConfigOptions").Get<ConfigOptions>();

            // This services add application setting services
            services.AddSingleton(configOptions);

            // This services add Database Context services
            services.AddDbContext<EProcurementContext>(options => options.UseSqlServer(configOptions.DefaultConnectionString));

            // This services add services from Application Core Project.
            AddCoreServices(services);

            // This services add ActionFilterAttribute services.
            AddActionFilterAttributeServices(services);

            // This services add AutoMapper services.
            ConfigureMapper.Init(services, Assembly.GetAssembly(typeof(Startup)));

            // This services add health and check url point
            services.AddHealthChecks();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));
            }


            // This method add Global exception handler.
            ConfigureErrorHandling.Init(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseHealthChecks("/healthcheck");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddCoreServices(IServiceCollection services)
        {
            // This method add Newtonsoft serialize services.
            SerializeOptions.Init(services);

            // This method add jwt manager
            services.AddSingleton<IJwtManager, JwtManager>();

            // This method add user services
            services.AddScoped<IUser, UserServices>();

            // This method add tender services
            services.AddScoped<ITender, TenderServices>();

            // This method add authenticate services
            services.AddScoped<IAuthenticate, AuthenticateServices>();
        }

        private void AddActionFilterAttributeServices(IServiceCollection services)
        {
            services.AddScoped<AddTenderFilterAttribute>();
            services.AddScoped<UpdateTenderFilterAttribute>();
            services.AddScoped<DeleteTenderFilterAttribute>();
            services.AddScoped<AuthenticateFilterAttribute>();
            services.AddScoped<UserFilterAttribute>();
        }
    }
}
