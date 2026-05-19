using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecPay.Catalog.Application.Common
{
    public class RespuestaAutenticacion
    {
        public bool Ok { get; set; }
        public string Token { get; set; }
        public UsuarioNETDTO Usuario { get; set; }
        public DateTime Expiracion { get; set; }
        public List<Menu> Menu { get; set; }
    }
}
