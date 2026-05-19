using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecPay.Catalog.Application.Common
{
    public class Menu
    {
        public string Titulo { get; set; }
        public string Icono { get; set; }
        public List<SubMenu> SubMenu { get; set; }
    }
}
