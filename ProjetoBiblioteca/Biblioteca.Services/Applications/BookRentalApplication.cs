using AutoMapper;
using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Repository;
using Biblioteca.Services.RepositoryApplication;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services.Applications
{
    public class BookRentalApplication : IBookRentalApplication
    {
        private readonly IBookRentalRepository _bookRentalRepository;
        private readonly IValidationExist _Validation;
        public IMapper mapa;
        public BookRentalApplication(IBookRentalRepository bookRentalRepository, IValidationExist validation, IMapper map)
        {
            _bookRentalRepository = bookRentalRepository;
            _Validation = validation;
            mapa = map;
        }

        public bool VerificarAluguelEmAberto(int ClienteId)
        {
            var busca = _bookRentalRepository.GetClientBookRental(ClienteId);

            if (busca)
            {
                return false;
            }
            return true;
        }

        public bool VerificarDisponbilidade(int LivroId)
        {
             var verificarDisponivel =  _bookRentalRepository.ExistBookAvailableAsync(LivroId);
            if (!verificarDisponivel)
            {
                return false;
            }
            return true;
        }

        public bool VerificarExistenciaCliente(int ClienteId)
        {
            var buscaCliente = _Validation.ClientExist(ClienteId);
            if (!buscaCliente)
            {
                return false;
            }
            return true;
        }
        public bool VerificarSeExisteCadastradoEstoque(int idLivro)
        {
            var buscaEstoque = _bookRentalRepository.ExistStock(idLivro);
            if (!buscaEstoque)
            {
                return false;
            }
            return true;
        }

        public bool VerificarExistenciaLivro(int LivroId)
        {
            var buscarLivro = _Validation.BookExists(LivroId);
            if (!buscarLivro)
            {
                return false;
            }
            return true;
        }

        public bool VerificarSaldoDevedor(int ClienteId)
        {
            var verificarSaldo = _bookRentalRepository.ExistValueClient(ClienteId);
            if (verificarSaldo > 0)
            {
                return false;
            }
            return true;
        }
        public async Task<Result> ValidateAddAsync(BookRentalRequest request)
        {
                if (!VerificarExistenciaCliente(request.ClienteId))
                {
                    return Result.Failure("Este cliente não existe");
                }

                if (!VerificarExistenciaLivro(request.LivroId))
                {
                    return Result.Failure("Este livro não existe");
                }

                if (!VerificarAluguelEmAberto(request.ClienteId))
                {
                    return Result.Failure("Este cliente ja tem um livro alugado");
                }

                if (!VerificarSaldoDevedor(request.ClienteId))
                {
                    return Result.Failure($"Este cliente não pode alugar um livro pois tem saldo devedor em aberto.");
                }
                if(!VerificarSeExisteCadastradoEstoque(request.LivroId))
            {
                return Result.Failure($"Temos este livro cadastrado, mas em registro no estoque não existe.");
            }

                if (!VerificarDisponbilidade(request.LivroId))
                {
                    return Result.Failure($"Este livro não esta disponivel no estoque para ser alugado");
                }
            var mapeamento = mapa.Map<BookRental>(request);
            await _bookRentalRepository.AddRentalAsync(mapeamento);
            return Result.OK();
        }

        public async Task<Result> ValidateUpdateAsync(BookRentalRequest request)
        {

            if (!VerificarExistenciaCliente(request.ClienteId))
            {
                return Result.Failure("Este cliente não existe");
            }
            if (!VerificarExistenciaLivro(request.LivroId))
            {
                return Result.Failure("Este livro não existe");
            }
            var unicBook = await _bookRentalRepository.GetRental(request.ClienteId);
            if (unicBook != null)
            {
                unicBook.DataEstimadaVolta = request.DataEstimadaVolta;
                unicBook.DataSaida = request.DataSaida;
                unicBook.ValorAluguel = request.ValorAluguel;
                await _bookRentalRepository.UpdateRentalAsync(unicBook);
                return Result.OK();
            }
            return Result.Failure("Aluguel não encontrado");
        }
    }
}
