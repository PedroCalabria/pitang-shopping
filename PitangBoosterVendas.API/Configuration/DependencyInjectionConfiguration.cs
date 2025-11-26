using PitangBoosterVendas.API.Middleware;
using PitangBoosterVendas.Business.Imp.Business;
using PitangBoosterVendas.Repository;
using PitangBoosterVendas.Repository.Imp.Repositories;
using PitangBoosterVendas.Repository.Imp.TransactionManager;
using System.Reflection;

namespace PitangBoosterVendas.Api.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            InjectMiddlewares(services);

            RegisterGeneric(services, typeof(PagamentoBusiness).Assembly, "Business");
            RegisterGeneric(services, typeof(PagamentoRepository).Assembly, "Repository");

            services.AddScoped<ITransactionManager, TransactionManager>();
        }

        private static void InjectMiddlewares(IServiceCollection services)
        {
            services.AddTransient<ApiMiddleware>();
        }

        private static void RegisterGeneric(IServiceCollection services, Assembly assembly, string sufixo)
        {
            var implementacoes = assembly.GetTypes()
                                .Where(t => t.IsClass && t.Name.EndsWith(sufixo)); 

            foreach (var implementacao in implementacoes)
            {
                var interfaceType = Array.Find(implementacao.GetInterfaces(), i => i.Name.EndsWith(implementacao.Name));

                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, implementacao);
                }
            }
        }
    }
}