using Chilicki.Commline.Infrastructure.Databases;
using Chilicki.Commline.Infrastructure.Resources;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Chilicki.Commline.Infrastructure.Repositories.Base
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        internal CommlineDBContext _db;
        internal DbSet<TEntity> _dbSet;

        public BaseRepository(CommlineDBContext db)
        {
            _db = db;
            _dbSet = db.Set<TEntity>();
        }

        public virtual int GetCount()
        {
            return _dbSet.Count();
        }

        public virtual TEntity GetById(long id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            _db.SaveChanges();
        }     
        
        public virtual void Update(TEntity entity)
        {
            if (!_dbSet.Contains(entity))
                throw new InvalidOperationException(DatabaseResources.Exception_EntityDoesntExist);
            _db.SaveChanges();
        }
    }
}
