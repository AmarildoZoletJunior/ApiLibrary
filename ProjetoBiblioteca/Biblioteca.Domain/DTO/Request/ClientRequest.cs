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


    }
}

