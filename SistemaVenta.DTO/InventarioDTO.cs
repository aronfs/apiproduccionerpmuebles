using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class InventarioDTO
    {
        public int IdInventario { get; set; }
        public int IdProducto { get; set; }
        public int IdBodega { get; set; }
        public int Stock { get; set; }
        public string? FechaRegistro { get; set; }

        public string? NombreProducto { get; set; }  // <- nullable
        public string? NombreBodega { get; set; }    // <- nullable
    }

}

