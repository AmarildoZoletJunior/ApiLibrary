using AutoMapper;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Pagination;
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
        private readonly IValidationExist _exist;
        public AuthorController(IAuthorRepository authorRepository,IValidationExist exist, IMapper mapper)
        {
            _authorRepository = authorRepository;
            Mapper = mapper;
                _exist = exist;
        }

        [HttpGet]
        [Route("GetAuthors")]
        public IActionResult GetAuthors([FromQuery] PageParameters parameters)
        {
            var autores = _authorRepository.GetAuthors(parameters);
            var mapeado = Mapper.Map<List<AuthorDTO>>(autores);
            if (mapeado.Any())
            {
                return Ok(mapeado);
            }
            return BadRequest("Não foi encontrado nenhum autor");
        }


        [HttpGet]
        [Route("GetAuthor/{id}")]
        public IActionResult GetAuthor([Required][FromRoute] int id)
        {
            var autor = _authorRepository.GetAuthor(id);
            if(autor != null)
            {
                var mapeado = Mapper.Map<AuthorDTO>(autor);
                return Ok(mapeado);
            }
            return BadRequest("Não foi encontrado nenhum autor");
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
        public IActionResult AddAuthor([Required][FromBody]AuthorRequest author)
        {
            Author autor = new Author { Nome = author.Nome };
            if (author != null)
            {
                _authorRepository.AddAuthor(autor);
                return Ok(author);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("UpdateAuthor/{id}")]
        public async Task<IActionResult> UpdateAuthorAsync([Required][FromBody] AuthorRequest author, [Required][FromRoute] int id)
        {
            var mapeado = Mapper.Map<Author>(author);
            var aut = await _authorRepository.GetAuthorSolo(id);
            if(aut != null)
            {
                aut.Nome = author.Nome;
                _authorRepository.UpdateAuthor(aut);
                return Ok(aut);
            }
            return BadRequest();
        }
    }
}
