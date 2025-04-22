using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Model.Models
{
    public class OrdenProduccion
    {
        public int Idproduccion { get; set; }

        public string Codigo { get; set; } = string.Empty;

        public int Idproducto { get; set; }

        public int Cantidad { get; set; }

        public string Estado { get; set; } = string.Empty;

        public DateTime Fecharegistro { get; set; }

        // Propiedad de navegación hacia Producto
        public Producto? Producto { get; set; }
        public string? Usuario { get; set; } // <-- Asegúrate de tenerlo acá también

        // Propiedad de navegación hacia detalles
        public ICollection<OrdenProduccionDetalle>? Detalles { get; set; }

        public ICollection<Wip>? Wips{ get; set; } 

        // Relación con trazabilidad si usas navegación inversa
        public ICollection<Trazabilidad>? Trazabilidades { get; set; }

    }

}
