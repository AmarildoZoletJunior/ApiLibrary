using AutoMapper;
using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Repository;
using Biblioteca.Services.RepositoryApplication;

namespace Biblioteca.Services.Applications
{
    public class ClientApplication : IClientApplication
    {
        private readonly IClientRepository _clientRepository;
        private readonly IValidationExist _Validation;
        public IMapper mapa;
        public ClientApplication(IClientRepository clientRepository, IValidationExist validation, IMapper map)
        {
            _clientRepository = clientRepository;
            _Validation = validation;
            mapa = map;
        }
        public Result ValidateCpfAdd(ClientRequest request)
        {
            if (VerificarCPF(request.CPF))
            {
                return Result.Failure("Cpf ja esta cadastrado");
            }
            var mapeamento = mapa.Map<Client>(request);
            _clientRepository.AddClient(mapeamento);
            return Result.OK();
        }

        public bool VerificarCPF(string cpf)
        {
            return _clientRepository.GetCpf(cpf);
        }
        public async Task<Result> UpdateClientAsync(ClientRequest request)
        {
            if (!VerificarCPF(request.CPF))
            {
                return Result.Failure("Cpf Não encontrado");
            }
            var pesquisaCliente = await _clientRepository.GetClient(request.CPF);
            pesquisaCliente.Email = request.Email;
            await _clientRepository.UpdateClient(pesquisaCliente);
            return Result.OK();
        }
    }
}
