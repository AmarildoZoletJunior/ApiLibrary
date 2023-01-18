using AutoMapper;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.Pagination;
using Biblioteca.Services.RepositoryApplication;

namespace Biblioteca.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        public IMapper Mapper;
        private readonly IValidationExist _exist;
        private readonly IClientApplication _clientApplication;
        public ClientController(IClientRepository clientRepository,IClientApplication app,IValidationExist exist, IMapper mapper)
        {
            _clientRepository = clientRepository;
            Mapper = mapper;
            _exist = exist;
            _clientApplication = app;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetClientsAsync([FromQuery] PageParameters parameters)
        {
            var clients = await _clientRepository.GetClients(parameters);
            var mapeado = Mapper.Map<List<ClientDTO>>(clients);
            if (mapeado.Any())
            {
                return Ok(mapeado);
            }
            return BadRequest("Não foi encontrado nenhum cliente");
        }


        [HttpGet("{cpf}")]
        public async Task<IActionResult> GetClientAsync([Required][FromRoute] string cpf)
        {
            var client = await _clientRepository.GetClient(cpf);
            if (client != null)
            {
                return Ok(client);
            }
            return BadRequest("Não foi encontrado nenhum cliente");
        }

        [HttpDelete("{cpf}")]
        public async Task<IActionResult> DeleteClient([Required][FromRoute] string cpf)
        {
            var client = await _clientRepository.GetClient(cpf);
            if (client != null)
            {
                await _clientRepository.DeleteClientAsync(cpf);
                return Ok(client);
            }
            return BadRequest("Não foi possivel deletar");
        }

        [HttpPost]
        public IActionResult AddClient([Required][FromBody] ClientRequest Client)
        {
            var validacao = _clientApplication.ValidateCpfAdd(Client);
            if (validacao.Ok)
            {
                return Ok(Client);
            }
            return BadRequest(validacao.ErrorMensagem);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClientAsync([Required][FromBody] ClientRequest Client)
        {
            var exec = await _clientApplication.UpdateClientAsync(Client);
            if (exec.Ok)
            {
                return Ok(Client);
            }
            return BadRequest(exec.ErrorMensagem);
        }
    }
}
