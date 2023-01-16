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
        public IActionResult GetAllRental([FromQuery] PageParameters parameters)
        {
            var alugueis = _bookRentalRepository.GetRents(parameters);
            if (alugueis.Any())
            {
                var mapeado = Mapper.Map<List<BookRentalDTO>>(alugueis);
                return Ok(mapeado);
            }
            return BadRequest("Não foi encontrado nenhum aluguel cadastrado");
        }

        [HttpGet("{id:int}")]
        public IActionResult GetRental([Required][FromRoute]int id)
        {
            var aluguel = _bookRentalRepository.GetRental(id);
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
            _bookRentalRepository.AddRental(mapeado);
            return Ok(mapeado);
        }


        [HttpDelete("{id:int}")]
        public IActionResult RemoveRental([Required][FromRoute] int id)
        {
            var rental = _bookRentalRepository.GetRental(id);
            if(rental != null)
            {
                _bookRentalRepository.DeleteRental(id);
                return Ok();
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRentalAsync([Required][FromBody] BookRentalRequest request,int id)
        {
            var mapeamento = Mapper.Map<BookRental>(request);
            var unicBook = await _bookRentalRepository.GetRental(id);
            if (unicBook != null)
            {
                unicBook.DataEstimadaVolta = request.DataEstimadaVolta;
                unicBook.DataSaida = request.DataSaida;
                unicBook.ClienteId = request.ClienteId;
                unicBook.LivroId = request.LivroId;
                _bookRentalRepository.UpdateRental(mapeamento);
                return Ok(request);
            }
            return BadRequest();
        }
    }
}
