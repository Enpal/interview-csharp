using FastEndpoints.Swagger;
using UrlShortenerService.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    _ = builder.Configuration
    .AddJsonFile($"{AppDomain.CurrentDomain.BaseDirectory}/appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"{AppDomain.CurrentDomain.BaseDirectory}/appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
}

// Add services to the container.
_ = builder.Services.AddMvc();
_ = builder.Services.SwaggerDocument();
_ = builder.Services.AddSwaggerDocument(x =>
{
    x.DocumentName = "Release 1.0";
    x.Title = "Url Shortener Service";
    x.Version = "1.0";
});

_ = builder.Services.AddApplicationServices();
_ = builder.Services.AddInfrastructureServices(builder.Configuration);
_ = builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

_ = app.UseMiddleware<ExceptionHandlingMiddleware>();

_ = app.UseFastEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseOpenApi();
    _ = app.UseSwaggerUI();
}

_ = app.UseHttpsRedirection();

app.Run();
