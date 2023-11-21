using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Portolo.Framework.Data
{
    public interface IGenericRepository<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void Delete(TEntity entity);

        void Edit(TEntity entity);

        void Save();

        IQueryable<TEntity> GetWithRwSql(string query, params object[] parameters);

        int ExecuteSqlCommand(string sqlStatement, params object[] parameters);

        IQueryable<TObject> GetAll<TObject>()
            where TObject : class;

        IQueryable<TObject> FindBy<TObject>(Expression<Func<TObject, bool>> predicate)
            where TObject : class;

        void Add<TObject>(TObject entity)
            where TObject : class;

        void Delete<TObject>(TObject entity)
            where TObject : class;

        void Edit<TObject>(TObject entity)
            where TObject : class;

        IQueryable<TObject> ExecuteSqlCommand<TObject>(string procedureName, params object[] parameters)
            where TObject : class;
    }
}