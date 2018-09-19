using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Chilicki.Commline.Infrastructure.Repositories.Base
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        internal DbContext _db;
        internal DbSet<TEntity> _dbSet;

        public BaseRepository(DbContext db)
        {
            _db = db;
            _dbSet = db.Set<TEntity>();
        }

        public virtual TEntity GetById(long id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            _db.SaveChanges();
        }     
        
        public virtual void Update(TEntity entity)
        {
            _db.SaveChanges();
        }
    }
}
