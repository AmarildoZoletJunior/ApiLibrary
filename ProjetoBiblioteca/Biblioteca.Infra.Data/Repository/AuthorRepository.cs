using AutoMapper;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Repository;
using Biblioteca.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void AddAuthor(AuthorDTO autor)
        {
            var mapeamento = Mapper.Map<Author>(autor);
            _context.Autores.Add(mapeamento);
        }

        public void DeleteAuthor(int id)
        {
            var autor = GetAuthor(id);
            var mapeamento = Mapper.Map<Author>(autor);
            _context.Autores.Remove(mapeamento);
        }

        public Author GetAuthor(int id)
        {
            var author = _context.Autores.FirstOrDefault(a => a.Id == id);
            return author;
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _context.Autores.ToList();
        }

        public void UpdateAuthor(AuthorDTO autor,int id)
        {
            var mapeamento = Mapper.Map<Author>(autor);
            _context.Autores.Update(mapeamento);
        }
    }
}
