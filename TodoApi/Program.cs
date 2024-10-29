using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure services
        ConfigureServices(builder.Services);

        var app = builder.Build();

        // Configure the HTTP request pipeline
        Configure(app);

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Add CORS policy
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder => builder.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader());
        });

        // Add other services
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    private static void Configure(WebApplication app)
    {
        // Configure middleware
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API v1");
            c.RoutePrefix = string.Empty;
        });

        // Use CORS
        app.UseCors("AllowAllOrigins");

        // Map routes to controllers
        app.MapControllers();
    }
}