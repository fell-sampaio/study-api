using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace StudyApi.Api.Configuration;

public static class ApiConfig
{
    public static IServiceCollection AddWebApiConfig(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static IApplicationBuilder UseWebApiConfig(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors(x => x.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        }

        app.UseHttpsRedirection();

        app.UseCors("Development");

        app.UseRouting();

        app.MapControllers();

        app.MapControllers();

        return app;
    }
}
