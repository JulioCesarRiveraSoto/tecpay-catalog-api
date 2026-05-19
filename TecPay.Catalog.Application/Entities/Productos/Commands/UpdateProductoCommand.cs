using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Application.Entities.Productos.DTOs;
using TecPay.Catalog.Application.Entities.Productos.Interfaces;

namespace TecPay.Catalog.Application.Entities.Productos.Commands
{
    public sealed record UpdateProductoCommand(Guid Id, ProductoCreacionDTO Producto) 
        : IRequest<ProductoListaDTO?>;

    public sealed class UpdateProductoCommandHandler(IProductoRepository productoRepository, IValidator<ProductoCreacionDTO> productoCreacionValidator, IMapper mapper) 
        : IRequestHandler<UpdateProductoCommand, ProductoListaDTO?>
    {
        public async Task<ProductoListaDTO?> Handle(UpdateProductoCommand request, CancellationToken cancellationToken)
        {
            await productoCreacionValidator.ValidateAndThrowAsync(request.Producto, cancellationToken);

            var entity = await productoRepository.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null)
                return null;

            if (await productoRepository.SkuExistsAsync(request.Producto.ProductoSKU, request.Id, cancellationToken))
                throw new InvalidOperationException("SKU ya existe.");

            entity.Update
                (
                    request.Producto.ProductoNombre, 
                    request.Producto.ProductoSKU, 
                    Guid.Parse(request.Producto.FK_IDCategoria), 
                    request.Producto.ProductoPrecio, 
                    request.Producto.ProductoStock, 
                    request.Producto.ProductoDescripcion
                );
            await productoRepository.SaveChangesAsync(cancellationToken);
            return mapper.Map<ProductoListaDTO>(entity);
        }
    }
}
