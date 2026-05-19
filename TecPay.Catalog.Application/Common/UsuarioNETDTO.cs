using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecPay.Catalog.Application.Common
{
    public class UsuarioNETDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Email { get; set; }

        public string Img { get; set; }

        public string Role { get; set; }

        public bool Google { get; set; }


    }
}
