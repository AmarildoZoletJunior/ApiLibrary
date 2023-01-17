using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Repository
{
    public interface IStockRepository
    {
        public Task<IEnumerable<Stock>> GetBookStock(PageParameters parametros);
        public void AddBookStock(Stock stock);
        public Task DeleteStockAsync(int id);
        public Task UpdateStockQuantity(Stock stock);
        public Task<Stock> GetStock(int id);
    }
}
