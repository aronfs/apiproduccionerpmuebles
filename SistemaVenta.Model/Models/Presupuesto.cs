using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Model.Models
{
    public partial class Presupuesto
    {
        public int IdPresupuesto { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Monto { get; set; }
        public int? IdCliente { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public virtual Cliente? Cliente { get; set; }

    }
}
