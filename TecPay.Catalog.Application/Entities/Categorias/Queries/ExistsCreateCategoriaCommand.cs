using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Application.Entities.Categorias.DTOs;
using TecPay.Catalog.Application.Entities.Categorias.Interfaces;
using TecPay.Catalog.Domain.Entities;

namespace TecPay.Catalog.Application.Entities.Categorias.Queries
{
    public sealed record ExistsCreateCategoriaCommand(CategoriaCreacionDTO Categoria) 
        : IRequest<bool>;

    public sealed class ExistsCreateCategoriaCommandHandler(ICategoriaRepository categoriaRepository) 
        : IRequestHandler<ExistsCreateCategoriaCommand, bool>
    {
        public async Task<bool> Handle(ExistsCreateCategoriaCommand request, CancellationToken cancellationToken)
        {
            return await categoriaRepository.ExistsCategoriaAsync(request.Categoria.CategoriaNombre, null, cancellationToken);
        }

    }
}
