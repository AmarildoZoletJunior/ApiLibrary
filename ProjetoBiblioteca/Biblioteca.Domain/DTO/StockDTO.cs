using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.DTO
{
    public class StockDTO
    {
        public int QuantidadeTotal { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public int IdLivro { get; set; }
    }
}
