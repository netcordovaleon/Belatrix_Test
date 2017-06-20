using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Belatrix.Plugin.Infraestructure.Entities;
using Belatrix.Plugin.Infraestructure.DataBase;
using Belatrix.Plugin.DataBase.Logger;
using Belatrix.Plugin.Infraestructure.DataBase.Repository;
using Belatrix.Plugin.Infraestructure.DataBase.Infraestructure;
using NUnit.Framework;
using Belatrix.Plugin.Contract.Logger;
using Belatrix.Plugin.File.Logger;

using NLog;
using NLog.Targets;
using System.Diagnostics;
using System.IO;

namespace Belatrix.Plugin.UnitTest
{
    [TestFixture]
    public class FunctionalTest
    {

        private Belatrix.Plugin.Contract.Logger.ILogger _databaseLoggerImplement;
        private Belatrix.Plugin.Contract.Logger.ILogger _fileLoggerImplement;

        private IUnitOfWork _uow;
        private ILoggerTableRepository _loggerTableRepository;

        public FunctionalTest() {
            _uow = new UnitOfWork();
            this._databaseLoggerImplement = new LoggerDataBase();
            this._fileLoggerImplement = new LoggerFile();
            this._loggerTableRepository = new LoggerTableRepository(_uow);
        }


        private LoggerParameters parametersLogger;

        [SetUp]
        public void Initialize_Parameter_Logger_In_Db()
        {
            parametersLogger = new LoggerParameters();
            parametersLogger.type = Infraestructure.Enum.LoggerType.TypeLog.Success;
            parametersLogger.message = "test message from NUNIT";
        }

        [Test]
        public void Save_Logger_In_Database()
        {
            IUnitOfWork _uow;
            ILoggerTableRepository _loggerTableRepository;
            _uow = new UnitOfWork();
            _loggerTableRepository = new LoggerTableRepository(_uow);
            //Save logger in implementation
            _databaseLoggerImplement.saveLogger(parametersLogger);
            //Verify is logger is in database
            var listLoggerInBD = _loggerTableRepository.GetAll().ToList();
            var lastLogger = listLoggerInBD.Last();
            Assert.IsTrue((lastLogger.typeLogger == parametersLogger.type.GetHashCode() && lastLogger.messageLogger == parametersLogger.message));
        }


        [Test]
        public void If_TrySave_IncorrectType_In_File_Throw_Not_Implement()
        {
            Assert.Throws<NotImplementedException>(() => {
                _fileLoggerImplement.saveLogger(new LoggerParameters());
            });
            
        }

    }
}
