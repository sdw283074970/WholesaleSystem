using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SoapCore;
using WholesaleSystem.Dto;
using WholesaleSystem.Models;

namespace WholesaleSystem
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
            var DefaultConnection = Configuration.GetConnectionString("DefaultConnection");
            var StorageConnectionstring = Configuration.GetSection("AppSettings")["StorageConnectionstring"];
            var ContainerName = Configuration.GetSection("AppSettings")["ContainerName"];
            AppSettingsModel.DefaultConnection = DefaultConnection;
            AppSettingsModel.StorageConnectionstring = StorageConnectionstring;
            AppSettingsModel.ContainerName = ContainerName;

            services.AddSoapCore();
            //services.TryAddSingleton<ServiceContractImpl>();

            services.AddControllers().AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });
            services.AddDbContext<ApplicationDbContext>().AddEntityFrameworkSqlServer();
            services.AddAutoMapper(typeof(AutoMapperConfig));
            services.AddCors(option => option.AddPolicy("cors", policy => policy.WithOrigins(new[] { "http://localhost:8080", "http://localhost:8090", "https://www.allbuylow.com", "https://allbuylow.com", "http://allbuylow.com", "http://giveitsaveit.com", "https://giveitsaveit.com", "https://www.giveitsaveit.com" })));

            services.AddCors(op =>
            {
                op.AddPolicy("cors2", set =>
                {
                    set.SetIsOriginAllowed(origin => true)
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
                });
            });

            services.AddMvc()
                //.AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase)
                .AddXmlSerializerFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("cors2");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            string path = @"D:\\";
            path = Path.Combine(path, @"UploadedFiles\Images");

            // 通过url访问文件
            app.UseStaticFiles(new StaticFileOptions()  // 自定义自己的文件路径
            {
                RequestPath = new PathString("/Images"),   // 对外的访问路径
                FileProvider = new PhysicalFileProvider(path)   // 指定实际物理路径
            });
        }
    }
}
