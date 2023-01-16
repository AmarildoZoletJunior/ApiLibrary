using AutoMapper;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Pagination;
using Biblioteca.Domain.Repository;
using Biblioteca.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infra.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ClassContext _context;
        public CategoryRepository(ClassContext context)
        {
            _context = context;
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public async Task DeleteCategory(int id)
        {
            var category = await GetCategory(id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Category>> GetCategories(PageParameters parametros)
        {
            return await _context.Categories.OrderBy(x => x.TipoCategoria).Skip((parametros.PageNumber - 1) * parametros.PageSize).Take(parametros.PageSize).ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _context.Categories.Include(x => x.Livros).AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
