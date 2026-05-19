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

namespace TecPay.Catalog.Application.Entities.Categorias.Commands
{
    public sealed record UpdateCategoriaCommand(Guid Id, CategoriaCreacionDTO Categoria) 
        : IRequest<CategoriaListaDTO?>;

    public sealed class UpdateCategoriaCommandHandler(ICategoriaRepository categoriaRepository, IValidator<CategoriaCreacionDTO> categoriaCreacionValidator, IMapper mapper) 
        : IRequestHandler<UpdateCategoriaCommand, CategoriaListaDTO?>
    {
        public async Task<CategoriaListaDTO?> Handle(UpdateCategoriaCommand request, CancellationToken cancellationToken)
        {
            await categoriaCreacionValidator.ValidateAndThrowAsync(request.Categoria, cancellationToken);

            var entity = await categoriaRepository.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null)
                return null;

            entity.Update
                (
                    request.Categoria.CategoriaNombre
                );
            await categoriaRepository.SaveChangesAsync(cancellationToken);
            return mapper.Map<CategoriaListaDTO>(entity);
        }
    }
}
