using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Pagination;
using Biblioteca.Domain.Repository;
using Biblioteca.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infra.Data.Repository
{
    public class BookRentalRepository : IBookRentalRepository
    {

        private readonly ClassContext _context;
        public BookRentalRepository(ClassContext context)
        {
            _context = context;
        }
        public async Task AddRentalAsync(BookRental book)
        {
            _context.BooksRents.Add(book);
            var livroAlugar = await _context.Estoque.Include(x => x.Livro).FirstOrDefaultAsync(x => x.Livro.Id == book.LivroId);
            livroAlugar.QuantidadeDisponivel += -1;
            _context.SaveChanges();
        }

        public bool ClientExists(int id)
        {
            var ClientExist = _context.Clients.FirstOrDefault(x => x.Id == id);
            if(ClientExist == null)
            {
                return false;
            }
            return true;
        }

        public async Task DeleteRentalAsync(int id)
        {
            var deletar = await _context.BooksRents.FirstOrDefaultAsync(x => x.ClienteId == id);
            _context.BooksRents.Remove(deletar);
             await _context.SaveChangesAsync();
        }

        public bool GetClientBookRental(int id)
        {
            var RentalExist = _context.BooksRents.FirstOrDefault(x => x.ClienteId == id);
            if (RentalExist == null)
            {
                return false;
            }
            return true;
        }

        public async Task<BookRental> GetRental(int ClientId)
        {
            return  await _context.BooksRents.Include(x => x.Cliente).Include(x => x.Livro).ThenInclude(x => x.Autor).Include(x => x.Livro).ThenInclude(x => x.Categoria).SingleOrDefaultAsync(x => x.ClienteId == ClientId);
        }

        public async Task<IEnumerable<BookRental>> GetRents(PageParameters parametros)
        {
            return await _context.BooksRents.OrderBy(x => x.ValorAluguel).Skip((parametros.PageNumber - 1) * parametros.PageSize).Take(parametros.PageSize).ToListAsync();
        }

        public async Task UpdateRentalAsync(BookRental book)
        {
            _context.BooksRents.Update(book);
            await _context.SaveChangesAsync();
        }
        public bool BookExists(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);
            if(book != null)
            {
                return true;
            }
            return false;
        }
        public decimal ExistValueClient(int id)
        {
            var book = _context.Clients.FirstOrDefault(x => x.Id == id);
            if(book.SaldoDevedor > 0)
            {
                return book.SaldoDevedor;
            }
            return 0;
        }
        public bool ExistBookAvailableAsync(int id)
        {
            var book = _context.Estoque.FirstOrDefault(x => x.IdLivro == id);
            if(book == null)
            {
                return false;
            }
            if(book.QuantidadeDisponivel > 0)
            {
                return true;
            }
            return false;
        }
        public bool ExistStock(int id)
        {
            var book = _context.Estoque.FirstOrDefault(x => x.IdLivro == id);
            if (book == null)
            {
                return false;
            }
            return true;
        }

    }
}
