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
            Initialize();
            if (forTest)
            {
                EmptyDatabase();
            }
        }

        private void Initialize() {
            if (this.context == null) {
                this.context = new MyContext();
            }
            this.dbSet = this.context.Set<TEntity>();
        }



        public virtual List<TEntity> GetAll()
        {
            var list = dbSet.ToList();
           return dbSet.ToList();
         /*   var local = dbSet.Local;
            var list = local.ToList();
            return list;  */
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

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
            this.context.SaveChanges();
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);                       
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            this.context.SaveChanges();

        }
    }
}