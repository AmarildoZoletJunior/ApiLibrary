using AutoMapper;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.Pagination;

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
        [Route("All")]
        public async Task<IActionResult> GetCategoriesAsync([FromQuery] PageParameters parameters)
        {
            var categories =  await _categoryRepository.GetCategories(parameters);
            var mapeado = Mapper.Map<List<CategoryDTO>>(categories);
            if (mapeado.Any())
            {
                return Ok(mapeado);
            }
            return BadRequest("Não foi encontrado nenhuma categoria");
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCategoryAsync([Required][FromRoute] int id)
        {
            var cat = await _categoryRepository.GetCategory(id);
            if (cat != null)
            {
                var mapeado = Mapper.Map<CategoryDTO>(cat);
                return Ok(mapeado);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync([Required][FromRoute] int id)
        {
            var cat = await _categoryRepository.GetCategory(id);
            if (cat != null)
            {
                await _categoryRepository.DeleteCategory(id);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        
        public IActionResult AddCategory([Required][FromBody]CategoryRequest category)
        {

            if (category != null)
            {
                var mapeamento = Mapper.Map<Category>(category);
                mapeamento.TipoCategoria = category.TipoCategoria;
                _categoryRepository.AddCategory(mapeamento);
                return Ok(category);
            }
            return BadRequest("A categoria esta nula.");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCategory([Required][FromBody] CategoryRequest category,[Required][FromRoute] int id)
        {
            var cat = await _categoryRepository.GetCategory(id);
            if (cat != null)
            {
                cat.TipoCategoria = category.TipoCategoria;
               await _categoryRepository.UpdateCategory(cat);
                return Ok(category);
            }
            return BadRequest();
        }
    }
}
