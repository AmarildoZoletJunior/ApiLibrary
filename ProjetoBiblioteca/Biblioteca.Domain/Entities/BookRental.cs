using Biblioteca.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Entities
{
    public class BookRental : BaseEntity
    {
        public DateTime DataSaida { get; set; }
        public DateTime DataVolta { get; set; }
        public decimal ValorAluguel { get; set; }
        public Book Livro { get; set; }
        public int LivroId { get; set; }
        public Client Cliente { get; set; }
        public int ClienteId { get; set; }
    }
}
