using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.DTO.Request
{
    public class ClientRequest
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public Result ValidateCpf(IClientRepository repository)
        {
            if (!VerificarCPF(repository))
            {
                return Result.Failure("Este cpf ja foi cadastrado");
            }
            return Result.OK();
        }
        public bool VerificarCPF(IClientRepository repository)
        {
            var resultado = repository.GetCpf(CPF);
            if (resultado)
            {
                return false;
            }
            return true;
        }

    }
}

