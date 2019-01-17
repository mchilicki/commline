using Chilicki.Commline.Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Commline.Infrastructure.Repositories.Base
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        internal CommlineDBContext _database;
        internal DbSet<TEntity> _entities;

        public BaseRepository(CommlineDBContext database)
        {
            _database = database;
            _entities = database.Set<TEntity>();
        }

        public virtual int Count()
        {
            return _entities.Count();
        }

        public virtual TEntity Find(Guid id)
        {
            return _entities.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _entities;
        }

        public virtual void Add(TEntity entity)
        {
            _entities.Add(entity);
            _database.SaveChanges();
        }     
        
        public virtual void Update(TEntity entity)
        {
            _database.SaveChanges();
        }

        public virtual void Remove(TEntity entity)
        {
            _entities.Remove(entity);
            _database.SaveChanges();
        }
    }
}
