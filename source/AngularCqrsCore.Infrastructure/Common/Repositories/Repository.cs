using System.Collections.Generic;
using System.Linq;
using Application.Common.Interfaces;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Common.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AngularCoreContext _dbContext;

        protected IQueryable<TEntity> Entities => _dbContext.Set<TEntity>().AsQueryable();


        public Repository(AngularCoreContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
        }

        public void AddRangeBulk(IList<TEntity> entities)
        {
            _dbContext.BulkInsert(entities, new BulkConfig() { SetOutputIdentity = true, BatchSize = 10000 });
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void DeleteBulk(IList<TEntity> entities)
        {
            _dbContext.BulkDelete(entities);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            if (_dbContext.Set<TEntity>().Local.Any(e => e == entity))
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                return;
            }

            _dbContext.Update(entity);
        }

        public void UpdateBulk(IList<TEntity> entities)
        {
            _dbContext.BulkUpdate(entities, new BulkConfig() { SetOutputIdentity = true, BatchSize = 10000 });
        }

        

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbContext.UpdateRange(entities);
        }
        
    }
}
