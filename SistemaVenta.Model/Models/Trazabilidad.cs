using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Model.Models
{
    public class Trazabilidad
    {
        public int Idtraza { get; set; }
        public int? Idproduccion { get; set; }
        public string? Origen { get; set; }
        public string? Destino { get; set; }
        public int? Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public string? Etapa { get; set; }
        public string? Accion { get; set; }
        public string? Descripcion { get; set; }
        public string? Usuario { get; set; }
        public string? Cliente { get; set; }

        // Opcional: puedes relacionarlo con OrdenProduccion si usas navigation properties
         //public OrdenProduccion? OrdenProduccion { get; set; }
    }

}
