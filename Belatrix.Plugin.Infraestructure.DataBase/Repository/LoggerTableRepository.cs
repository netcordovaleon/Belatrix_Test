using Belatrix.Plugin.Infraestructure.DataBase.Entities;
using Belatrix.Plugin.Infraestructure.DataBase.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belatrix.Plugin.Infraestructure.DataBase.Repository
{

    public interface ILoggerTableRepository : IRepository<LoggerTable>
    {
    }

    public class LoggerTableRepository : Repository<LoggerTable>, ILoggerTableRepository
    {
        private readonly IUnitOfWork _uow;

        public LoggerTableRepository(IUnitOfWork uow) : base(uow)
        {
            this._uow = uow;
        }
    }
}
