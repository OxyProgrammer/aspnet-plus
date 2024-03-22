using CQRSPlus;
using CQRSPlus.Contracts;
using CQRSPlus.Extensions;
using CQRSPlus.Presentation.ActionFilters;
using CQRSPlus.Service.DataShaping;
using CQRSPlus.Shared.DataTransferObjects;
using CQRSPlus.Utility;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.ConfigureCors();
//builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
//Add data shaper
builder.Services.AddScoped<IDataShaper<EmployeeDto>, DataShaper<EmployeeDto>>();

//Add filters
builder.Services.AddScoped<ValidationFilterAttribute>();
//The following is added to prevent the opinionated validation by the "ApiController" attribute.
//E.g. An empty post body.
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() =>
new ServiceCollection()
.AddLogging()
.AddMvc()
.AddNewtonsoftJson()
.Services.BuildServiceProvider()
.GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
.OfType<NewtonsoftJsonPatchInputFormatter>().First();

//The following is added to enable custom content type like xml and csv.
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
    config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
}).AddXmlDataContractSerializerFormatters()
.AddCustomCSVFormatter()
.AddApplicationPart(typeof(CQRSPlus.Presentation.AssemblyReference).Assembly);

//For HATEOS
builder.Services.AddCustomMediaTypes();
builder.Services.AddScoped<IEmployeeLinks, EmployeeLinks>();
builder.Services.AddScoped<ValidateMediaTypeAttribute>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//var logger = app.Services.GetRequiredService<ILoggerManager>();
//app.ConfigureExceptionHandler(logger);
app.UseExceptionHandler(opt => { });
if (app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");


app.UseAuthorization();

app.MapControllers();

app.Run();
