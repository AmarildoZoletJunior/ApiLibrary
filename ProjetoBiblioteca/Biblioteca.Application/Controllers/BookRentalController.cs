using AutoMapper;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Pagination;
using Biblioteca.Domain.Repository;
using Biblioteca.Infra.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookRentalController : ControllerBase
    {
        private readonly IBookRentalRepository _bookRentalRepository;
        public IMapper Mapper;
        private readonly IValidationExist _exist;

        public BookRentalController(IBookRentalRepository bookRentalRepository,IValidationExist exist, IMapper mapper)
        {
            _bookRentalRepository = bookRentalRepository;
            Mapper = mapper;
            _exist = exist;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRentalAsync([FromQuery] PageParameters parameters)
        {
            var alugueis = await _bookRentalRepository.GetRents(parameters);
            if (alugueis.Any())
            {
                var mapeado = Mapper.Map<List<BookRentalDTO>>(alugueis);
                return Ok(mapeado);
            }
            return BadRequest("Não foi encontrado nenhum aluguel cadastrado");
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRentalAsync([Required][FromRoute]int id)
        {
            var aluguel = await _bookRentalRepository.GetRental(id);
            if (aluguel != null)
            {
                var mapeado = Mapper.Map<BookRentalDTO>(aluguel);
                return Ok(mapeado);
            }
            return BadRequest("Não foi encontrado nenhum aluguel cadastrado");
        }


        [HttpPost]
        public IActionResult AddRental([Required][FromBody]BookRentalRequest request)
        {
            var validacao = request.ValidarAluguel(_bookRentalRepository,_exist);
            if (!validacao.Ok) 
            {
                return Ok(validacao.ErrorMensagem);
            }
            var mapeado = Mapper.Map<BookRental>(request);
            _bookRentalRepository.AddRentalAsync(mapeado);
            return Ok(mapeado);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveRentalAsync([Required][FromRoute] int id)
        {
            var rental = await _bookRentalRepository.GetRental(id);
            if(rental != null)
            {
               await _bookRentalRepository.DeleteRentalAsync(id);
                return Ok(rental);
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRentalAsync([Required][FromBody] BookRentalRequest request,int id)
        {
            var unicBook = await _bookRentalRepository.GetRental(id);
            if (unicBook != null)
            {
                unicBook.DataEstimadaVolta = request.DataEstimadaVolta;
                unicBook.DataSaida = request.DataSaida;
                unicBook.ClienteId = request.ClienteId;
                unicBook.LivroId = request.LivroId;
               await _bookRentalRepository.UpdateRentalAsync(unicBook);
                return Ok(unicBook);
            }
            return BadRequest();
        }
    }
}
