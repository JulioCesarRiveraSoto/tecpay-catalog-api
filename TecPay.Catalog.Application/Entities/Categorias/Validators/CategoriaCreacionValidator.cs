using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Application.Entities.Categorias.DTOs;

namespace TecPay.Catalog.Application.Entities.Categorias.Validators
{
    public sealed class CategoriaCreacionValidator : AbstractValidator<CategoriaCreacionDTO>
    {
        public CategoriaCreacionValidator()
        {
            RuleFor(x => x.CategoriaNombre)
                .NotEmpty()
                .MaximumLength(120);

        }
    }
}
