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
        public IMapper Mapper;
        public AuthorRepository(ClassContext context,IMapper map)
        {
            _context = context;
            Mapper = map;
        }
        public void AddAuthor(Author autor)
        {
            var mapeamento = Mapper.Map<Author>(autor);
            _context.Autores.Add(mapeamento);
            _context.SaveChanges();
        }

        public void DeleteAuthor(int id)
        {
            var autor = GetAuthor(id);
            var mapeamento = Mapper.Map<Author>(autor);
            _context.Autores.Remove(mapeamento);
            _context.SaveChanges();
        }

        public Author GetAuthor(int id)
        {
            return _context.Autores.Include(x => x.Livros).AsNoTracking().FirstOrDefault(a => a.Id == id);
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
    }
}
