
namespace Tokiota.BookStore.Domains.Business.Core
{
    using Entities.Core;
    using Repositories.NewCore;
    using Repositories.UoW;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class BusinessCore<TEntity, TId> : IBusinessCore<TEntity, TId> where TEntity : EntityCore<TId>
    {
        protected readonly GenericRepository<TEntity, TId> _repository;
        protected readonly ILibraryUoW _uow;

        public BusinessCore(ILibraryUoW uow, GenericRepository<TEntity, TId> repository)
        {
            _uow = uow;
            _repository = repository;
        }
        public virtual void Add(TEntity entity)
        {
            this._repository.Create(entity);
            this._uow.SaveChanges();
        }

        public virtual List<TEntity> Get()
        {
            return this._repository.Get().ToList();
        }

        public virtual TEntity Get(TId id)
        {
            return this._repository.Get(id);
        }

        public virtual void Remove(TEntity entity)
        {
            this._repository.Remove(entity);
            this._uow.SaveChanges();
        }

        public virtual void Remove(TId id)
        {
            this._repository.Remove(id);
            this._uow.SaveChanges();
        }

        public virtual void Update(TId id, TEntity entity)
        {
            entity.Id = id;
            this._repository.Update(entity);
            this._uow.SaveChanges();
        }
    }
}
