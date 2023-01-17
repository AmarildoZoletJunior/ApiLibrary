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
    public class StockMapping : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Livro).WithOne(x => x.Estoque).HasForeignKey<Stock>(x => x.IdLivro);
            builder.Property(x => x.QuantidadeDisponivel).IsRequired();
            builder.Property(x => x.IdLivro).ValueGeneratedOnAdd();
            builder.Property(x => x.QuantidadeTotal).IsRequired();
        }
    }
}
