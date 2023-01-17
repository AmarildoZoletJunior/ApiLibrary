using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Pagination;
using Biblioteca.Domain.Repository;
using Biblioteca.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infra.Data.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ClassContext _classContext;
        public StockRepository(ClassContext classContext)
        {
            _classContext = classContext;
        }

        public void AddBookStock(Stock stock)
        {
            _classContext.Estoque.Add(stock);
            _classContext.SaveChanges();
        }

        public async Task DeleteStockAsync(int id)
        {
            var stock =  await _classContext.Estoque.FirstOrDefaultAsync(e => e.Id == id);
            _classContext.Estoque.Remove(stock);
           await _classContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Stock>> GetBookStock(PageParameters parametros)
        {
            return await _classContext.Estoque.OrderBy(x => x.Id).Skip((parametros.PageNumber - 1) * parametros.PageSize).Take(parametros.PageSize).ToListAsync();
        }

        public async Task<Stock> GetStock(int id)
        {
            return await _classContext.Estoque.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateStockQuantity(Stock stock)
        {
            _classContext.Estoque.Update(stock);
            await _classContext.SaveChangesAsync();
        }
    }
}
