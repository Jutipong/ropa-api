using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Services;
using WebApi.Entities.DdContextTcrb;
//using WebApi.Services.FlowRateCharge;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public ILoggerFactory LoggerFactory;

        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            LoggerFactory = loggerFactory;
        }

        // add services to the DI container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(typeof(Startup));
            services.AddLogging(options => options.AddFilter("Microsoft", LogLevel.None).AddFilter(nameof(System), LogLevel.Warning));
            services.AddCors();
            services.AddControllers();

            // configure strongly typed settings object
            services.Configure<AppSittingModel>(Configuration.GetSection("AppSettings"));

            // configure DI for application services
            services.AddDbContext<DdContextTcrb>();
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

            //services.AddScoped<ILateChargeService, LateChargeService>();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            if ((Convert.ToBoolean(Configuration.GetSection("AppSettings")["Swagger"])))
            {
                services.AddSwaggerGen(swagger =>
                {
                    swagger.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Ropa API",
                        Version = "Version For Developer (◍•ᴗ•◍)❤",
                    });
                    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
                        "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                        "Example: Bearer token",
                    });
                    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            }, Array.Empty<string>()
                    }
                    });
                });
            }

        }

        // configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // global cors policy
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if ((Convert.ToBoolean(Configuration.GetSection("AppSettings")["Swagger"])))
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("./swagger/v1/swagger.json", "For Developer (◍•ᴗ•◍)❤");
                    c.RoutePrefix = string.Empty;
                });
            }

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "api", pattern: "{controller}/{id?}");
            });
            //app.UseEndpoints(x => x.MapControllers());
        }
    }
}
