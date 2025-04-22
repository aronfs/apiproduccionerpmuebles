using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class TrazabilidadDTO
    {
        public int Idtraza { get; set; }
        public int? Idproduccion { get; set; }
        public string? Origen { get; set; }
        public string? Destino { get; set; }
        public int? Cantidad { get; set; }
        public string Fecha { get; set; } = string.Empty;
        public string? Etapa { get; set; }
        public string? Accion { get; set; }
        public string? Descripcion { get; set; }
        public string? Usuario { get; set; }
        public string? Cliente { get; set; }
        //public string? CodigoOrden { get; set; } // si querés mostrar en UI el código de orden
    }


}
