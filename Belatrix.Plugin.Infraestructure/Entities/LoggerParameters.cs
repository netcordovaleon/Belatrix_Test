using Belatrix.Plugin.Infraestructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belatrix.Plugin.Infraestructure.Entities
{
    public class LoggerParameters : MarshalByRefObject
    {
        public string message { get; set; }
        public LoggerType.TypeLog type { get; set; }

        public LoggerParameters() { 
        }

        public LoggerParameters(string _message, LoggerType.TypeLog _type)
        {
            this.message = _message;
            this.type = _type;
        }
    }
}
