using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.DTO.Request
{
    public class StockRequest
    {
        public int QuantidadeTotal { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public int ISBN { get; set; }

    }
}
