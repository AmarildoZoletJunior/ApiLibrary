using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Repository
{
    public interface IValidationExist
    {
        public bool ClientExist(int id);
        public bool BookRentalExist(int id);
        public bool BookExists(int ISBN);
        public bool CategoryExist(int id);
        public bool AuthorExist(int id);
        public bool GetStock(int ISBN);
    }
}
