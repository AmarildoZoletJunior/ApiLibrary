﻿using AutoMapper;
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

namespace Biblioteca.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public IMapper Mapper;
        private readonly IValidationExist _exist;
        public BookController(IBookRepository bookRepository,IValidationExist exist, IMapper mapper)
        {
            _bookRepository = bookRepository;
            Mapper = mapper;
            _exist = exist;
        }
        [HttpGet]
        [Route("GetBooks")]
        public IActionResult GetBooks([FromQuery] PageParameters parameters)
        {
            var books = _bookRepository.GetBooks(parameters);

            if (books.Any())
            {
                return Ok(books);
            }
            return BadRequest("Não foi encontrado nenhum livro");
        }


        [HttpGet]
        [Route("GetBook/{id}")]
        public IActionResult GetBook([Required][FromRoute] int id)
        {
            var bookUnic = _bookRepository.GetBook(id);
            if (bookUnic != null)
            {
                var mapeado = Mapper.Map<BookDTO>(bookUnic);
                return Ok(mapeado);
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
                _bookRepository.DeleteBookAsync(id);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("AddBook")]
        public IActionResult AddBook([Required][FromBody] BookRequest bookR)
        {
            if (bookR == null)
            {
                return BadRequest();
            }
            var validacao = bookR.ValidarBook(_exist,_bookRepository);
            if (!validacao.Ok)
            {
                return BadRequest(validacao.ErrorMensagem);
            }
            Book book = new Book { DataLancamento = bookR.DataLancamento, Nome = bookR.Nome, CategoriaId = bookR.CategoriaId, AutorId = bookR.AutorId, QuantidadePagina = bookR.QuantidadePagina };
            _bookRepository.AddBook(book);
            return Ok(bookR);
        }
        
        [HttpPut]
        [Route("UpdateBook/{id}")]
        public async Task<IActionResult> UpdateBook([Required][FromBody]BookRequest book,[Required][FromRoute] int id)
        {
            var mapeamento = Mapper.Map<Book>(book);
            var unicBook = await  _bookRepository.GetBook(id);
            if (unicBook != null)
            {
                unicBook.DataLancamento = book.DataLancamento;
                unicBook.QuantidadePagina = book.QuantidadePagina;
                unicBook.AutorId = book.AutorId;
                unicBook.Nome = book.Nome;
                unicBook.CategoriaId = book.CategoriaId;
                _bookRepository.UpdateBook(unicBook);
                return Ok(book);
            }
            return BadRequest();
        }
    }
}
