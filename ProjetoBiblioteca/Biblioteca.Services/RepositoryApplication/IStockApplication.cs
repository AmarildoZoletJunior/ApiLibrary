using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services.RepositoryApplication
{
    public interface IStockApplication
    {
        public Task<Result> ValidateAddAsync(StockRequest request);
        public  Task<Result> ValidateUpdateAsync(StockRequest request);
        public bool VerificarLivroExiste(int ISBN);
        public bool VerificarSeExisteEstoque(int ISBN);


    }
}
