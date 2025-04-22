using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Model.Models
{
    public partial class Tipo_Mueble
    {

        public int IdTipoMueble { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaRegistro { get; set; }

        // ✅ Relación inversa: un tipo puede tener muchos productos
        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
