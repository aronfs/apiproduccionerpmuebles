using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Model.Models
{
   public partial class Bodega
    {
        public int IdBodega { get; set; }
        public string? Nombre { get; set; }
        public string? Ubicacion { get; set; }
        public DateTime? FechaRegistro { get; set; }

    }
}
