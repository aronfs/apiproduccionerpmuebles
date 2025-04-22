using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class MateriaPrimaDTO
    {
        public int IdProducto { get; set; }
        public string? UnidadMedida { get; set; }
        public string? Descripcion { get; set; }

        // Opcional: si querés incluir algo del producto relacionado (nombre, precio, etc.)
        public string? NombreProducto { get; set; }

        public decimal? Precio { get; set; }

        public string? Usuario { get; set; } // <-- Asegúrate de tener esto
    }

}
