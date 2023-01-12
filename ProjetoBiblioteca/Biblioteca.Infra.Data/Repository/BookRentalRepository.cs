using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Repository;
using Biblioteca.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infra.Data.Repository
{
    public class BookRentalRepository : IBookRentalRepository
    {

        private readonly ClassContext _context;
        public BookRentalRepository(ClassContext context)
        {
            _context = context;
        }
        public void AddRental(BookRental book)
        {
            _context.BooksRents.Add(book);
            _context.SaveChanges();
        }

        public bool ClientExists(int id)
        {
            var ClientExist = _context.Clients.FirstOrDefault(x => x.Id == id);
            if(ClientExist == null)
            {
                return false;
            }
            return true;
        }

        public void DeleteRental(int id)
        {
            var deletar = _context.BooksRents.FirstOrDefault(x => x.Id == id);
            _context.BooksRents.Remove(deletar);
            _context.SaveChanges();
        }

        public bool GetClientBookRental(int id)
        {
            var RentalExist = _context.BooksRents.FirstOrDefault(x => x.ClienteId == id);
            if (RentalExist == null)
            {
                return false;
            }
            return true;
        }

        public BookRental GetRental(int id)
        {
            return _context.BooksRents.FirstOrDefault(x => x.ClienteId == id);
        }

        public IEnumerable<BookRental> GetRents()
        {
            return _context.BooksRents.ToList();
        }

        public void UpdateRental(BookRental book)
        {
            _context.BooksRents.Update(book);
            _context.SaveChanges();
        }
    }
}
