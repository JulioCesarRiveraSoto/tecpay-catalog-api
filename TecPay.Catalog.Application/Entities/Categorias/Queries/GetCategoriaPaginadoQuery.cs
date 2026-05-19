using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Application.Common;
using TecPay.Catalog.Application.Entities.Categorias.DTOs;
using TecPay.Catalog.Application.Entities.Categorias.Interfaces;

namespace TecPay.Catalog.Application.Entities.Categorias.Queries
{
    public sealed record GetCategoriaPaginadoQuery(string? CategoriaNombre, int NumeroPagina = 1, int TamanioPagina = 10)
        : IRequest<PagedResult<CategoriaListaDTO>>;

    public sealed class GetCategoriaPaginadoQueryHandler(ICategoriaRepository categoriaRepository, IMapper mapper)
        : IRequestHandler<GetCategoriaPaginadoQuery, PagedResult<CategoriaListaDTO>>
    {
        public async Task<PagedResult<CategoriaListaDTO>> Handle(GetCategoriaPaginadoQuery request, CancellationToken cancellationToken)
        {
            var dataPerPage = await categoriaRepository.SearchPaginadoAsync
                (
                    request.CategoriaNombre,
                    Math.Max(1, request.NumeroPagina),
                    Math.Clamp(request.TamanioPagina, 1, 10),
                    cancellationToken
                );

            return new PagedResult<CategoriaListaDTO>
                (
                    dataPerPage.Elementos.Select(mapper.Map<CategoriaListaDTO>).ToList(),
                    dataPerPage.NumeroPagina,
                    dataPerPage.TamanioPagina,
                    dataPerPage.TotalRegistros
                );
        }
    }


    //public sealed record GetCategoriaPaginadoQuery(string? CategoriaNombre, int NumeroPagina = 1, int TamanioPagina = 10)
    //    : IRequest<List<CategoriaListaDTO>>;

    //public sealed class GetCategoriaPaginadoQueryHandler(ICategoriaRepository categoriaRepository, IMapper mapper)
    //    : IRequestHandler<GetCategoriaPaginadoQuery, List<CategoriaListaDTO>>
    //{
    //    public async Task<List<CategoriaListaDTO>> Handle(GetCategoriaPaginadoQuery request, CancellationToken cancellationToken)
    //    {
    //        var dataPerPage = await categoriaRepository.SearchPaginadoAsync
    //            (
    //                request.CategoriaNombre,
    //                Math.Max(1, request.NumeroPagina),
    //                Math.Clamp(request.TamanioPagina, 1, 10),
    //                cancellationToken
    //            );

    //        return mapper.Map<List<CategoriaListaDTO>>(dataPerPage.Elementos);
    //    }
    //}
}
