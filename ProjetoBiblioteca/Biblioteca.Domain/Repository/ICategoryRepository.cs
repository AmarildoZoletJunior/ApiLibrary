using Biblioteca.Domain.DTO;
using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Repository
{
    public interface ICategoryRepository
    {
        public IEnumerable<Category> GetCategories();
        public void AddCategory(Category category);
        public void DeleteCategory(int id);
        public void UpdateCategory(Category category);
        public Category GetCategory(int id);
    }
}
