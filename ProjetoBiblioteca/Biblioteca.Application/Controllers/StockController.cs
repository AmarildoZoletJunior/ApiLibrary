using AutoMapper;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Pagination;
using Biblioteca.Domain.Repository;
using Biblioteca.Infra.Data.Repository;
using Microsoft.AspNetCore.Mvc;


namespace Biblioteca.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        public IMapper Mapper;
        public StockController(IStockRepository stockRepository,IMapper map)
        {
            Mapper = map;
          _stockRepository = stockRepository;
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
            var pesquisar = await _stockRepository.GetStock(request.ISBN);
            if (pesquisar != null)
            {
                await UpdateStock(request);
                return Ok("Livro ja cadastrado, Foram atualiazadas as quantidades");
            }
            var bookPesquisa = await _stockRepository.GetBookForAdd(request.ISBN);
            var novo = new Stock { IdLivro = bookPesquisa.Id, QuantidadeDisponivel = request.QuantidadeDisponivel, QuantidadeTotal = request.QuantidadeTotal };
            _stockRepository.AddBookStock(novo);
            return Ok(novo);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStock(StockRequest request)
        {
            var stock = await _stockRepository.GetStock(request.ISBN);
            if (stock != null)
            {
                stock.QuantidadeTotal = request.QuantidadeTotal;
                stock.QuantidadeDisponivel = request.QuantidadeTotal;
                await _stockRepository.UpdateStockQuantity(stock);
                return Ok(stock);
            }
            return BadRequest();
        }


    }
}
