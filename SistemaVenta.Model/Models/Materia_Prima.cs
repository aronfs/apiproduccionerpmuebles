using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Model.Models
{
    public class Materia_Prima
    {
        public int IdProducto { get; set; } // PK y FK de Producto

        public string UnidadMedida { get; set; } = null!;

        public string? Descripcion { get; set; }

        // Relación: 1 a 1 con Producto
        public virtual Producto? Producto { get; set; }
        public string? Usuario { get; set; } // <-- Asegúrate de tenerlo acá también
    }

}
