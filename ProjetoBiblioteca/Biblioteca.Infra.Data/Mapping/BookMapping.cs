using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infra.Data.Mapping
{
    public class BookMapping : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.QuantidadePagina).IsRequired();
            builder.Property(x => x.DataLancamento).IsRequired();
            builder.Property(x => x.Nome).IsRequired();
            builder.Ignore(x => x.Aluguel);
            builder.Property(x => x.ISBN).IsRequired();
        }
    }
}
