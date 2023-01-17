using Biblioteca.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Entities
{
    public class Stock : BaseEntity
    {
        public int QuantidadeTotal { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public Book Livro { get; set; }
        public int IdLivro { get; set; }
    }
}
