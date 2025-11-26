using log4net;
using log4net.Config;
using Microsoft.AspNetCore;
using PitangBoosterVendas.Utils.Resources;
using System.Reflection;

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

                var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _log.Info(string.Format(InfraMessages.InitializingApplication, currentDate));
                var webHost = WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();

                webHost.Build().Run();
            }
            catch (Exception ex)
            {
                var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _log.Info(string.Format(InfraMessages.FatalError, currentDate));
                throw;
            }
        }

        
    }
}