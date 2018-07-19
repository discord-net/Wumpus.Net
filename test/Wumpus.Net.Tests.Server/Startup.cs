using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Voltaic;
using Voltaic.Serialization;
using Wumpus.Entities;
using Wumpus.Serialization;
using Wumpus.Server.Binders;
using Wumpus.Server.Formatters;

namespace Wumpus.Server
{
    public class Startup
    {
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
            services.Configure<ApiBehaviorOptions>(x =>
                {
                    x.SuppressInferBindingSourcesForParameters = true;
                    x.InvalidModelStateResponseFactory = context =>
                    {
                        var msg = "Model validation failed:\n" + string.Join("\n", context.ModelState
                            .Where(e => e.Value.Errors.Count > 0)
                            .Select(e => $"{e.Key}: {e.Value.Errors.First().ErrorMessage}"));
                        return new BadRequestObjectResult(new RestError
                        {
                            Message = (Utf8String)msg
                        });
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseMvc();
        }
    }
}
