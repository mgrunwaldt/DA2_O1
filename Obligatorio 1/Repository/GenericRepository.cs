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
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal DbSet<TEntity> dbSet;
        internal MyContext context;

        public GenericRepository(bool forTest = false)
        {
            if (forTest)
            {
                EmptyDatabase();
            }
        }



        public virtual List<TEntity> GetAll()
        {
            using (var newContext = new MyContext())
            {
                this.dbSet = newContext.Set<TEntity>();
                var local = dbSet.Local;
                var list = local.ToList();
                return list;
            }

            
        }

        public virtual TEntity Get(object id)
        {
            using (var newContext = new MyContext())
            {
                this.dbSet = newContext.Set<TEntity>();
                return dbSet.Find(id);
            }
            
        }

        public virtual void Add(TEntity entity)
        {
            using (var newContext = new MyContext())
            {
                this.dbSet = newContext.Set<TEntity>();
                dbSet.Add(entity);
    
            }


        }

        public void EmptyDatabase()
        {
            using (var newContext = new MyContext())
            {
                this.dbSet = newContext.Set<TEntity>();
                newContext.Empty();
            }
            
        }

        public virtual void Delete(object id)
        {
            using (var newContext = new MyContext())
            {
                this.dbSet = newContext.Set<TEntity>();
                TEntity entityToDelete = dbSet.Find(id);
                Delete(entityToDelete);
            }
            
            
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            using (var newContext = new MyContext())
            {
                this.dbSet = newContext.Set<TEntity>();
                if (context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
            }
            
           
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            using (var newContext = new MyContext())
            {
                this.dbSet = newContext.Set<TEntity>();
                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
            }
           
        }
    }
}