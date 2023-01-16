using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Repository
{
    public interface IBookRepository
    {
        public IEnumerable<Book> GetBooks(PageParameters parametros);
        public void AddBook(Book book);
        public void DeleteBookAsync(int id);
        public void UpdateBook(Book book);
        public Task<Book> GetBook(int id);

    }
}
