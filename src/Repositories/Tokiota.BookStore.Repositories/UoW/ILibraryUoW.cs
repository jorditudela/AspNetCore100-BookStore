
namespace Tokiota.BookStore.Repositories.UoW
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Entities;
    using NewCore;

    public interface ILibraryUoW : IUoW
    {
        IGenericRespository<Author, Guid> AuthorRepository { get; }
        IGenericRespository<Book, Guid> BookRepository { get; }
        IGenericRespository<Serie, Guid> SerieRepository { get; }
        bool CanSave { get; set; }
    }
}
