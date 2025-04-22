using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class OrdenProduccionDTO
    {
        public int IdProduccion { get; set; }
        public string Codigo { get; set; }
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public string Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string? Usuario { get; set; } // <-- Asegúrate de tenerlo acá también

        public List<OrdenProduccionDetalleDTO> Detalles { get; set; } = new();
    }

}
