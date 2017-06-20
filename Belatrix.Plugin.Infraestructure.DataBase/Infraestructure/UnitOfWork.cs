using Belatrix.Plugin.Infraestructure.OperationalManage;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Belatrix.Plugin.Infraestructure.DataBase.Infraestructure
{

    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void StartTransaction();
        DbContext Db { get; }
        BelatrixLoggerContext DataContext();

    }

    public class UnitOfWork : Disposable, IUnitOfWork
    {
        private readonly IDatabaseFactory _databaseFactory;
        private TransactionScope _transaction;
        private readonly BelatrixLoggerContext _db;

        public UnitOfWork()
        {
            _db = new BelatrixLoggerContext();
            this._databaseFactory = new DatabaseFactory();

        }

        public void Commit()
        {
            _db.SaveChanges();
            _transaction.Complete();
        }
 
        public void StartTransaction()
        {
            _transaction = new TransactionScope();
        }

        public DbContext Db { get { return _db; } }

        public BelatrixLoggerContext DataContext()
        {
            return _databaseFactory.Get();
        }
    }
}
