using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services.RepositoryApplication
{
    public interface IBookApplication
    {
        public Task<Result> ValidateAddAsync(BookRequest request);
        public bool VerificarCategoria(int categoriaId);
        public bool VerificarAutor(int id);
        public Task<bool> VerificarExistencia(int ISBN);

    }
}
