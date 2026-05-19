using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Application.Entities.Categorias.DTOs;
using TecPay.Catalog.Application.Entities.Categorias.Interfaces;

namespace TecPay.Catalog.Application.Entities.Categorias.Queries
{
    public sealed record GetCategoriaAllQuery() 
        : IRequest<List<CategoriaListaDTO>>;

    public sealed class GetCategoriaAllQueryHandler(ICategoriaRepository categoriaRepository, IMapper mapper) 
        : IRequestHandler<GetCategoriaAllQuery, List<CategoriaListaDTO>>
    {
        public async Task<List<CategoriaListaDTO>> Handle(GetCategoriaAllQuery request, CancellationToken cancellationToken)
        {
            var categoria = await categoriaRepository.GetAllAsync(cancellationToken);

            if (categoria != null)
            {
                return mapper.Map<List<CategoriaListaDTO>>(categoria);
            }
            else
            {
                return null;
            }
        }
    }
}
