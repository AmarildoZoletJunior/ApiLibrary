using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Repository
{
    public interface IBookRepository
    {
        public IEnumerable<Book> GetBooks();
        public void AddBook(Book book);
        public void DeleteBook(int id);
        public void UpdateBook(Book book);
        public Book GetBook(int id);

    }
}
