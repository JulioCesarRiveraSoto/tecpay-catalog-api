using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecPay.Catalog.Application.Common
{
    public class FiltroFechaParametroDTO
    {
        public int Pagina { get; set; } = 1;
        public int CantidadRegistrosPorPagina { get; set; } = 10;

        public PaginacionDTO Paginacion
        {
            get
            {
                return new PaginacionDTO()
                {
                    Pagina = Pagina,
                    CantidadRegistrosPorPagina = CantidadRegistrosPorPagina
                };
            }
        }

        public DateTime? FiltroFechaParametroFecha { get; set; }

        public string? FiltroFechaParametroParametro { get; set; }
    }
}
