using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Application.Common;
using TecPay.Catalog.Domain.Entities;

namespace TecPay.Catalog.Application.Entities.Categorias.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> SearchAsync(string? categoriaNombre, CancellationToken cancellationToken);
        Task<PagedResult<Categoria>> SearchPaginadoAsync(string? categoriaNombre, int numeroPagina, int tamanioPagina, CancellationToken cancellationToken);
        Task<Categoria?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Categoria?>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> ExistsCategoriaAsync(string categoriaNombre, Guid? ignorarId, CancellationToken cancellationToken);
        Task AddAsync(Categoria categoria, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<List<Categoria?>> GetComboReloadAsync(CancellationToken cancellationToken);
    }
}
