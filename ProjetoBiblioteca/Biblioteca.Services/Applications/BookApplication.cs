using AutoMapper;
using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Pagination;
using Biblioteca.Domain.Repository;
using Biblioteca.Services.RepositoryApplication;

namespace Biblioteca.Services.Applications
{
    public class BookApplication : IBookApplication
    {
        private readonly IBookRepository _bookRepository;
        private readonly IValidationExist _Validation;
        public IMapper mapa;
        public BookApplication(IBookRepository bookRepository, IValidationExist validation,IMapper map)
        {
            _bookRepository = bookRepository;
            _Validation = validation;
            mapa = map;
        }
        public bool VerificarAutor(int id)
        {
            var AutorExist = _Validation.AuthorExist(id);
            if (!AutorExist)
            {
                return false;
            }
            return true;
        }
        public bool VerificarCategoria(int categoriaId)
        {
            var CategoriaExist = _Validation.CategoryExist(categoriaId);
            if (!CategoriaExist)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> VerificarExistencia(int ISBN)
        {
            var bookExist = await _bookRepository.GetBook(ISBN);
            if (bookExist != null)
            {
                return true;
            }
            return false;
        }
        public async Task<Result> ValidateAddAsync(BookRequest request)
        {

            if (!VerificarCategoria(request.CategoriaId))
            {
                return Result.Failure("Esta categoria não existe");
            }
            if (!VerificarAutor(request.AutorId))
            {
                return Result.Failure("Este autor não existe");
            }
            if (await VerificarExistencia(request.ISBN))
            {
                await AtualizarBook(request);
                return Result.OK();

            }
            var mapeamento = mapa.Map<Book>(request);
            _bookRepository.AddBook(mapeamento);
            return Result.OK();
        }
        public async Task AtualizarBook(BookRequest request)
        {
            var buscar = await _bookRepository.GetBook(request.ISBN);
            buscar.Nome = request.Nome;
            buscar.QuantidadePagina = request.QuantidadePagina;
            await _bookRepository.UpdateBook(buscar);
        }
    }
}
