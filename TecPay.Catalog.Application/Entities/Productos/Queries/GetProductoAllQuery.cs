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
    public sealed record GetProductoAllQuery() 
        : IRequest<List<ProductoListaDTO>>;

    public sealed class GetProductoAllQueryHandler(IProductoRepository productoRepository, IMapper mapper) 
        : IRequestHandler<GetProductoAllQuery, List<ProductoListaDTO>>
    {
        public async Task<List<ProductoListaDTO>> Handle(GetProductoAllQuery request, CancellationToken cancellationToken)
        {
            var producto = await productoRepository.GetAllAsync(cancellationToken);

            if (producto != null)
            {
                return mapper.Map<List<ProductoListaDTO>>(producto);
            }
            else
            {
                return null;
            }
        }
    }
}
