using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecPay.Catalog.Application.Common
{
    public sealed record PagedResult<T>(IReadOnlyList<T> Elementos, int NumeroPagina, int TamanioPagina, int TotalRegistros)
    {
        public int PaginasTotal => (int)Math.Ceiling(TotalRegistros / (double)TamanioPagina);
    }
}
