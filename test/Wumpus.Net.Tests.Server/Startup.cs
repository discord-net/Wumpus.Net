using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wumpus.Serialization;
using Wumpus.Server.Binders;
using Wumpus.Server.Formatters;

namespace Wumpus.Server
{
    public class Startup
    {
        private class NullObjectValidator : IObjectModelValidator
        {
            public void Validate(ActionContext actionContext, ValidationStateDictionary validationState, string prefix, object model) { }
        }

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(x =>
                {
                    x.ModelBinderProviders.Insert(0, new VoltaicModelBinderProvider());
                })
                .AddVoltaicJsonSerializerFormatters(new WumpusJsonSerializer())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<IObjectModelValidator, NullObjectValidator>();
            //services.Configure<ApiBehaviorOptions>(x =>
            //    {
            //        x.SuppressInferBindingSourcesForParameters = true;
            //        x.InvalidModelStateResponseFactory = context =>
            //        {
            //            var msg = "Model validation failed:\n" + string.Join("\n", context.ModelState
            //                .Where(e => e.Value.Errors.Count > 0)
            //                .Select(e => $"{e.Key}: {e.Value.Errors.First().ErrorMessage}"));
            //            return new BadRequestObjectResult(new RestError
            //            {
            //                Message = (Utf8String)msg
            //            });
            //        };
            //    });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseMvc();
        }
    }
}
