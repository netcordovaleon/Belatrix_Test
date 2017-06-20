using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belatrix.Plugin.Infraestructure.Enum
{
    public class LoggerType
    {

        public static string loggerTypeName(TypeLog type) {
            if (type == TypeLog.Warning)
                return "Warning";
            else if (type == TypeLog.Error)
                return "Error";
            else if (type == TypeLog.Success)
                return "Success";
            else
                throw new NotImplementedException();
        }

        public enum TypeLog {
            [Description("Success Response")]
            Success = 1,
            [Description("Warning Response")]
            Warning = 2,
            [Description("Error Response")]
            Error = 3
        }
    }
}
