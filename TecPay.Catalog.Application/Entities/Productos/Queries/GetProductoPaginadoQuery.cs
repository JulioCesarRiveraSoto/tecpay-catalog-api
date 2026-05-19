using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Application.Common;
using TecPay.Catalog.Application.Entities.Categorias.DTOs;
using TecPay.Catalog.Application.Entities.Categorias.Interfaces;
using TecPay.Catalog.Application.Entities.Productos.DTOs;
using TecPay.Catalog.Application.Entities.Productos.Interfaces;

namespace TecPay.Catalog.Application.Entities.Productos.Queries
{
    public sealed record GetProductoPaginadoQuery(string? Buscar, string? CategoriaNombre, int NumeroPagina = 1, int TamanioPagina = 10)
        : IRequest<PagedResult<ProductoListaDTO>>;

    public sealed class GetProductoPaginadoQueryHandler(IProductoRepository productoRepository, IMapper mapper)
        : IRequestHandler<GetProductoPaginadoQuery, PagedResult<ProductoListaDTO>>
    {
        public async Task<PagedResult<ProductoListaDTO>> Handle(GetProductoPaginadoQuery request, CancellationToken cancellationToken)
        {

            var dataPerPage = await productoRepository.SearchPaginadoAsync
                (
                    request.Buscar,
                    request.CategoriaNombre,
                    Math.Max(1, request.NumeroPagina),
                    Math.Clamp(request.TamanioPagina, 1, 100),
                    cancellationToken
                );

            return new PagedResult<ProductoListaDTO>
                (
                    dataPerPage.Elementos.Select(mapper.Map<ProductoListaDTO>).ToList(),
                    dataPerPage.NumeroPagina,
                    dataPerPage.TamanioPagina,
                    dataPerPage.TotalRegistros
                );
        }
    }

    //public sealed record GetProductoPaginadoQuery(string? ProductoNombre, int NumeroPagina = 1, int TamanioPagina = 10)
    //    : IRequest<List<ProductoListaDTO>>;

    //public sealed class GetProductoPaginadoQueryHandler(IProductoRepository productoRepository, IMapper mapper)
    //    : IRequestHandler<GetProductoPaginadoQuery, List<ProductoListaDTO>>
    //{
    //    public async Task<List<ProductoListaDTO>> Handle(GetProductoPaginadoQuery request, CancellationToken cancellationToken)
    //    {
    //        var dataPerPage = await productoRepository.SearchPaginadoAsync
    //            (
    //                request.ProductoNombre,
    //                request.ProductoNombre,
    //                Math.Max(1, request.NumeroPagina),
    //                Math.Clamp(request.TamanioPagina, 1, 10),
    //                cancellationToken
    //            );

    //        return mapper.Map<List<ProductoListaDTO>>(dataPerPage);
    //    }
    //}
}
