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

namespace TecPay.Catalog.Application.Entities.Productos.Queries
{
    public sealed record ExistsCreateProductoCommand(ProductoCreacionDTO Producto) 
        : IRequest<bool>;

    public sealed class ExistsCreateProductoCommandHandler(IProductoRepository productoRepository) 
        : IRequestHandler<ExistsCreateProductoCommand, bool>
    {
        public async Task<bool> Handle(ExistsCreateProductoCommand request, CancellationToken cancellationToken)
        {
            return await productoRepository.ExistsProductoAsync(request.Producto.ProductoNombre, null, cancellationToken);
        }

    }
}
