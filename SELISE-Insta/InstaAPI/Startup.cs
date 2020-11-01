using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using InstaAPI.InstaMapper;
using InstaAPI.Models;
using InstaAPI.Repository;
using InstaAPI.Repository.IRepository;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

namespace InstaAPI
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
            services.Configure<SeliseInstaDatabaseSettings>(
                Configuration.GetSection(nameof(SeliseInstaDatabaseSettings)));

            services.AddSingleton<ISeliseInstaDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<SeliseInstaDatabaseSettings>>().Value);

            services.AddScoped<IPhotoRepository, PhotoRepository>();

            services.AddAutoMapper(typeof(InstaMappings));

            services.AddSwaggerGen(options => {
                options.SwaggerDoc("SELISEInstaAPISpec",
                new OpenApiInfo()
                {
                    Title = "Insta API",
                    Version = "1"
                });
                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                options.IncludeXmlComments(cmlCommentsFullPath);
            });

            services.AddMediatR(typeof(Startup));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("swagger/SELISEInstaAPISpec/swagger.json", "SELISE-Insta API");
                options.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
