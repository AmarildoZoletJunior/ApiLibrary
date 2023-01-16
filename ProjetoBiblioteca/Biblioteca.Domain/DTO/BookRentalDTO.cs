using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.DTO
{
    public class BookRentalDTO
    {
        public int Id { get; set; }
        public DateTime DataSaida { get; set; }
        public DateTime DataEstimadaVolta { get; set; }
        public decimal ValorAluguel { get; set; }
        public ClientDTO Cliente { get; set; }
        public BookDTO Livro { get; set; }

    }
}
