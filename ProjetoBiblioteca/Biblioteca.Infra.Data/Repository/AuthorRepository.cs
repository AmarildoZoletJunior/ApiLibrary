using AutoMapper;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Repository;
using Biblioteca.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infra.Data.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ClassContext _context;
        public AuthorRepository(ClassContext context)
        {
            _context = context;
        }
        public void AddAuthor(Author autor)
        {
            _context.Autores.Add(autor);
            _context.SaveChanges();
        }

        public void DeleteAuthor(int id)
        {
            var autor = GetAuthorSolo(id);
            _context.Autores.Remove(autor);
            _context.SaveChanges();
        }

        public Author GetAuthor(int id)
        {
            return _context.Autores.Include(x => x.Livros).ThenInclude(x => x.Categoria).AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _context.Autores.ToList();
        }

        public void UpdateAuthor(Author autor)
        {
            _context.Autores.Update(autor);
            _context.SaveChanges();
        }
        public Author GetAuthorSolo(int id)
        {
            return _context.Autores.AsNoTracking().FirstOrDefault(a => a.Id == id);
        }
    }
}
