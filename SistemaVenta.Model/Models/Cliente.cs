using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Model.Models
{
    public partial class Cliente
    {
        public int IdCliente { get; set; }
        public string? Nombre { get; set; }
        public string? Cedula_Ruc { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
