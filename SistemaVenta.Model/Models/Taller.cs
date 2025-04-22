using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Model.Models
{
    public class Taller
    {
        public int Idtaller { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Ubicacion { get; set; }
        public string? Telefono { get; set; }
    }

}
