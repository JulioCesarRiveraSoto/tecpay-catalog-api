using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TecPay.Catalog.Application.Entities.Categorias.Interfaces;

namespace TecPay.Catalog.Application.Entities.Categorias.Commands
{
    public sealed record DeleteCategoriaCommand(Guid Id) 
        : IRequest<bool>;

    public sealed class DeleteCategoriaCommandHandler(ICategoriaRepository categoriaRepository) 
        : IRequestHandler<DeleteCategoriaCommand, bool>
    {
        public async Task<bool> Handle(DeleteCategoriaCommand request, CancellationToken cancellationToken)
        {
            var categoria = await categoriaRepository.GetByIdAsync(request.Id, cancellationToken);

            if (categoria is null)
                return false;

            categoria.Deactivate();

            await categoriaRepository.SaveChangesAsync(cancellationToken); 
            
            return true;
        }
    }
}
