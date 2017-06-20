using Belatrix.Plugin.Contract.Logger;
using Belatrix.Plugin.Infraestructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Belatrix.Plugin.Infraestructure.DataBase.Infraestructure;
using Belatrix.Plugin.Infraestructure.DataBase.Repository;

namespace Belatrix.Plugin.DataBase.Logger
{
    public class LoggerDataBase : MarshalByRefObject, ILogger
    {
        private readonly IUnitOfWork _uow;
        private readonly ILoggerTableRepository _loggerRepository;
        public LoggerDataBase() {
            _uow = new UnitOfWork();
            _loggerRepository = new LoggerTableRepository(_uow);
        }

        public string loggerDescription
        {
            get { return "\nLogger execute from DATABASE Implementation"; }
        }

        public void saveLogger(Infraestructure.Entities.LoggerParameters logger)
        {
            _loggerRepository.Add(new Infraestructure.DataBase.Entities.LoggerTable() { 
                typeLogger = logger.type.GetHashCode(),
                messageLogger = logger.message,
                dateInsertLogger = DateTime.Now,
                userInsertLogger = "SYSTEM"
            });
            System.Console.WriteLine(this.loggerDescription);
        }
    }
}
