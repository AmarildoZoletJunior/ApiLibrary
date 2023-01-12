using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Repository;
using System;
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
        public DateTime DataVolta { get; set; }
        public decimal ValorAluguel { get; set; }
        public int LivroId { get; set; }
        public int ClienteId { get; set; }


        public Result ValidarAluguel(IBookRentalRepository bookRental)
        {
            var buscaCliente = bookRental.ClienteExiste(this.ClienteId);
            if (!buscaCliente)
            {
                return Result.Failure("Este cliente não existe");
            }

            var busca = bookRental.LivroAlugadoPorCliente(this.ClienteId);
            if(busca != null)
            {
              return  Result.Failure("Este cliente ja tem um livro alugado");
            }
            return Result.OK();
        }
    }
}
