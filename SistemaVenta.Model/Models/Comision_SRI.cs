using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Model.Models
{
    public partial class Comision_SRI
    {
        public int IdComision { get; set; }

        public int IdProducto { get; set; } // ✅ Relación con Producto
        public string? Descripcion { get; set; }

        public decimal? Porcentaje { get; set; }

        public decimal? MontoCalculado { get; set; } // ✅ Cálculo final

        public DateTime? FechaRegistro { get; set; }

        public virtual Producto? Producto { get; set; } // navegación opcional
    }
}
