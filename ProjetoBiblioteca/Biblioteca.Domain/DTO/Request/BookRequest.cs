using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.DTO.Request
{
    public class BookRequest
    {
        public string Nome { get; set; }
        public DateTime DataLancamento { get; set; }
        public int QuantidadePagina { get; set; }
        public int AutorId { get; set; }
        public int CategoriaId { get; set; }
        public int ISBN { get; set; }
    }
}
