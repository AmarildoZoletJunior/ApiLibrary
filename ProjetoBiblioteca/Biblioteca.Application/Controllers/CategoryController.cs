using AutoMapper;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.DTO;

namespace Biblioteca.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public IMapper Mapper;
        private readonly IValidationExist _exist;
        public CategoryController(ICategoryRepository categoryRepository,IValidationExist exist, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            Mapper = mapper;
                _exist = exist;
        }

        [HttpGet]
        [Route("GetCategories")]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            var mapeado = Mapper.Map<List<CategoryDTO>>(categories);
            if (mapeado.Any())
            {
                return Ok(mapeado);
            }
            return BadRequest("Não foi encontrado nenhuma categoria");
        }


        [HttpGet]
        [Route("GetCategory/{id}")]
        public IActionResult GetCategory([Required][FromRoute] int id)
        {
            var cat = _categoryRepository.GetCategory(id);
            if (cat != null)
            {
                var mapeado = Mapper.Map<CategoryDTO>(cat);
                return Ok(mapeado);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteCategory/{id}")]
        public IActionResult DeleteCategory([Required][FromRoute] int id)
        {
            var cat = _categoryRepository.GetCategory(id);
            if (cat != null)
            {
                _categoryRepository.DeleteCategory(id);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("AddCategory")]
        public IActionResult AddCategory([Required][FromBody]CategoryRequest category)
        {
            Category cat = new Category { TipoCategoria = category.TipoCategoria};
            if (category != null)
            {
                _categoryRepository.AddCategory(cat);
                return Ok(category);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("UpdateCategory/{id}")]
        public IActionResult UpdateCategory([Required][FromBody] CategoryRequest category,[Required][FromRoute] int id)
        {
            var cat = _categoryRepository.GetCategory(id);
            if (cat != null)
            {
                cat.TipoCategoria = category.TipoCategoria;
                _categoryRepository.UpdateCategory(cat);
                return Ok(category);
            }
            return BadRequest();
        }
    }
}
