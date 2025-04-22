using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class ComisionSRIDTO
    {
        public int IdComision { get; set; }
        public int IdProducto { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal MontoCalculado { get; set; }
        // Fecha como string (ej: "2025-04-18" o con hora si quieres)
        public string? Descripcion { get; set; }

        public string FechaRegistro { get; set; } = string.Empty;

        public string? NombreProducto { get; set; }
        public decimal? CostoProducto { get; set; }
    }
}
