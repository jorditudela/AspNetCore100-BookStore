
namespace Tokiota.BookStore.Repositories.NewCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Tokiota.BookStore.Entities;
    interface ISerieRepository
    {
        IEnumerable<Serie> GetList();

        Serie GetById(Guid Id);

        Guid Create(Serie serie);

        bool Delete(Guid id);

        bool Update(Guid id);
    }
}
