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
    public sealed record ExistsUpdateCategoriaCommand(Guid Id, CategoriaCreacionDTO Categoria) 
        : IRequest<bool>;

    public sealed class ExistsUpdateCategoriaCommandHandler(ICategoriaRepository categoriaRepository) 
        : IRequestHandler<ExistsUpdateCategoriaCommand, bool>
    {
        public async Task<bool> Handle(ExistsUpdateCategoriaCommand request, CancellationToken cancellationToken)
        {
            return await categoriaRepository.ExistsCategoriaAsync(request.Categoria.CategoriaNombre, request.Id, cancellationToken);
        }

    }
}
