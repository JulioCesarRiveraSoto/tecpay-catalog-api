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

namespace TecPay.Catalog.Application.Entities.Categorias.Commands
{
    public sealed record CreateCategoriaCommand(CategoriaCreacionDTO Categoria) 
        : IRequest<CategoriaListaDTO>;

    public sealed class CreateCategoriaCommandHandler(ICategoriaRepository categoriaRepository, IValidator<CategoriaCreacionDTO> categoriaCreacionValidator, IMapper mapper) 
        : IRequestHandler<CreateCategoriaCommand, CategoriaListaDTO>
    {
        public async Task<CategoriaListaDTO> Handle(CreateCategoriaCommand request, CancellationToken cancellationToken)
        {
            await categoriaCreacionValidator.ValidateAndThrowAsync(request.Categoria, cancellationToken);

            var categoria = new Categoria
                (
                    request.Categoria.CategoriaNombre
                );

            await categoriaRepository.AddAsync(categoria, cancellationToken);
            await categoriaRepository.SaveChangesAsync(cancellationToken);
            return mapper.Map<CategoriaListaDTO>(categoria);
        }

    }
}
