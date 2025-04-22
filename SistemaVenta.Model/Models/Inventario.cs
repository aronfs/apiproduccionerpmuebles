using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Model.Models
{
    public partial class Inventario
    {
        public int IdInventario { get; set; }
        public int? IdProducto { get; set; }
        public int? IdBodega { get; set; }
        public int? Stock { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public virtual Bodega? IdBodegaNavigation { get; set; }
        public virtual Producto? IdProductoNavigation { get; set; }
    }
}
