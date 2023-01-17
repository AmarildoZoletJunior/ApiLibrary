using Biblioteca.Domain.Entities;
using Biblioteca.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infra.Data.Context
{
    public class ClassContext : DbContext
    {
        public ClassContext(DbContextOptions<ClassContext> context) : base(context)
        {
        }
        public DbSet<Author> Autores { get; set; }
        public DbSet<Category> Categories {get;set;}
        public DbSet<Client> Clients { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookRental> BooksRents { get; set; }
        public DbSet<Stock> Estoque { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorMapping());
            modelBuilder.ApplyConfiguration(new BookMapping());
            modelBuilder.ApplyConfiguration(new CategoryMapping());
            modelBuilder.ApplyConfiguration(new ClientMapping());
            modelBuilder.ApplyConfiguration(new BookRentalMapping());
            modelBuilder.ApplyConfiguration(new StockMapping());
        }
    }
}
