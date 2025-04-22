using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Model.Models
{
    public class Costo
    {
        public int IdCosto { get; set; }

        public int IdProducto { get; set; }

        public string Tipo { get; set; } = string.Empty;

        public decimal Monto { get; set; }

        public DateTime FechaRegistro { get; set; }

        public virtual Producto? Producto { get; set; }
    }
}
