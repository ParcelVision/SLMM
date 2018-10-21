using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SLMM.Api.Config;
using SLMM.Api.Middleware;
using SLMM.Domain;
using SLMM.Domain.LawnMap;
using Swashbuckle.AspNetCore.Swagger;

namespace SLMM.Api
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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "SLMM API", Version = "v1"}); });

            services.Configure<LawnConfig>(Configuration.GetSection("LawnConfig"));

            services
                .AddSingleton<ILawnMap>(provider =>
                {
                    //TODO: Validate Config
                    var lawnConfig = provider.GetService<IOptions<LawnConfig>>().Value;

                    return new RectLawnMap(new Rectangle(0, 0, lawnConfig.Width, lawnConfig.Length));
                })
                .AddSingleton<ILawnMowingMachine>(provider =>
                {
                    //TODO: Validate Config
                    var lawnConfig = provider.GetService<IOptions<LawnConfig>>().Value;

                    var lawnMap = provider.GetService<ILawnMap>();
                    var startPosition = new Position(new Point(lawnConfig.X, lawnConfig.Y), lawnConfig.Orientation);

                    return new LawnMowingMachine(lawnMap, startPosition);
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "SLMM API V1"); });
            
            app.UseHttpsRedirection();
            app.UseProblemDetailsMiddleware();
            app.UseMvc();
        }
    }
}