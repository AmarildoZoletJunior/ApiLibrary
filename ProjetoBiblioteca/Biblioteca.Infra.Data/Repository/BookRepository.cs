using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Pagination;
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

        public async void DeleteBookAsync(int id)
        {
            var book = await GetBook(id);
            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        public async Task<Book> GetBook(int id)
        {
            return await _context.Books.Include(X => X.Autor).Include(X => X.Categoria).AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public IEnumerable<Book> GetBooks(PageParameters parametros)
        {
            return _context.Books.Include(X => X.Autor).OrderBy(x => x.Nome).Skip((parametros.PageNumber - 1) * parametros.PageSize).Take(parametros.PageSize).ToList();
        }

        public void UpdateBook(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }

    }
}
