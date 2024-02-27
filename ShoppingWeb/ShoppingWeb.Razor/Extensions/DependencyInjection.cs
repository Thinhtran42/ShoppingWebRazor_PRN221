using Microsoft.EntityFrameworkCore;
using ShoppingWeb.Repository;
using ShoppingWeb.Repository.Interfaces;
using ShoppingWeb.Repository.Models;

namespace ShoppingWeb.Razor.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<ShoppingWebRazorDatabaseContext>(options =>
                options.UseSqlServer(GetConnectionString()));
            return services;
        }

        private static string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
            var strConn = config["ConnectionStrings:ShoppingDB"];
            return strConn;
        }
    }
}