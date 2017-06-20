using Belatrix.Plugin.Contract.Logger;
using Belatrix.Plugin.Infraestructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.IO;
using System.Linq;

namespace Belatrix.Plugin.AppDomain.Logger
{
    public class ConfigureAppDomain : MarshalByRefObject
    {

        private CompositionContainer container;
        private DirectoryCatalog directoryCatalogLogger;
        private IEnumerable<ILogger> loggerExports;
        private LoggerParameters appDomainLoggerParameters;
        
        private static readonly string pluginPath = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");

        public void DoWorkInShadowCopiedDomain()
        {
            var regBuilder = new RegistrationBuilder();
            regBuilder.ForTypesDerivedFrom<ILogger>().Export<ILogger>();

            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ConfigureAppDomain).Assembly, regBuilder));

            if (directoryCatalogLogger == null)
            {
                directoryCatalogLogger = new DirectoryCatalog(pluginPath, regBuilder);
                catalog.Catalogs.Add(directoryCatalogLogger);
                container = new CompositionContainer(catalog);
                container.ComposeExportedValue(container);
                loggerExports = container.GetExportedValues<ILogger>();
            }
            else {
                RefreshCatalog();
            }
        }

        public void RefreshCatalog()
        {
            directoryCatalogLogger.Refresh();
            container.ComposeParts(directoryCatalogLogger.Parts);
            loggerExports = container.GetExportedValues<ILogger>();
        }


        public void ExecuteLogger(LoggerParameters logger)
        {
            loggerExports.ToList().ForEach(e =>
            {
                e.saveLogger(appDomainLoggerParameters);
            });
        }


        public LoggerParameters InitializeParameter(int type, string message)
        {

            Infraestructure.Enum.LoggerType.TypeLog typeLogger;

            if (type == Infraestructure.Enum.LoggerType.TypeLog.Success.GetHashCode())
                typeLogger = Infraestructure.Enum.LoggerType.TypeLog.Success;
            else if (type == Infraestructure.Enum.LoggerType.TypeLog.Warning.GetHashCode())
                typeLogger = Infraestructure.Enum.LoggerType.TypeLog.Warning;
            else
                typeLogger = Infraestructure.Enum.LoggerType.TypeLog.Error;

            if (System.AppDomain.CurrentDomain.FriendlyName != "Belatrix_AppDomain") return null;
            appDomainLoggerParameters = new LoggerParameters { type = typeLogger, message = message };
            return appDomainLoggerParameters;
         
        }

    }
}
