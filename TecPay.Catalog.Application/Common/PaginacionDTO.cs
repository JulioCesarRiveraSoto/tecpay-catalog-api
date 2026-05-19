using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecPay.Catalog.Application.Common
{
    public class PaginacionDTO
    {
        public int Pagina { get; set; } = 1;
        private int cantidadregistrosPorPagina = 10;
        private readonly int cantidadMaximaRegistrosPorPagina = 50;

        public int CantidadRegistrosPorPagina
        {
            get
            {
                return cantidadregistrosPorPagina;
            }
            set
            {
                cantidadregistrosPorPagina = (value > cantidadMaximaRegistrosPorPagina) ? cantidadMaximaRegistrosPorPagina : value;
            }
        }
    }
}
