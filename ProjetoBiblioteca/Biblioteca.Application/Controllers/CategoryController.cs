using AutoMapper;
using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public IMapper Mapper;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            Mapper = mapper;
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
                return Ok(cat);
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
        [Route("UpdateCategory")]
        public IActionResult UpdateCategory([Required][FromBody] CategoryDTO category)
        {
            var mapeamento = Mapper.Map<Category>(category);
            var cat = _categoryRepository.GetCategory(mapeamento.Id);
            if (cat != null)
            {
                cat.TipoCategoria = category.TipoCategoria;
                _categoryRepository.UpdateCategory(mapeamento);
                return Ok(cat);
            }
            return BadRequest();
        }
    }
}
