using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void AddRangeBulk(IList<TEntity> entities);


        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        void DeleteBulk(IList<TEntity> entities);


        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void UpdateBulk(IList<TEntity> entities);
    }
}
