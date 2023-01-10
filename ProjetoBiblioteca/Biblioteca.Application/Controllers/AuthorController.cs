using AutoMapper;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Application.Controllers
{
    [Route("api/[controller]")]
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
        public IActionResult GetAuthors()
        {
            var autores = _authorRepository.GetAuthors();
            var mapeado = Mapper.Map<AuthorDTO>(autores);
            if (autores.Any())
            {
                return Ok(mapeado);
            }
            return BadRequest();
        }
    }
}
