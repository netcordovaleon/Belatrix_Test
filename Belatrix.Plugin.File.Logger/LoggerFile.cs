using Belatrix.Plugin.Contract.Logger;
using Belatrix.Plugin.Infraestructure.Nlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belatrix.Plugin.File.Logger
{
    public class LoggerFile : MarshalByRefObject, ILogger
    {

        public NlogConfiguration configurationLog;

        public LoggerFile() {
            configurationLog = new NlogConfiguration();
        }

        public string loggerDescription
        {
            get { return "\nLogger execute from FILE Implementation"; }
        }

        public void saveLogger(Infraestructure.Entities.LoggerParameters logger)
        {
            if (logger.type == Infraestructure.Enum.LoggerType.TypeLog.Warning)
                configurationLog.Warn(logger.message);
            else if (logger.type == Infraestructure.Enum.LoggerType.TypeLog.Success)
                configurationLog.Info(logger.message);
            else if (logger.type == Infraestructure.Enum.LoggerType.TypeLog.Error)
                configurationLog.Error(logger.message);
            else
                throw new NotImplementedException();
            System.Console.WriteLine(this.loggerDescription);
        }
    }
}
