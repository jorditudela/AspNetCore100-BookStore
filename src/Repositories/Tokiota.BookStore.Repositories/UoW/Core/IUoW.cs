using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tokiota.BookStore.Repositories.UoW.Core
{
    public interface IUoW
    {
        int SaveChanges();
    }
}
