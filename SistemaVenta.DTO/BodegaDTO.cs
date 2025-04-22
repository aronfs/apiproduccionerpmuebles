using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class BodegaDTO
    {
        public int IdBodega { get; set; }
        public string? Nombre { get; set; }
        public string? Ubicacion { get; set; }
        public string? FechaRegistro { get; set; }
    }
}
