using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Repository;
using Biblioteca.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infra.Data.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ClassContext _context;
        public BookRepository(ClassContext context)
        {
            _context = context;
        }
        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void DeleteBook(int id)
        {
            var book = GetBook(id);
            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        public Book GetBook(int id)
        {
            return _context.Books.Include(X => X.Autor).Include(X => X.Categoria).AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Book> GetBooks()
        {
            return _context.Books.ToList();
        }

        public void UpdateBook(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }
    }
}
