using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services.RepositoryApplication
{
    public interface IClientApplication
    {
        public Result ValidateCpfAdd(ClientRequest request);
        public bool VerificarCPF(string cpf);
        public Task<Result> UpdateClientAsync(ClientRequest request);
    }
}
