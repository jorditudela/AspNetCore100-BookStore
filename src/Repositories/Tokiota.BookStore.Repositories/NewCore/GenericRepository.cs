
namespace Tokiota.BookStore.Repositories.NewCore
{
    using Context;
    using Entities.Core;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Tokiota.BookStore.Entities;
    using static XCutting.Enums.Repository;

    public class GenericRepository<TEntity, TId> : IGenericRespository<TEntity, TId> where TEntity : EntityCore<TId>
    {
        protected LibraryContext Context { get; private set; }
        protected DbSet<TEntity> Set { get; private set; }

        #region Constructor
        public GenericRepository(LibraryContext context)
        {
            this.Context = context;
            this.Set = this.Context.Set<TEntity>();
        }
        #endregion

        public Response.Status Create(TEntity entity)
        {
            this.Set.Add(entity);
            return Response.Status.Ok;
        }

        public Response.Status Remove(TEntity entity)
        {
            this.Set.Remove(entity);
            return Response.Status.Ok;
        }

        public Response.Status Remove(TId id)
        {
            this.Remove(this.Get(id));
            return Response.Status.Ok;
        }

        public TEntity Get(TId id)
        {
            return this.Set.FirstOrDefault(e => e.Id.Equals(id));
        }

        public IEnumerable<TEntity> Get()
        {
            return this.Set;
        }

        public Response.Status Update(TEntity entity)
        {
            this.Set.Update(entity);
            return Response.Status.Ok;
        }
    }
}
