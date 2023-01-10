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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorMapping());
        }
    }
}
