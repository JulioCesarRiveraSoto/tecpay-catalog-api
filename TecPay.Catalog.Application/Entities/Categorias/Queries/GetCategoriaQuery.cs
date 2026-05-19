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
    public sealed record GetCategoriaQuery(string? CategoriaNombre)
        : IRequest<List<CategoriaListaDTO>>;

    public sealed class GetCategoriaQueryHandler(ICategoriaRepository categoriaRepository, IMapper mapper)
        : IRequestHandler<GetCategoriaQuery, List<CategoriaListaDTO>>
    {
        public async Task<List<CategoriaListaDTO>> Handle(GetCategoriaQuery request, CancellationToken cancellationToken)
        {
            var entities = await categoriaRepository.SearchAsync
                (
                    request.CategoriaNombre,
                    cancellationToken
                );

            return mapper.Map<List<CategoriaListaDTO>>(entities);
        }
    }
}
