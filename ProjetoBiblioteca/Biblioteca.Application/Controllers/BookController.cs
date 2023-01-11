using AutoMapper;
using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Repository;
using Biblioteca.Infra.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public IMapper Mapper;
        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            Mapper = mapper;
        }
        [HttpGet]
        [Route("GetBooks")]
        public IActionResult GetBooks()
        {
            var books = _bookRepository.GetBooks();
            var mapeado = Mapper.Map<List<BookDTO>>(books);
            if (mapeado.Any())
            {
                return Ok(mapeado);
            }
            return BadRequest("Não foi encontrado nenhum livro");
        }


        [HttpGet]
        [Route("GetBook/{id}")]
        public IActionResult GetBook([Required][FromRoute] int id)
        {
            var autor = _bookRepository.GetBook(id);
            if (autor != null)
            {
                return Ok(autor);
            }
            return BadRequest("Não foi encontrado nenhum livro");
        }

        [HttpDelete]
        [Route("DeleteBook/{id}")]
        public IActionResult DeleteBook([Required][FromRoute] int id)
        {
            var autor = _bookRepository.GetBook(id);
            if (autor != null)
            {
                _bookRepository.DeleteBook(id);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("AddBook")]
        public IActionResult AddBook([Required][FromBody] BookRequest bookR)
        {
            Book book = new Book { DataLancamento = bookR.DataLancamento, Nome = bookR.Nome, CategoriaId = bookR.CategoriaId, AutorId = bookR.AutorId, QuantidadePagina = bookR.QuantidadePagina };
            if (bookR != null)
            {
                _bookRepository.AddBook(book);
                return Ok(book);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("UpdateBook")]
        public IActionResult UpdateBook([Required][FromBody]BookDTO book)
        {
            var mapeamento = Mapper.Map<Book>(book);
            var unicBook = _bookRepository.GetBook(mapeamento.Id);
            if (unicBook != null)
            {
                _bookRepository.UpdateBook(mapeamento);
                return Ok(book);
            }
            return BadRequest();
        }
    }
}
