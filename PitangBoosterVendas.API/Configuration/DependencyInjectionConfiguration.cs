using PitangBoosterVendas.API.Middleware;
using PitangBoosterVendas.Business.IBusiness;
using PitangBoosterVendas.Business.Imp.Business;
using PitangBoosterVendas.Repository;
using PitangBoosterVendas.Repository.Imp.Repositories;
using PitangBoosterVendas.Repository.Imp.TransactionManager;
using PitangBoosterVendas.Repository.IRepository;

namespace PitangBoosterVendas.Api.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            InjectRepositories(services);
            InjectBusinesses(services);
            InjectMiddlewares(services);

            services.AddScoped<ITransactionManager, TransactionManager>();
        }

        private static void InjectMiddlewares(IServiceCollection services)
        {
            services.AddTransient<ApiMiddleware>();
        }

        private static void InjectRepositories(IServiceCollection services)
        {
            services.AddScoped<IPedidoRepository, PedidoRepository>();            
            services.AddScoped<IProdutoRepository, ProdutoRepository>();            
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();            
        }

        private static void InjectBusinesses(IServiceCollection services)
        {
            services.AddScoped<IPedidoBusiness, PedidoBusiness>();
            services.AddScoped<IProdutoBusiness, ProdutoBusiness>();
            services.AddScoped<IPagamentoBusiness, PagamentoBusiness>();
        }
    }
}