
namespace Tokiota.BookStore.Repositories.NewCore
{
    using Entities.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using static XCutting.Enums.Repository;

    public interface IGenericRespository<TEntity, TId> where TEntity : EntityCore<TId>
    {
        IEnumerable<TEntity> Get();
        TEntity Get(TId id);
        Response.Status Create(TEntity entity);
        Response.Status Update(TEntity entity);
        Response.Status Remove(TEntity entity);
        Response.Status Remove(TId id);
    }
}
