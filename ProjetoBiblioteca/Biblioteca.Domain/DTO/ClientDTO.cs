﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.DTO
{
    public class ClientDTO
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public Decimal SaldoDevedor { get; set; }
    }
}