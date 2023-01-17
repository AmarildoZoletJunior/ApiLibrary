using Biblioteca.Domain.DTO.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services.Validators
{
    public class StockValidation : AbstractValidator<StockRequest>
    {
        public StockValidation()
        {
            RuleFor(x => x.QuantidadeTotal)
                .GreaterThan(0).WithMessage("O valor da quantidade total tem que ser maior que 0")
                .NotNull().WithMessage("O campo quantidade nao pode ser nulo")
                .NotEmpty().WithMessage("O campo quantidade não pode ser vazio");


            RuleFor(x => x.QuantidadeDisponivel).GreaterThan(-1).WithMessage("o valor da quantidade disponivel não pode ser menos que 0")
                .NotEmpty().WithMessage("O campo quantidade disponivel não pode ser vazio")
                .NotNull().WithMessage("O campo quantidade disponivel não pode ser nulo");

            RuleFor(x => x.ISBN).NotNull().WithMessage("O campo ISBN não pode ser nulo")
                .NotEmpty().WithMessage("O campo ISBN não pode ser vazio");

        }
    }
}
