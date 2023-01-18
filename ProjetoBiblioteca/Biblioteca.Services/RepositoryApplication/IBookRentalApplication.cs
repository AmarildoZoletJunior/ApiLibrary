using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services.RepositoryApplication
{
    public interface IBookRentalApplication
    {
        public bool VerificarExistenciaCliente(int ClienteId);
        public bool VerificarExistenciaLivro(int LivroId);
        public bool VerificarAluguelEmAberto(int ClienteId);
        public bool VerificarSaldoDevedor(int ClienteId);
        public bool VerificarDisponbilidade(int LivroId);
        public Task<Result> ValidateAddAsync(BookRentalRequest request);

    }
}
