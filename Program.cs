using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Guest.Middleware;
using Guest.Data;
using Guest.Interfaces;
using Guest.Services;
using Guest.Util;
using Serilog;
using Guest.CQRS.Behaviors;
using MediatR;
using Microsoft.AspNetCore.HttpsPolicy;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers().AddJsonOptions(options =>
         options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        #region logger
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);
        builder.Services.AddMediatR(a => a.RegisterServicesFromAssembly(typeof(Program).Assembly));
        builder.Services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        #endregion

        #region Swagger
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Guest Service", Version = "v1", });
            c.OperationFilter<CustomHeaderSwaggerAttribute>();
        });
        #endregion

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddDbContext<DataContext>();
        builder.Services.AddScoped<IGuestsService, GuestsService>();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddScoped<ValidationFilterAttribute>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseMiddleware<AuthenticationMiddleware>();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}