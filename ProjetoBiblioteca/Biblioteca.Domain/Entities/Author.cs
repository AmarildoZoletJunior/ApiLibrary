using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Biblioteca.Domain.Entities.Base;

namespace Biblioteca.Domain.Entities
{
    public class Author : BaseEntity
    {
        public string Nome { get; set; }
        [JsonIgnore]
        public ICollection<Book>? Livros { get; set; }

    }
}
