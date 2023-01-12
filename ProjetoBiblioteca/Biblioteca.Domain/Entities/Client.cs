using Biblioteca.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Entities
{
    public class Client : BaseEntity
    {
        public string CPF { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public string Email { get; set; }
        public Decimal SaldoDevedor { get; set; }
        public BookRental Aluguel { get; set; }
        //Relacionamento com Aluguel
        //Relacionamento com Historico
    }
}
