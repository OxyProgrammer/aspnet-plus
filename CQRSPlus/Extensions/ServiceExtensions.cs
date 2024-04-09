using ASPNETPlus.Contracts;
using ASPNETPlus.LoggerService;
using ASPNETPlus.Repository;
using ASPNETPlus.Service;
using ASPNETPlus.Service.Contracts;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asp.Versioning;
using System.Threading.RateLimiting;
using Microsoft.OpenApi.Models;

namespace ASPNETPlus.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
             services.AddCors(options =>
             {
                 options.AddPolicy("CorsPolicy", builder =>
                 builder.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .WithExposedHeaders("X-Pagination"));
             });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
         services.Configure<IISOptions>(options =>
         {
         });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
         services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
         services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
         services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
         services.AddDbContext<RepositoryContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder) =>
             builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));

        public static void AddCustomMediaTypes(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(config =>
            {
                var systemTextJsonOutputFormatter = config.OutputFormatters.OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();
                if (systemTextJsonOutputFormatter != null)
                {
                    systemTextJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.aspnetplus.hateoas+json");
                    systemTextJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.aspnetplus.apiroot+json");
                }
                var xmlOutputFormatter = config.OutputFormatters.OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();
                if (xmlOutputFormatter != null)
                {
                    xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.aspnetplus.hateoas+xml");
                    xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.aspnetplus.apiroot+xml");
                }
            });
        }

        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new QueryStringApiVersionReader("api-version");
            }).AddMvc();
        }

        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {
            services.AddRateLimiter(opt =>
            {
                opt.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context => RateLimitPartition.GetFixedWindowLimiter("GlobalLimiter",
                partition => new FixedWindowRateLimiterOptions
                {
                    AutoReplenishment = true,
                    PermitLimit = 5,
                    QueueLimit = 2,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    Window = TimeSpan.FromMinutes(1)
                }));
                opt.AddPolicy("SpecificPolicy", context => RateLimitPartition.GetFixedWindowLimiter("SpecificLimiter",
                 partition => new FixedWindowRateLimiterOptions
                 {
                     AutoReplenishment = true,
                     PermitLimit = 3,
                     Window = TimeSpan.FromSeconds(10)
                 }));
                opt.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = 429;
                    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                    {
                        await context.HttpContext.Response.WriteAsync($"Too many requests. Please try again after {retryAfter.TotalSeconds} second(s).", token);
                    }
                    else
                    {
                        await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.", token);
                    }
                };
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ASPNETPlus",
                    Version = "v1"
                });
                s.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "ASPNETPlus",
                    Version = "v2"
                });
            });
        }

    }
}
