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
        [Route("All")]
        public async Task<IActionResult> GetAuthorsAsync([FromQuery] PageParameters parameters)
        {
            var autores = await _authorRepository.GetAuthors(parameters);
            var mapeado = Mapper.Map<List<AuthorDTO>>(autores);
            if (mapeado.Any())
            {
                return Ok(mapeado);
            }
            return BadRequest("Não foi encontrado nenhum autor");
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAuthorAsync([Required][FromRoute] int id)
        {
            var autor = await _authorRepository.GetAuthor(id);
            if(autor != null)
            {
                var mapeado = Mapper.Map<AuthorDTO>(autor);
                return Ok(mapeado);
            }
            return BadRequest("Não foi encontrado nenhum autor");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAuthorAsync([Required][FromRoute] int id)
        {
            var autor = await _authorRepository.GetAuthor(id);
            if(autor != null)
            {
                await _authorRepository.DeleteAuthor(id);
                return Ok();
            }
            return BadRequest("Não foi encontrado nenhum autor");
        }

        [HttpPost]
        public IActionResult AddAuthor([Required][FromBody]AuthorRequest author)
        {
            if (author != null)
            {
                Author autor = new Author { Nome = author.Nome };
                _authorRepository.AddAuthor(autor);
                return Ok(author);
            }     
            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAuthorAsync([Required][FromBody] AuthorRequest author,[Required][FromRoute] int id)
        {
            var aut = await _authorRepository.GetAuthorSolo(id);
            if(aut != null)
            {
                aut.Nome = author.Nome;
                await _authorRepository.UpdateAuthor(aut);
                return Ok(aut);
            }
            return BadRequest("Não foi encontrado nenhum autor");
        }
    }
}
