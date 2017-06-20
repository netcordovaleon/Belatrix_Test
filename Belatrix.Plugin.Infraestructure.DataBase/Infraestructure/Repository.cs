using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;


namespace Belatrix.Plugin.Infraestructure.DataBase.Infraestructure
{
    public interface IRepository<T> where T : class
    {
        T GetById(object primaryKey);
        IEnumerable<T> GetAll();
        bool Exists(object primaryKey);
        T Add(T entity);
        T Update(T entity);
        void Delete(object id);
        T Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        IEnumerable<T> GetMany(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        IUnitOfWork UnitOfWork { get; }
        void UpdatePartial(dynamic entity, params Expression<Func<T, object>>[] property);
        void UpdatePartial(dynamic entity, string[] properties);
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        internal DbSet<T> dbSet;
        internal DbContext Database { get { return _unitOfWork.Db; } }

        public IUnitOfWork UnitOfWork { get { return _unitOfWork; } }
        private readonly IUnitOfWork _unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            _unitOfWork = unitOfWork;
            this.dbSet = _unitOfWork.Db.Set<T>();
        }

        public T GetById(object primaryKey)
        {
            var dbResult = dbSet.Find(primaryKey);
            return dbResult;
        }

        public bool Exists(object primaryKey)
        {
            return dbSet.Find(primaryKey) == null ? false : true;
        }

        public virtual T Add(T entity)
        {
            dynamic obj = dbSet.Add(entity);
            this._unitOfWork.Db.SaveChanges();
            return obj;
        }

        public virtual T Update(T entity)
        {
            dbSet.Attach(entity);
            _unitOfWork.Db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            this._unitOfWork.Db.SaveChanges();
            return entity;
        }

        public void Delete(object id)
        {
            T entity = dbSet.Find(id);
            dbSet.Attach(entity);
            _unitOfWork.Db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            this._unitOfWork.Db.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            Filter(ref query);
            return query.ToList();
        }


        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = dbSet;
            Filter(ref query, filter);
            return orderBy != null ? orderBy(query).SingleOrDefault() : query.SingleOrDefault();
        }

        public IEnumerable<T> GetMany(System.Linq.Expressions.Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = dbSet;
            Filter(ref query, filter);
            foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        private static void Filter<T>(ref IQueryable<T> query, Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                query = query.Where(filter);
            }
        }

        public void UpdatePartial(dynamic entity, Expression<Func<T, object>>[] properties)
        {
            var entry = _unitOfWork.Db.Entry(entity);
            _unitOfWork.Db.Set<T>().Attach(entity);

            foreach (var item in properties)
            {
                entry.Property(item).IsModified = true;
            }
        }

        public void UpdatePartial(dynamic entity, string[] properties)
        {
            var entry = _unitOfWork.Db.Entry(entity);
            _unitOfWork.Db.Set<T>().Attach(entity);
            foreach (var name in properties)
            {
                entry.Property(name).IsModified = true;
            }
        }
    }
}
