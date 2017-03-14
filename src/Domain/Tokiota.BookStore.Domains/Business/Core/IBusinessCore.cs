
namespace Tokiota.BookStore.Domains.Business.Core
{
    using Entities.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public interface IBusinessCore<TEntity, TId> where TEntity : EntityCore<TId>
    {
        void Add(TEntity entity);
        List<TEntity> Get();
        TEntity Get(TId id);
        void Remove(TId id);
        void Remove(TEntity entity);
        void Update(TId id, TEntity entity);
    }
}
