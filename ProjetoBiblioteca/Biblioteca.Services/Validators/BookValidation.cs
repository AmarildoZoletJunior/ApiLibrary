using Biblioteca.Domain.DTO;
using Biblioteca.Domain.DTO.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services.Validators
{
    public class BookValidation : AbstractValidator<BookRequest>
    {
        public BookValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithName("Nome").WithMessage("O campo Nome não pode ser vazio.")
                .NotNull().WithName("Nome").WithMessage("O campo Nome não pode ser nulo.");

            RuleFor(x => x.DataLancamento)
                .NotEmpty().WithName("DataLançamento").WithMessage("O campo DataLançamento não pode ser vazio.")
                .NotNull().WithName("DataLançamento").WithMessage("O campo DataLançamento não pode ser nulo.");

            RuleFor(x => x.QuantidadePagina)
                .NotEmpty().WithName("QuantidadeFolhas").WithMessage("O campo QuantidadeFolhas não pode ser vazio.")
                .NotNull().WithName("QuantidadeFolhas").WithMessage("O campo QuantidadeFolhas não pode ser nulo.");


            RuleFor(x => x.CategoriaId)
                 .NotEmpty().WithName("CategoriaId").WithMessage("O campo CategoriaId não pode ser vazio.")
                .NotNull().WithName("CategoriaId").WithMessage("O campo CategoriaId não pode ser nulo.");

        }
    }
}
