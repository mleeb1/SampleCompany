using System;
using System.Linq;
using System.Reflection;
using Company.Common;
using Company.Common.Repository;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SampleSPA.Api.Business;
using SampleSPA.Api.Business.Validators;
using SampleSPA.Api.Resources;
using SampleSPA.Data;

namespace SampleSPA.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<BlogValidator>(); });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage)).ToList();
                    var result = new
                    {
                        Message = WebStrings.ValidationErrors,
                        Errors = errors
                    };

                    return new ObjectResult(result)
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                };
            });

            services.AddDbContext<BloggingContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BloggingDatabase")));

            services.AddScoped(typeof(IDbContext), provider => provider.GetService<BloggingContext>());
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IBlogProcessor), typeof(BlogProcessor));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Sample SPA API",
                    Description = "Sample SPA Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Our Team",
                        Email = "OurEmail@company.com"
                    }
                });

                // Set comments path for Swagger JSON and UI
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = ConfigurationPath.Combine(AppContext.BaseDirectory, xmlFile).Replace($":{xmlFile}", $"{xmlFile}", StringComparison.InvariantCultureIgnoreCase);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable Middleware to serve Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable Middleware to serve swagger-ui (HTML, JS, CSS, etc.)
            // Specifying the Swagger JSON endpoint
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample SPA API v1");
                c.RoutePrefix = "doc";
            });

            app.UseCors("CorsPolicy");
            //app.UseCors(options => options.AllowAnyOrigin());
            //app.UseCors(options => options.WithOrigins("https://localhost:8080"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
