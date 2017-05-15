using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Repository
{
    public class GenericRepository<TEntity>:IGenericRepository<TEntity> where TEntity : class
    {
        internal DbSet<TEntity> dbSet;
        internal MyContext context;

        public MyContext GetContext()
        {
            return context;
        }

        public GenericRepository(MyContext contextParam,bool forTest = false)
        {
            this.context = contextParam;
            this.dbSet = this.context.Set<TEntity>();

            if (forTest)
            {
                EmptyDatabase();
            }
        }


        public virtual List<TEntity> GetAll()
        {
            var list = dbSet.ToList();
           return dbSet.ToList();
        }

        public virtual TEntity Get(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public void EmptyDatabase()
        {
            this.context.Empty();
        }

        public virtual bool Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            if (entityToDelete != null) {
                Delete(entityToDelete);
                this.context.SaveChanges();
                return true;
            }
            else
            {
                return false; 
            }
            
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            this.context.SaveChanges();                   
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            this.context.SaveChanges();
        }
    }
}