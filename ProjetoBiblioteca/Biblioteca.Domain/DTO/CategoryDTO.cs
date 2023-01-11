using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string TipoCategoria { get; set; }
        public ICollection<Book> Livros { get; set; }
    }
}
