using Biblioteca.Domain.Entities.Base;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Nome { get; set; }
        public DateTime DataLancamento { get; set; }
        public int QuantidadePagina { get; set; }
        [JsonIgnore]
        public Author Autor { get; set; }
        public int AutorId { get; set; }
        [JsonIgnore]
        public Category Categoria { get; set; }
        public int  CategoriaId { get; set; }

        [JsonIgnore]
        public List<BookRental> Aluguel { get; set; }

        [JsonIgnore]
        public Stock Estoque { get; set; }
        public int ISBN { get; set; }
    }
}
