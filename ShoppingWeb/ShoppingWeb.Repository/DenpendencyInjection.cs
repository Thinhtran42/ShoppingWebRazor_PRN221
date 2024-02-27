using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingWeb.Repository.Interfaces;
using ShoppingWeb.Repository.Models;

namespace ShoppingWeb.Repository
{
	public static class DenpendencyInjection
	{
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, string databaseConnection)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<ShoppingWebRazorDatabaseContext>(option => option.UseSqlServer("Data Source=.;Initial Catalog=ShoppingWebRazorDatabase;User ID=sa;Password=Password.1;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"));

            //services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);

            return services;
        }

    }
}

