using AutoMapper;
using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Repository;
using Biblioteca.Services.RepositoryApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services.Applications
{
    public class StockApplication : IStockApplication
    {
        private readonly IStockRepository _stockRepository;
        private readonly IValidationExist _Validation;
        public IMapper mapa;
        public StockApplication(IStockRepository stockRepository, IValidationExist validation, IMapper mapa)
        {
            _stockRepository = stockRepository;
            _Validation = validation;
            this.mapa = mapa;
        }

        public async Task<Result> ValidateAddAsync(StockRequest request)
        {
            if (!VerificarLivroExiste(request.ISBN)) 
            {
                return Result.Failure("Livro não existe");
            }
            if (_Validation.GetStock(request.ISBN))
            {
                var bookPesquisado = await _stockRepository.GetStock(request.ISBN);
                bookPesquisado.QuantidadeTotal =+ request.QuantidadeTotal;
                bookPesquisado.QuantidadeDisponivel += request.QuantidadeDisponivel;
                return Result.OKMessage("Livro ja cadastrado, Foram atualiazadas as quantidades");
            }
            var mapeado = mapa.Map<Stock>(request);
            var bookPesquisa = await _stockRepository.GetBookForAdd(request.ISBN);
            mapeado.IdLivro = bookPesquisa.Id;
            _stockRepository.AddBookStock(mapeado);
            return Result.OKMessage("Livro Adicionado no estoque com sucesso");
        }

        public async Task<Result> ValidateUpdateAsync(StockRequest request)
        {
            if (!VerificarLivroExiste(request.ISBN))
            {
                return Result.Failure("Livro não existe");
            }
            var bookPesquisa = await _stockRepository.GetStock(request.ISBN);
            bookPesquisa.QuantidadeTotal = request.QuantidadeTotal;
            bookPesquisa.QuantidadeDisponivel = request.QuantidadeDisponivel;
            await _stockRepository.UpdateStockQuantity(bookPesquisa);
            return Result.OKMessage("Livro atualizado no estoque com sucesso");
        }
        public bool VerificarLivroExiste(int ISBN)
        {
            var book = _Validation.BookExists(ISBN);
            if (book)
            {
                return true;
            }
            return false;
        }

        public bool VerificarSeExisteEstoque(int ISBN)
        {
            var stock = _Validation.GetStock(ISBN);
            if (stock)
            {
                return true;
            }
            return false;
        }
    }
}
