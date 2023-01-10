using Biblioteca.Domain.DTO;
using Biblioteca.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services.Validators
{
    public class AuthorValidation : AbstractValidator<AuthorDTO>
    {
        public AuthorValidation()
        {
            RuleFor(x => x.Nome).NotEmpty().WithName("Nome").WithMessage("O campo Nome não pode ser vazio.").NotNull().WithName("Nome").WithMessage("O campo Nome não pode ser nulo.").MinimumLength(3).WithName("Nome").WithMessage("O campo Nome não pode conter menos de 3 caracteres.");
        }
    }
}
