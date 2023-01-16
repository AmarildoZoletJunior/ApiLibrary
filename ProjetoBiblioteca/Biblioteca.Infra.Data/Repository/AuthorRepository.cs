using AutoMapper;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Pagination;
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

        public async void DeleteAuthor(int id)
        {
            var autor = await GetAuthorSolo(id);
            _context.Autores.Remove(autor);
            _context.SaveChanges();
        }

        public async Task<Author> GetAuthor(int id)
        {
            return await _context.Autores.Include(x => x.Livros).ThenInclude(x => x.Categoria).AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public IEnumerable<Author> GetAuthors(PageParameters parametros)
        {
            return _context.Autores.OrderBy(x => x.Nome).Skip((parametros.PageNumber - 1) * parametros.PageSize).Take(parametros.PageSize).ToList();
        }

        public void UpdateAuthor(Author autor)
        {
            _context.Autores.Update(autor);
            _context.SaveChanges();
        }
        public async Task<Author> GetAuthorSolo(int id)
        {
            return await _context.Autores.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
