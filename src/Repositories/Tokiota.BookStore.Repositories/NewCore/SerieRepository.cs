
namespace Tokiota.BookStore.Repositories.NewCore
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class SerieRepository
    {
        public IEnumerable<Serie> GetList()
        {
            return null;
        }

        public Serie GetById(Guid Id)
        {
            return null;
        }

        public Guid Create(Serie serie)
        {
            return new Guid();
        }

        public bool Delete(Guid id)
        {
            return true;
        }

        public bool Update(Guid id)
        {
            return true;
        }

    }
}
