using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecPay.Catalog.Domain.Interfaces
{
    public interface IEntidad
    {
        public Guid Id { get; set; }

        public DateTime FechaCreacionUtc { get; set; } 

        public DateTime? FechaActualizacionUtc { get; set; }

        public DateTime? FechaBajaUtc { get; set; }
    }
}
