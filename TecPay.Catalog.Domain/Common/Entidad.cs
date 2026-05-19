using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Domain.Interfaces;

namespace TecPay.Catalog.Domain.Common
{
    public abstract class Entidad : IEntidad
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime FechaCreacionUtc { get; set; } = DateTime.UtcNow;

        public DateTime? FechaActualizacionUtc { get; set; }

        public DateTime? FechaBajaUtc { get; set; }

        protected void Touch()
        {
            FechaBajaUtc = DateTime.UtcNow;
        }

    }
}
