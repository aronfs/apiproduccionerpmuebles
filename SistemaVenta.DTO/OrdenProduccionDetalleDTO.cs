using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class OrdenProduccionDetalleDTO
    {
        public int IdDetalle { get; set; }
        public int IdProduccion { get; set; } // se llena automático desde el padre
        public int IdMateriaPrima { get; set; }
        public decimal CantidadUsada { get; set; }
    }

}
