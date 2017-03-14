using System;
using System.Collections.Generic;
using System.Linq;

namespace Tokiota.BookStore.Repositories.UoW
{
    using System.Threading.Tasks;
    using Context;
    using Entities;
    using NewCore;
    public class LibraryUoW : ILibraryUoW
    {
        private readonly LibraryContext _context;
        public bool CanSave { get; set; } = true;

        public LibraryUoW(LibraryContext context, 
            IGenericRespository<Author, Guid> authorRepo, 
            IGenericRespository<Book, Guid> bookRepo,
            IGenericRespository<Serie, Guid> serieRepo)
        {
            this._context = context;
            this.AuthorRepository = authorRepo;
            this.BookRepository = bookRepo;
            this.SerieRepository = serieRepo;
        }
        public IGenericRespository<Author, Guid> AuthorRepository
        {
            get; private set;
        }

        public IGenericRespository<Book, Guid> BookRepository
        {
            get; private set;
        }

        public IGenericRespository<Serie, Guid> SerieRepository
        {
            get; private set;
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
