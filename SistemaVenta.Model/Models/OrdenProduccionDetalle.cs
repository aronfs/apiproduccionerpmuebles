using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Model.Models
{
    public class OrdenProduccionDetalle
    {
        public int Iddetalle { get; set; }

        public int Idproduccion { get; set; }

        public int Idmateriaprima { get; set; }

        public int CantidadUsada { get; set; }

        // Navegación hacia Orden de Producción
        public OrdenProduccion? Produccion { get; set; }

        // Navegación hacia Producto (Materia Prima)
        public Producto? MateriaPrima { get; set; }
    }

}
