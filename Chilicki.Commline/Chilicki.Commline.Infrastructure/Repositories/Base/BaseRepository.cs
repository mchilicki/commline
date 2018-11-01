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
        internal CommlineDBContext _database;
        internal DbSet<TEntity> _entities;

        public BaseRepository(CommlineDBContext database)
        {
            _database = database;
            _entities = database.Set<TEntity>();
        }

        public virtual int GetCount()
        {
            return _entities.Count();
        }

        public virtual TEntity GetById(long id)
        {
            return _entities.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _entities;
        }

        public virtual void Insert(TEntity entity)
        {
            _entities.Add(entity);
            _database.SaveChanges();
        }     
        
        public virtual void Update(TEntity entity)
        {
            if (!_entities.Contains(entity))
                throw new InvalidOperationException(DatabaseResources.Exception_EntityDoesntExist);
            _database.SaveChanges();
        }
    }
}
