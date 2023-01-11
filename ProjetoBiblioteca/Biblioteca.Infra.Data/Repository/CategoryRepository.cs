using AutoMapper;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Repository;
using Biblioteca.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infra.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ClassContext _context;
        public IMapper Mapper;
        public CategoryRepository(ClassContext context, IMapper map)
        {
            _context = context;
            Mapper = map;
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = GetCategory(id);
            var mapeamento = Mapper.Map<Category>(category);
            _context.Categories.Remove(mapeamento);
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Include(x => x.Livros).AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
    }
}
