using Biblioteca.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string TipoCategoria { get; set; } = null!;
        [JsonIgnore]
        public ICollection<Book> Livros { get; set; }
    }
}
