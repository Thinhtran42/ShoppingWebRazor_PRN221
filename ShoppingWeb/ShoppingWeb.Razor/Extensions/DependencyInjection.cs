using Microsoft.EntityFrameworkCore;
using ShoppingWeb.Repository;
using ShoppingWeb.Repository.Interfaces;
using ShoppingWeb.Repository.Models;

namespace ShoppingWeb.Razor.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<ShoppingWebRazorDatabaseContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ShoppingDB")));
            return services;
        }
    }
}