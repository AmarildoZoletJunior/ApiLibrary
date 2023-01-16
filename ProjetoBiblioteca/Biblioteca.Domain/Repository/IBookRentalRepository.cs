using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Repository
{
    public interface IBookRentalRepository
    {
        public IEnumerable<BookRental> GetRents(PageParameters parametros);
        public void AddRental(BookRental book);
        public void DeleteRental(int id);
        public void UpdateRental(BookRental book);
        public Task<BookRental> GetRental(int id);
        public bool GetClientBookRental(int id);
        public decimal ExistValueClient(int id);
    }
}
