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
    public sealed record GetCategoriaByIdQuery(Guid Id) 
        : IRequest<CategoriaListaDTO?>;

    public sealed class GetCategoriaByIdQueryHandler(ICategoriaRepository categoriaRepository, IMapper mapper)
        : IRequestHandler<GetCategoriaByIdQuery, CategoriaListaDTO?>
    {
        public async Task<CategoriaListaDTO?> Handle(GetCategoriaByIdQuery request, CancellationToken cancellationToken)
        {
            var categoria = await categoriaRepository.GetByIdAsync(request.Id, cancellationToken);

            if (categoria != null)
            {
                return mapper.Map<CategoriaListaDTO>(categoria);
            }
            else
            {
                return null;
            }
        }
    }
}
