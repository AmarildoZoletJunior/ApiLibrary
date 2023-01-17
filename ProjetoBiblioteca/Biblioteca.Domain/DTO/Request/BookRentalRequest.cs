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


        public Result ValidarAluguel(IBookRentalRepository bookRental,IValidationExist exist)
        {
            if (!VerificarExistenciaCliente(exist))
            {
                return Result.Failure("Este cliente não existe");
            }

            if (!VerificarExistenciaLivro(exist))
            {
                return Result.Failure("Este livro não existe");
            }

            if(!VerificarAluguelEmAberto(bookRental))
            {
              return  Result.Failure("Este cliente ja tem um livro alugado");
            }

            if (!VerificarSaldoDevedor(bookRental))
            {
                return Result.Failure($"Este cliente não pode alugar um livro pois tem saldo devedor em aberto.");
            }

            if (!VerificarDisponbilidade(bookRental))
            {
                return Result.Failure($"Este livro não esta disponivel no estoque para ser alugado");
            }
            return Result.OK();
        }

        public bool VerificarDisponbilidade(IBookRentalRepository bookRental)
        {
            var verificarDisponivel = bookRental.ExistBookAvailable(this.LivroId);
            if (!verificarDisponivel)
            {
                return false;
            }
            return true;
        }

        public bool VerificarSaldoDevedor(IBookRentalRepository bookRental)
        {
            var verificarSaldo = bookRental.ExistValueClient(this.ClienteId);
            if (verificarSaldo > 0)
            {
                return false;
            }
            return true;
        }
        public bool VerificarAluguelEmAberto(IBookRentalRepository bookRental)
        {
            var busca = bookRental.GetClientBookRental(this.ClienteId);

            if (busca)
            {
                return false;
            }
            return true;
        }
        public bool VerificarExistenciaLivro(IValidationExist exist)
        {
            var buscarLivro = exist.BookExists(this.LivroId);
            if (!buscarLivro)
            {
                return false;
            }
            return true;
        }
        public bool VerificarExistenciaCliente(IValidationExist exist)
        {
            var buscaCliente = exist.ClientExist(this.ClienteId);
            if (!buscaCliente)
            {
                return false;
            }
            return true;
        }
    }
}
