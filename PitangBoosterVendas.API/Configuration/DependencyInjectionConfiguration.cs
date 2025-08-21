using PitangBoosterVendas.Business.IBusiness;
using PitangBoosterVendas.Business.Imp.Business;
using PitangBoosterVendas.Repository.Imp.Repositories;
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

            //services.AddScoped<ITransactionManager, TransactionManager>();
            //services.AddScoped<IPatientContext, PatientContext>();
            //services.AddOptions<AuthenticationConfig>().Bind(configuration.GetSection("Authorization"));
        }

        private static void InjectMiddlewares(IServiceCollection services)
        {
            //services.AddTransient<ApiMiddleware>();
            //services.AddTransient<PatientContextMiddleware>();
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