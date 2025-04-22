using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Model.Models
{
    public partial class DiasRemision
    {
        public int IdDiasRemision { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Observacion { get; set; }
    }
}
