using AutoMapper;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.DTO;

namespace Biblioteca.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        public IMapper Mapper;
        private readonly IValidationExist _exist;
        public ClientController(IClientRepository clientRepository,IValidationExist exist, IMapper mapper)
        {
            _clientRepository = clientRepository;
            Mapper = mapper;
            _exist = exist;
        }

        [HttpGet]
        [Route("GetClients")]
        public IActionResult GetCategories()
        {
            var clients = _clientRepository.GetClients();
            var mapeado = Mapper.Map<List<ClientDTO>>(clients);
            if (mapeado.Any())
            {
                return Ok(mapeado);
            }
            return BadRequest("Não foi encontrado nenhum cliente");
        }


        [HttpGet]
        [Route("GetClient/{id}")]
        public IActionResult GetCategory([Required][FromRoute] int id)
        {
            var client = _clientRepository.GetClient(id);
            if (client != null)
            {
                return Ok(client);
            }
            return BadRequest("Não foi encontrado nenhum cliente");
        }

        [HttpDelete]
        [Route("DeleteClient/{id}")]
        public IActionResult DeleteCategory([Required][FromRoute] int id)
        {
            var client = _clientRepository.GetClient(id);
            if (client != null)
            {
                _clientRepository.DeleteClient(id);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("AddClient")]
        public IActionResult AddCategory([Required][FromBody] ClientRequest Client)
        {
            var validacao = Client.ValidateCpf(_clientRepository);
            if (!validacao.Ok)
            {
                return BadRequest(validacao.ErrorMensagem);
            }
                Client client = new Client { Email = Client.Email, CPF = Client.CPF, Nome = Client.Nome };
                if (client != null)
                {
                    _clientRepository.AddClient(client);
                    return Ok(Client);
                }
               return BadRequest();
        }

        [HttpPut]
        [Route("UpdateClient")]
        public IActionResult UpdateCategory([Required][FromBody] ClientRequest Client,int id)
        {
            var mapeamento = Mapper.Map<Client>(Client);
            var client = _clientRepository.GetClient(id);
            if (client != null)
            {
                client.Nome = mapeamento.Nome;
                client.Email = mapeamento.Email;
                _clientRepository.UpdateClient(mapeamento);
                return Ok(client);
            }
            return BadRequest();
        }
    }
}
