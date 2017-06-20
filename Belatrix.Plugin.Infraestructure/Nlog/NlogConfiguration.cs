using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
namespace Belatrix.Plugin.Infraestructure.Nlog
{
    public class NlogConfiguration
    {
        private static Logger _logger;

        public NlogConfiguration() {
            _logger = LogManager.GetLogger("TaskBelatrixLogger");
        }

             
        public void Debug(string message)
        {
            if (!_logger.IsDebugEnabled) return;
            _logger.Debug(message);
        }
 
        public void Info(string message)
        {
            if (!_logger.IsInfoEnabled) return;
            _logger.Info(message);
        }

 
        public void Warn(string message)
        {
            if (!_logger.IsWarnEnabled) return;
            _logger.Warn(message);
        }

    
        public void Error(string error, Exception exception = null)
        {
            if (!_logger.IsErrorEnabled) return;
            _logger.Error(error, exception);
        }

     
        public void Fatal(string message)
        {
            if (!_logger.IsFatalEnabled) return;
            _logger.Warn(message);
        }

      
        public void Trace(string message)
        {
            if (!_logger.IsTraceEnabled) return;
            _logger.Trace(message);
        }

        public void Trace(string message, params object[] args)
        {
            if (!_logger.IsTraceEnabled) return;
            _logger.Trace(message, args);
        }
    }
}
