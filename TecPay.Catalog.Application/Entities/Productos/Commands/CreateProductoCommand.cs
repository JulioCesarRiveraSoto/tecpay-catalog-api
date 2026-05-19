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
using TecPay.Catalog.Domain.Entities;

namespace TecPay.Catalog.Application.Entities.Productos.Commands
{
    public sealed record CreateProductoCommand(ProductoCreacionDTO Producto) 
        : IRequest<ProductoListaDTO>;

    public sealed class CreateProductoCommandHandler(IProductoRepository productoRepository, IValidator<ProductoCreacionDTO> productoCreacionValidator, IMapper mapper) 
        : IRequestHandler<CreateProductoCommand, ProductoListaDTO>
    {
        public async Task<ProductoListaDTO> Handle(CreateProductoCommand request, CancellationToken cancellationToken)
        {
            await productoCreacionValidator.ValidateAndThrowAsync(request.Producto, cancellationToken);

            if (await productoRepository.SkuExistsAsync(request.Producto.ProductoSKU, null, cancellationToken))
                throw new InvalidOperationException("SKU ya existe.");

            var producto = new Producto
                (
                    request.Producto.ProductoNombre, 
                    request.Producto.ProductoSKU, 
                    Guid.Parse(request.Producto.FK_IDCategoria), 
                    request.Producto.ProductoPrecio, 
                    request.Producto.ProductoStock, 
                    request.Producto.ProductoDescripcion
                );

            await productoRepository.AddAsync(producto, cancellationToken);
            await productoRepository.SaveChangesAsync(cancellationToken);
            return mapper.Map<ProductoListaDTO>(producto);
        }

    }
}
