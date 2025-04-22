using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Model.Models
{
    public partial class Proveedor
    {
        public int IdProveedor { get; set; }
        public string? Nombre { get; set; }
        public string? Contacto { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }
        public DateTime? FechaRegistro { get; set; }
       
    }
}
