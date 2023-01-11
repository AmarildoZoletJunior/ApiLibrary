using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<BookDTO>? Livros { get; set; }

    }
}
