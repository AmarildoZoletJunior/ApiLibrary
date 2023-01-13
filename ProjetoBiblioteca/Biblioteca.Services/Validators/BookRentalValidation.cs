using Biblioteca.Domain.DTO.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services.Validators
{
    public class BookRentalValidation : AbstractValidator<BookRentalRequest>
    {
        public BookRentalValidation()
        {
            RuleFor(x => x.DataEstimadaVolta).NotEmpty().WithName("DataEstimadaVolta").WithMessage("O campo DataEstimadaVolta não pode ser vazio.")
                .NotNull().WithName("DataEstimadaVolta").WithMessage("O campo DataEstimadaVolta não pode ser nulo.");
            
            RuleFor(x => x.DataSaida).NotEmpty().WithName("DataSaida").WithMessage("O campo DataSaida não pode ser vazio.")
                .NotNull().WithName("DataSaida").WithMessage("O campo DataSaida não pode ser nulo.");

            RuleFor(x => x.LivroId).NotEmpty().WithName("LivroId").WithMessage("O campo LivroId não pode ser vazio.")
                .NotNull().WithName("LivroId").WithMessage("O campo LivroId não pode ser nulo.");
            
            RuleFor(x => x.ClienteId).NotEmpty().WithName("ClienteId").WithMessage("O campo ClienteId não pode ser vazio.")
                .NotNull().WithName("ClienteId").WithMessage("O campo ClienteId não pode ser nulo.");

            RuleFor(x => x.ValorAluguel).NotEmpty().WithName("ValorAluguel").WithMessage("O campo ValorAluguel não pode ser vazio.")
                .NotNull().WithName("ValorAluguel").WithMessage("O campo ValorAluguel não pode ser nulo.");

            RuleFor(x => x.DataEstimadaVolta).GreaterThan(x => x.DataSaida).WithMessage("A data de saida não pode ser menor que a data de retorno.");
        }
    }
}
