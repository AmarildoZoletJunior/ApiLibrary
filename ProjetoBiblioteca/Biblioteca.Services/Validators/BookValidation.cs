using Biblioteca.Domain.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services.Validators
{
    public class BookValidation : AbstractValidator<BookDTO>
    {
        public BookValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithName("Nome").WithMessage("O campo Nome não pode ser vazio.").NotNull().WithName("Nome").WithMessage("O campo Nome não pode ser nulo.");
        }
    }
}
