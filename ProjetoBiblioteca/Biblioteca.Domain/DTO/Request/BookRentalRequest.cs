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
        public DateTime DataEstimadaVolta { get; set; }
        public decimal ValorAluguel { get; set; }
        public int LivroId { get; set; }
        public int ClienteId { get; set; }


        public Result ValidarAluguel(IBookRentalRepository bookRental,IValidationExist exist)
        {
            var buscaCliente = exist.ClientExist(this.ClienteId);
            if (!buscaCliente)
            {
                return Result.Failure("Este cliente não existe");
            }
            var buscarLivro = exist.BookExists(this.LivroId);
            if (!buscarLivro)
            {
                return Result.Failure("Este livro não existe");
            }

            var busca = bookRental.GetClientBookRental(this.ClienteId);

            if(busca)
            {
              return  Result.Failure("Este cliente ja tem um livro alugado");
            }
            return Result.OK();

            var verificarSaldo = bookRental.ExistValueClient(this.ClienteId);
            if(verificarSaldo > 0)
            {
                return Result.Failure($"Este cliente não pode alugar um livro pois tem saldo devedor em aberto. Saldo: R${verificarSaldo}");
            }
            
            return Result.OK();
        }
    }
}
