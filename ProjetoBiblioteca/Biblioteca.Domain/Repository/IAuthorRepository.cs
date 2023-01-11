using Biblioteca.Domain.DTO;
using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Repository
{
    public interface IAuthorRepository
    {
        public IEnumerable<Author> GetAuthors();
        public void AddAuthor(Author autor);
        public void DeleteAuthor(int id);
        public void UpdateAuthor(Author autor);
        public Author GetAuthor(int id);
    }
}
