using EShop.Application.Extensions;
using EShop.Infrastructure.Extensions;
using EShop.Infrastructure.Utilities;

namespace EShop.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

        builder.Services.AddApplication();
        builder.Services.AddInfrastrusture(configuration);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAnyOrigin", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
            //app.ApplyMigrations();
        }

        app.UseCors("AllowAnyOrigin");
        //app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
