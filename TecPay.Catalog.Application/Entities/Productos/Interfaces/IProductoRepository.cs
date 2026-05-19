using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Application.Common;
using TecPay.Catalog.Domain.Entities;

namespace TecPay.Catalog.Application.Entities.Productos.Interfaces
{
    public interface IProductoRepository
    {
        Task<List<Producto>> SearchAsync(string? buscar, string? categoriaNombre, CancellationToken cancellationToken);
        Task<PagedResult<Producto>> SearchPaginadoAsync(string? buscar, string? categoriaNombre, int numeroPagina, int tamanioPagina, CancellationToken cancellationToken);
        Task<Producto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Producto?>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> ExistsProductoAsync(string productoNombre, Guid? ignorarId, CancellationToken cancellationToken);
        Task<bool> SkuExistsAsync(string productoSKU, Guid? ignorarId, CancellationToken cancellationToken);
        Task AddAsync(Producto producto, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<List<Producto?>> GetComboReloadAsync(CancellationToken cancellationToken);
    }
}
