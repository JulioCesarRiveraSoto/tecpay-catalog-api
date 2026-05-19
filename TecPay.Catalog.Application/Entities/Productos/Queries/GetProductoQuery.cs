using AutoMapper;
using MediatR;
using TecPay.Catalog.Application.Entities.Productos.DTOs;
using TecPay.Catalog.Application.Entities.Productos.Interfaces;

namespace TecPay.Catalog.Application.Entities.Productos.Queries
{
    public sealed record GetProductoQuery(string? ProductoNombre)
        : IRequest<List<ProductoListaDTO>>;

    public sealed class GetProductoQueryHandler(IProductoRepository productoRepository, IMapper mapper)
        : IRequestHandler<GetProductoQuery, List<ProductoListaDTO>>
    {
        public async Task<List<ProductoListaDTO>> Handle(GetProductoQuery request, CancellationToken cancellationToken)
        {
            var entities = await productoRepository.SearchAsync
                (
                    request.ProductoNombre,
                    request.ProductoNombre,
                    cancellationToken
                );

            return mapper.Map<List<ProductoListaDTO>>(entities);
        }
    }
}
