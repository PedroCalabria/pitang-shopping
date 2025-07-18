using Microsoft.EntityFrameworkCore;
using PitangBoosterVendas.Repository;

namespace PitangBoosterVendas.Api.Configuration
{
    public static class DataBaseConfiguration
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}