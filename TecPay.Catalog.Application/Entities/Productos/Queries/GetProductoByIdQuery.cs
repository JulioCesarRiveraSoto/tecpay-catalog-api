using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Application.Entities.Productos.DTOs;
using TecPay.Catalog.Application.Entities.Productos.Interfaces;

namespace TecPay.Catalog.Application.Entities.Productos.Queries
{
    public sealed record GetProductoByIdQuery(Guid Id) 
        : IRequest<ProductoListaDTO?>;

    public sealed class GetProductoByIdQueryHandler(IProductoRepository productoRepository, IMapper mapper) 
        : IRequestHandler<GetProductoByIdQuery, ProductoListaDTO?>
    {
        public async Task<ProductoListaDTO?> Handle(GetProductoByIdQuery request, CancellationToken cancellationToken)
        {
            var producto = await productoRepository.GetByIdAsync(request.Id, cancellationToken);

            if (producto != null)
            {
                return mapper.Map<ProductoListaDTO>(producto);
            }
            else
            {
                return null;
            }
        }
    }
}
