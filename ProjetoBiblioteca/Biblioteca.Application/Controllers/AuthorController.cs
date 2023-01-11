using AutoMapper;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {

        private readonly IAuthorRepository _authorRepository;
        public IMapper Mapper;
        public AuthorController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            Mapper = mapper;
        }

        [HttpGet]
        [Route("GetAuthors")]
        public IActionResult GetAuthors()
        {
            var autores = _authorRepository.GetAuthors();
            var mapeado = Mapper.Map<List<AuthorDTO>>(autores);
            if (mapeado.Any())
            {
                return Ok(mapeado);
            }
            return BadRequest("Não foi encontrado nenhuma categoria");
        }


        [HttpGet]
        [Route("GetAuthor/{id}")]
        public IActionResult GetAuthor([Required][FromRoute] int id)
        {
            var autor = _authorRepository.GetAuthor(id);
            var mapeado = Mapper.Map<AuthorDTO>(autor);
            if(autor != null)
            {
                return Ok(mapeado);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteAuthor/{id}")]
        public IActionResult DeleteAuthor([Required][FromRoute] int id)
        {
            var autor = _authorRepository.GetAuthor(id);
            if(autor != null)
            {
                _authorRepository.DeleteAuthor(id);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("AddAuthor")]
        public IActionResult AddAuthor([Required][FromBody]AuthorDTO author)
        {
            if(author != null)
            {
                _authorRepository.AddAuthor(author);
                return Ok(author);
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateAuthor([Required][FromBody] AuthorDTO author)
        {
            var mapeamento = Mapper.Map<Author>(author);
            var aut = _authorRepository.GetAuthor(mapeamento.Id);
            if(aut != null)
            {
                aut.Nome = author.Nome;
                _authorRepository.UpdateAuthor(mapeamento);
                return Ok(author);
            }
            return BadRequest();
        }
    }
}
