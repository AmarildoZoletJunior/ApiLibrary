using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Repository
{
    public interface IBookRentalRepository
    {
        public IEnumerable<BookRental> GetRents();
        public void AddRental(BookRental book);
        public void DeleteRental(int id);
        public void UpdateRental(BookRental book);
        public BookRental GetRental(int id);
        public bool GetClientBookRental(int id);
    }
}
