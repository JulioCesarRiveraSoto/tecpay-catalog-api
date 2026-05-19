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
    public sealed record GetProductoComboReloadQuery() 
        : IRequest<List<ProductoComboDTO?>>;

    public sealed class GetProductoComboReloadQueryHandler(IProductoRepository productoRepository, IMapper mapper) 
        : IRequestHandler<GetProductoComboReloadQuery, List<ProductoComboDTO?>>
    {
        public async Task<List<ProductoComboDTO?>> Handle(GetProductoComboReloadQuery request, CancellationToken cancellationToken)
        {
            var producto = await productoRepository.GetComboReloadAsync(cancellationToken);

            if (producto != null)
            {
                return mapper.Map<List<ProductoComboDTO?>>(producto);
            }
            else
            {
                return null;
            }
        }
    }
}
