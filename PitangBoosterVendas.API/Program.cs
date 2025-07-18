using log4net.Config;
using log4net;
using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace PitangBoosterVendas.Api
{
    public static class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Program));

        public static void Main(string[] args)
        {
            try
            {
                var logRepository = LogManager.GetRepository(Assembly.GetCallingAssembly());
                XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

                //_log.Info(InfraMessages.InitializingApplication);
                var webHost = WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();

                webHost.Build().Run();
            }
            catch (Exception ex)
            {
                //_log.Fatal(InfraMessages.FatalError, ex);
                throw;
            }
        }
    }
}