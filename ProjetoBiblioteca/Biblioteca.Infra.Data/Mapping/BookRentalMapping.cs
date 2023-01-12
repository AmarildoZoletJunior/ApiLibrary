using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Biblioteca.Infra.Data.Mapping
{
    public class BookRentalMapping : IEntityTypeConfiguration<BookRental>
    {
        public void Configure(EntityTypeBuilder<BookRental> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
            builder.HasOne(x => x.Livro).WithOne(x => x.Aluguel).HasForeignKey<BookRental>(x => x.LivroId).IsRequired();
            builder.Property(x => x.DataSaida).IsRequired();
            builder.Property(x => x.DataVolta).IsRequired();
            builder.Property(x => x.ValorAluguel).IsRequired();
            builder.HasOne(x => x.Cliente).WithOne(x => x.Aluguel).HasForeignKey<BookRental>(x => x.ClienteId).IsRequired();
        }
    }
}
