using Belatrix.Plugin.Contract.Logger;
using Belatrix.Plugin.Infraestructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belatrix.Plugin.Console.Logger
{
    public class LoggerConsole : MarshalByRefObject, ILogger
    {

        public string loggerDescription
        {
            get { return "\nLogger execute from CONSOLE Implementation"; }
        }

 
        public void saveLogger(Infraestructure.Entities.LoggerParameters logger)
        {
            System.Console.WriteLine(string.Format("OPTION: {0}", logger.type.ToString()));
            System.Console.WriteLine(string.Format("MESSAGE: {0}", logger.message));
            System.Console.WriteLine(this.loggerDescription);
        }
    }
}
