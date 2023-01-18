using AutoMapper;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Repository;
using Biblioteca.Infra.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static System.Reflection.Metadata.BlobBuilder;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.Pagination;
using Biblioteca.Services.RepositoryApplication;
using Biblioteca.Domain.Exceptions;

namespace Biblioteca.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public IMapper Mapper;
        private readonly IValidationExist _exist;
        private readonly IBookApplication _IBookApplication;
        public BookController(IBookRepository bookRepository,IBookApplication bookApp,IValidationExist exist, IMapper mapper)
        {
            _bookRepository = bookRepository;
            Mapper = mapper;
            _exist = exist;
            _IBookApplication = bookApp;
        }
        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetBooksAsync([FromQuery] PageParameters parameters)
        {
            var books = await _bookRepository.GetBooks(parameters);

            if (books.Any())
            {
                return Ok(books);
            }
            return BadRequest("Não foi encontrado nenhum livro");
        }


        [HttpGet]
        [Route("{isbn}")]
        public async Task<IActionResult> GetBookAsync([Required][FromRoute] int isbn)
        {
            var bookUnic = await _bookRepository.GetBook(isbn);
            if (bookUnic != null)
            {
                var mapeado = Mapper.Map<BookDTO>(bookUnic);
                return Ok(mapeado);
            }
            return BadRequest("Não foi encontrado nenhum livro");
        }

        [HttpDelete]
        [Route("{Isbn}")]
        public async Task<IActionResult> DeleteBookAsync([Required][FromRoute] int Isbn)
        {
            var book = await _bookRepository.GetBook(Isbn);
            if (book != null)
            {
                await _bookRepository.DeleteBookAsync(Isbn);
                return Ok();
            }
            return BadRequest("Não foi encontrado o livro para ser deletado");
        }

        [HttpPost]
        public async Task<IActionResult> AddBookAsync([Required][FromBody] BookRequest bookR)
        {
           var add = await _IBookApplication.ValidateAddAsync(bookR);
            if (add.Ok)
            {
                return Ok(bookR);
            }
            return BadRequest(add.ErrorMensagem);
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateBook([Required][FromBody]BookRequest book)
        {
            var add = await _IBookApplication.AtualizarBook(book);
            if (add.Ok)
            {
                return Ok(book);
            }
            return BadRequest(add.ErrorMensagem);
        }
    }
}
