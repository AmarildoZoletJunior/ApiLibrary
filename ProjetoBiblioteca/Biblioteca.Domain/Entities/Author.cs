using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Domain.Entities.Base;

namespace Biblioteca.Domain.Entities
{
    public class Author : BaseEntity
    {
        public string Nome { get; set; }

        public ICollection<Book>? Livros { get; set; }

    }
}
