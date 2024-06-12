using EShop.Application.Interfaces;
using EShop.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EShop.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<EShopDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("EShopConnectionString"));
            });
            builder.Services.AddScoped<IEShopDbContext, EShopDbContext>();

            builder.Services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.UseDeveloperExceptionPage();

            app.Run();
        }
    }
}
