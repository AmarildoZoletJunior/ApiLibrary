using AutoMapper;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Pagination;
using Biblioteca.Domain.Repository;
using Biblioteca.Infra.Data.Repository;
using Biblioteca.Services.RepositoryApplication;
using Microsoft.AspNetCore.Mvc;


namespace Biblioteca.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        public IMapper Mapper;
        private readonly IStockApplication _StockApplication;
        public StockController(IStockRepository stockRepository, IStockApplication app, IMapper map)
        {
            Mapper = map;
          _stockRepository = stockRepository;
            _StockApplication = app;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetStockAll([FromQuery]PageParameters parameter)
        {
            var busca = await _stockRepository.GetBooksStock(parameter);
            if (busca.Any())
            {
                var mapeado = Mapper.Map<List<StockDTO>>(busca);
                return Ok(mapeado);
            }
            return BadRequest("Nenhum item no estoque foi encontrado");
        }

        [HttpGet]
        [Route("{isbn}")]
        public async Task<IActionResult> GetStock([FromRoute] int isbn)
        {
            var busca = await _stockRepository.GetStock(isbn);
            if (busca != null)
            {
                return Ok(busca);
            }
            return BadRequest("Nenhum item no estoque foi encontrado");
        }

        [HttpPost]
        public async Task<IActionResult> AddInStock(StockRequest request)
        {
            var adicionar = await _StockApplication.ValidateAddAsync(request);
            if (adicionar.Ok)
            {
                return Ok(adicionar.Message);
            }
            return BadRequest(adicionar.ErrorMensagem);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStock(StockRequest request)
        {
            var atualizar = await _StockApplication.ValidateUpdateAsync(request);
            if (atualizar.Ok)
            {
                return Ok(atualizar.Message);
            }
            return BadRequest(atualizar.ErrorMensagem);
        }


    }
}
