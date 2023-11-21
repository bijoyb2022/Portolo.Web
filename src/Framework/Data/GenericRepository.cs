using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Portolo.Framework.Data
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        protected internal DbSet<TEntity> DbSet;
        protected TContext dbContext;

        public GenericRepository(TContext context)
        {
            this.dbContext = context;
            this.DbSet = context.Set<TEntity>();
            this.dbContext.Database.CommandTimeout = 0;
        }

        public string DbConnection { get; set; }

        public virtual IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = this.DbSet;

            return query;
        }

        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            var query = this.DbSet.Where(predicate);

            return query;
        }

        public virtual void Add(TEntity entity)
        {
            this.DbSet.Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (this.dbContext.Entry(entity).State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            this.DbSet.Remove(entity);
        }

        public virtual void Edit(TEntity entity)
        {
            this.DbSet.Attach(entity);
            this.dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            this.dbContext.SaveChanges();
        }

        public IQueryable<TEntity> GetWithRwSql(string query, params object[] parameters) => this.DbSet.SqlQuery(query, parameters).AsQueryable();

        public int ExecuteSqlCommand(string sqlStatement, params object[] parameters) =>
            this.dbContext.Database.ExecuteSqlCommand(sqlStatement, parameters);

        public IQueryable<TObject> GetAll<TObject>()
            where TObject : class
        {
            var objectDbSet = this.dbContext.Set<TObject>();
            IQueryable<TObject> query = objectDbSet;

            return query;
        }

        public IQueryable<TObject> FindBy<TObject>(Expression<Func<TObject, bool>> predicate)
            where TObject : class
        {
            var objectDbSet = this.dbContext.Set<TObject>();
            var query = objectDbSet.Where(predicate);

            return query;
        }

        public void Add<TObject>(TObject entity)
            where TObject : class
        {
            var objectDbSet = this.dbContext.Set<TObject>();
            objectDbSet.Add(entity);
        }

        public void Delete<TObject>(TObject entity)
            where TObject : class
        {
            var objectDbSet = this.dbContext.Set<TObject>();

            if (this.dbContext.Entry(entity).State == EntityState.Detached)
            {
                objectDbSet.Attach(entity);
            }

            objectDbSet.Remove(entity);
        }

        public virtual void Edit<TObject>(TObject entity)
            where TObject : class
        {
            this.dbContext.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<TObject> ExecuteSqlCommand<TObject>(string sqlStatement, params object[] parameters)
            where TObject : class
            =>
            this.dbContext.Database.SqlQuery<TObject>(sqlStatement, parameters).AsQueryable();
    }
}