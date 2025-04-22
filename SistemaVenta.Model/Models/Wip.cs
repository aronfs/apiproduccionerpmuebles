using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Model.Models
{
    public class Wip
    {
        public int Idwip { get; set; }
        public int Idproduccion { get; set; }
        public int? Taller { get; set; }
     
        public string Estado { get; set; } = "Pendiente";
        public DateTime? Fecha_inicio { get; set; }
        public DateTime? Fecha_fin { get; set; }
        public int? IdUsuario { get; set; }
        // Esta es la propiedad "falsa" solo para el código, no se guarda directamente en la BD
        [NotMapped]
        public int? Operador
        {
            get => IdUsuario;
            set => IdUsuario = value;
        }
        public string? Usuario { get; set; } // <-- Asegúrate de tenerlo acá también

        // Navegaciones opcionales
        public OrdenProduccion? OrdenProduccion { get; set; }
        public Usuario? UsuarioOperador { get; set; }
    }

}
