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
    public sealed record GetCategoriaComboReloadQuery() 
        : IRequest<List<CategoriaComboDTO?>>;

    public sealed class GetCategoriaComboReloadQueryHandler(ICategoriaRepository categoriaRepository, IMapper mapper) 
        : IRequestHandler<GetCategoriaComboReloadQuery, List<CategoriaComboDTO?>>
    {
        public async Task<List<CategoriaComboDTO?>> Handle(GetCategoriaComboReloadQuery request, CancellationToken cancellationToken)
        {
            var categoria = await categoriaRepository.GetComboReloadAsync(cancellationToken);

            if (categoria != null)
            {
                return mapper.Map<List<CategoriaComboDTO?>>(categoria);
            }
            else
            {
                return null;
            }
        }
    }
}
