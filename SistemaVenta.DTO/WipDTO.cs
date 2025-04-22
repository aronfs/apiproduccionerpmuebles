using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class WipDTO
    {
        public int Idwip { get; set; }
        public int Idproduccion { get; set; }
        public int? Taller { get; set; }
        public int? IdUsuario { get; set; }
        public string? Estado { get; set; }
        public string? Fecha_inicio { get; set; }
        public string? Fecha_fin { get; set; }
        public string? Usuario { get; set; } // <-- Asegúrate de tenerlo acá también


        // Extras opcionales si quieres mostrar datos relacionados
        public string? NombreOperador { get; set; } // Por si haces join con Usuario
        public string? CodigoOrden { get; set; } // Por si haces join con OrdenProduccion
    }

}
