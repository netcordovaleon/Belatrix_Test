using System;
using System.Data;
using System.Data.SqlClient;
using Belatrix.Plugin.Console.Logger;
using Belatrix.Plugin.Contract.Logger;
using Belatrix.Plugin.Infraestructure.Entities;
using System.IO;
using NUnit.Framework;
using System.Configuration;
using System.Runtime.InteropServices;
using Belatrix.Plugin.DataBase.Logger;
using Belatrix.Plugin.Infraestructure.DataBase.Infraestructure;
using Belatrix.Plugin.Infraestructure.DataBase.Repository;
using Belatrix.Plugin.Infraestructure.DataBase.Entities;
using NLog;
namespace Belatrix.Plugin.UnitTest
{
    [TestFixture]
    public class ConfigurationTest  
    {

        private IUnitOfWork _uow;
        private ILoggerTableRepository _loggerTableRepository;
        public ConfigurationTest (){
            _uow = new UnitOfWork();
            _loggerTableRepository = new LoggerTableRepository(_uow);
        }

        [Test]
        public void ConnectionString_Is_Correct_And_Open()
        {           
            Assert.DoesNotThrow(() => {
                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BelatrixLoggerContext"].ToString();
                var connectionSQL = new SqlConnection(connectionString);
                connectionSQL.Open();            
            });
        }

        [Test]
        public void NLog_Configuration_Is_Complete()
        {
            var manage = LogManager.GetLogger("TaskBelatrixLogger");
            var info = manage.IsInfoEnabled;
            var warn = manage.IsWarnEnabled;
            var error = manage.IsErrorEnabled;
            Assert.IsTrue((info && warn && error));
        }

        [Test]
        public void Repository_Pattern_Could_Save() {
            Assert.DoesNotThrow(() =>
            {
                _loggerTableRepository.Add(new LoggerTable
                {
                    messageLogger = "Reposity Pattern",
                    typeLogger = 1,
                    dateInsertLogger = System.DateTime.Now,
                    userInsertLogger = "TEST"
                });
            });
        }



    }
}
