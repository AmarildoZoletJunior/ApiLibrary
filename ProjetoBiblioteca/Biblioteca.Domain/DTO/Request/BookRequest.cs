using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Repository;
using System;
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
        public Result ValidarBook(IValidationExist exist, IBookRepository book)
        {
            var AutorExist = exist.AuthorExist(this.AutorId);
            if(!AutorExist)
            {
                return Result.Failure("Este autor não existe.");
            }
            var CategoriaExist = exist.CategoryExist(this.CategoriaId);
            if (!CategoriaExist)
            {
                return Result.Failure("Esta categoria não existe.");
            }
            return Result.OK();
        }
    }
}
