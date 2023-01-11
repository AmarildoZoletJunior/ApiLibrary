using Biblioteca.Domain.DTO;
using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services.Validators
{
    public class ClientValidation : AbstractValidator<ClientRequest>
    {
        public ClientValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithName("Nome").WithMessage("O campo Nome não pode estar vazio.")
                .NotNull().WithMessage("O campo Nome não pode ser nulo").WithName("Nome")
                .MinimumLength(3).WithMessage("O seu Nome não pode conter menos de 3 letras.");

            RuleFor(x => x.CPF)
                .NotEmpty().WithName("CPF").WithMessage("O campo CPF não pode estar vazio.")
                .NotNull().WithName("CPF").WithMessage("O campo CPF não pode ser nulo.");
                

            RuleFor(x => x.Email)
                .NotEmpty().WithName("Email").WithMessage("O campo Email não pode estar vazio.")
                .NotNull().WithName("Email").WithMessage("O campo Email não pode ser nulo.")
                .EmailAddress().WithName("Email").WithMessage("Este email é invalido.");
        }
    }
}
