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
    public class ValidationExist : IValidationExist
    {
        private readonly ClassContext _context;
        public ValidationExist(ClassContext context)
        {
            _context = context;
        }
        public bool AuthorExist(int id)
        {
            var author = _context.Autores.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (author != null)
                return true;
            return false;
        }

        public bool BookExists(int BookId)
        {
            var book = _context.Books.AsNoTracking().FirstOrDefault(x => x.Id == BookId);
            if (book != null)
                return true;
            return false;
        }

        public bool BookRentalExist(int id)
        {
            var bookRental = _context.BooksRents.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (bookRental != null)
                return true;
            return false;
        }

        public bool CategoryExist(int id)
        {
            var category = _context.Categories.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (category != null)
                return true;
            return false;
        }

        public bool ClientExist(int id)
        {
            var client = _context.Clients.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (client != null)
                return true;
            return false;
        }
        public bool GetStock(int ISBN)
        {
            var stock = _context.Estoque.Include(x => x.Livro).AsNoTracking().FirstOrDefault(x => x.Livro.ISBN == ISBN);
            if(stock != null)
            {
                return true;
            }
            return false;
        }
    }
}
