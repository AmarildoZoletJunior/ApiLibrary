using Biblioteca.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Entities
{
    public class Client : BaseEntity
    {
        public string CPF { get; set; } = null!;
        public string NomeCliente { get; set; } = null!;

    }
}
