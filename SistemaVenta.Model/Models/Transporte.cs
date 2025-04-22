using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Model.Models
{
    public partial class Transporte
    {
        public int IdTransporte { get; set; }
        public string? Nombre { get; set; }
        public string? Placa { get; set; }
        public string? Conductor { get; set; }
        public string? Telefono { get; set; }
        public DateTime? FechaRegistro { get; set; }
     
    }
}
