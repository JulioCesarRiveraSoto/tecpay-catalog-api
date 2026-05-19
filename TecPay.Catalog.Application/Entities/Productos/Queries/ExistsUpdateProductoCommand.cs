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
    public sealed record ExistsUpdateProductoCommand(Guid Id, ProductoCreacionDTO Producto) 
        : IRequest<bool>;

    public sealed class ExistsUpdateProductoCommandHandler(IProductoRepository productoRepository) 
        : IRequestHandler<ExistsUpdateProductoCommand, bool>
    {
        public async Task<bool> Handle(ExistsUpdateProductoCommand request, CancellationToken cancellationToken)
        {
            return await productoRepository.ExistsProductoAsync(request.Producto.ProductoNombre, request.Id, cancellationToken);
        }

    }
}
