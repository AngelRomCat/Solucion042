using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using _05_Data;
using _05_Data.Data;

namespace _04_Persistencia.Repository.Class
{
    public class GenericRepository<T> where T : class
    {
        public NorthWindTuneadoDbContext Db { get; set; }

        public GenericRepository(NorthWindTuneadoDbContext context)
        {
            Db = context;
        }

        public virtual IList<T> GetAll()
        {
            IQueryable<T> query = Db.Set<T>();
            return query.ToList();
        }

        public IList<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            var query = Db.Set<T>().Where(predicate);
            return query.ToList();
        }

        public virtual bool Add(T entity)
        {
            Db.Set<T>().Add(entity);
            return true;
        }

        public virtual bool Exists(T entity)
        {
            return Db.Set<T>().Any();

        }
        public virtual T AddAndGet(T entity)
        {
            Db.Set<T>().Add(entity);
            return entity;
        }
        public virtual bool Delete(T entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            Db.Set<T>().Remove(entity);
            return true;
        }

        public virtual bool Edit(T entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            return true;
        }
        public virtual bool Save()
        {
            Db.SaveChanges();
            return true;
        }

        public virtual bool SaveChanges(T entity)
        {
            if (Db.Entry(entity).State == EntityState.Detached)
            {
                Db.Set<T>().Attach(entity);
            }
            Save();
            return true;
        }

        public virtual T FindById(int id)
        {
            return Db.Set<T>().Find(id);
        }

        public virtual void Attach(T entity)
        {
            Db.Set<T>().Attach(entity);
        }
    }

}