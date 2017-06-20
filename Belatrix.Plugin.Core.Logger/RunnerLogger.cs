using System;
using System.IO;
using Belatrix.Plugin.AppDomain.Logger;

namespace Belatrix.Plugin.Core.Logger
{
    public class RunnerLogger
    {
        private static System.AppDomain domain;

        private static void OptionsLogger() {
            Console.Clear();
            Console.WriteLine("*OPTIONS FOR LOGGER*");
            Console.WriteLine("[1] Success");
            Console.WriteLine("[2] Warning");
            Console.WriteLine("[3] Error");
        }

        [STAThread]
        public static void Executed()
        {
            var cachePath = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "ShadowCopyCache");
            var pluginPath = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");

            if (!Directory.Exists(cachePath))
                Directory.CreateDirectory(cachePath);

            if (!Directory.Exists(pluginPath))
                Directory.CreateDirectory(pluginPath);

            var setup = new AppDomainSetup
            {
                CachePath = cachePath,
                ShadowCopyFiles = "true",
                ShadowCopyDirectories = pluginPath
            };

            domain = System.AppDomain.CreateDomain("Belatrix_AppDomain", System.AppDomain.CurrentDomain.Evidence, setup);
            var runnerLoggerConfiguration = (ConfigureAppDomain)domain.CreateInstanceAndUnwrap(typeof(ConfigureAppDomain).Assembly.FullName, typeof(ConfigureAppDomain).FullName);

            OptionsLogger();

            while (true) 
            {
                Console.WriteLine("\nPlease choose an option:");
                var typeLogger = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Please write a message:");
                var messageLogger = Console.ReadLine();
                Console.WriteLine("\n*RESULTADO*\n");
                runnerLoggerConfiguration.DoWorkInShadowCopiedDomain();
                var loggerParameters = runnerLoggerConfiguration.InitializeParameter(typeLogger, messageLogger);
                if (loggerParameters == null) break;
                runnerLoggerConfiguration.ExecuteLogger(loggerParameters);
                Console.WriteLine("\n*COMPLETE*\n");
                Console.WriteLine("Do you want continue: S/N");
                var flgContinue = Console.ReadLine().ToUpper();
                if (flgContinue == "N") break;
                else 
                    OptionsLogger();
            }
        }
    }
}
