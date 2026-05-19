using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TecPay.Catalog.Application.Entities.Productos.Interfaces;

namespace TecPay.Catalog.Application.Entities.Productos.Commands
{
    public sealed record DeleteProductoCommand(Guid Id) 
        : IRequest<bool>;

    public sealed class DeleteProductoCommandHandler(IProductoRepository productoRepository) 
        : IRequestHandler<DeleteProductoCommand, bool>
    {
        public async Task<bool> Handle(DeleteProductoCommand request, CancellationToken cancellationToken)
        {
            var producto = await productoRepository.GetByIdAsync(request.Id, cancellationToken);

            if (producto is null)
                return false;

            producto.Deactivate();

            await productoRepository.SaveChangesAsync(cancellationToken); 
            
            return true;
        }
    }
}
