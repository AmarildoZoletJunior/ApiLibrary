using Biblioteca.Domain.DTO.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services.Validators
{
    public class CategoryValidation : AbstractValidator<CategoryRequest>
    {
        public CategoryValidation()
        {
            RuleFor(x => x.TipoCategoria)
                .NotEmpty().WithName("TipoCategoria").WithMessage("O campo TipoCategoria não pode ser vazio.")
                .NotNull().WithName("TipoCategoria").WithMessage("O campo TipoCategoria não pode ser nulo.")
                .MinimumLength(3).WithName("TipoCategoria").WithMessage("O campo TipoCategoria não pode conter menos de 3 caracteres.");


        }
    }
}
