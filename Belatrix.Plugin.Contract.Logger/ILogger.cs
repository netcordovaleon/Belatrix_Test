using Belatrix.Plugin.Infraestructure.Entities;
using Belatrix.Plugin.Infraestructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belatrix.Plugin.Contract.Logger
{
    public interface ILogger
    {
        /// <summary>
        /// Optional, description of logger implementation
        /// </summary>
        string loggerDescription { get;  }

        /// <summary>
        /// save a logger in a data base, file or show in console
        /// </summary>
        /// <param name="type">Type of logger (1 = Success, 2 = Warning, 3 = Error)</param>
        /// <param name="message">Message description of loger</param>
        void saveLogger(LoggerParameters logger);
    }
}
