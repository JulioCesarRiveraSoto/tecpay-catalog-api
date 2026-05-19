using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Application.Entities.Productos.DTOs;

namespace TecPay.Catalog.Application.Entities.Productos.Validators
{
    public sealed class ProductoCreacionValidator : AbstractValidator<ProductoCreacionDTO>
    {
        public ProductoCreacionValidator()
        {
            RuleFor(x => x.ProductoNombre)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.ProductoSKU)
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(x => x.FK_IDCategoria)
                .NotEmpty();

            RuleFor(x => x.ProductoPrecio)
                .GreaterThan(0)
                .LessThanOrEqualTo(999999);

            RuleFor(x => x.ProductoStock)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(999999);

            RuleFor(x => x.ProductoDescripcion)
                .MaximumLength(500);
        }
    }
}
