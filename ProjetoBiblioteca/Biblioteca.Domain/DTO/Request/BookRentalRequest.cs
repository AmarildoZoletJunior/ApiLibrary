using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Biblioteca.Domain.DTO.Request
{
    public class BookRentalRequest
    {
        public DateTime DataSaida { get; set; }
        public DateTime DataEstimadaVolta { get; set; }
        public decimal ValorAluguel { get; set; }
        public int LivroId { get; set; }
        public int ClienteId { get; set; }
    }
}
