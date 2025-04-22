using System;
using System.Collections.Generic;

namespace SistemaVenta.Model.Models;

public class Producto
{
    public int IdProducto { get; set; }

    public string? Nombre { get; set; }

    public int? IdCategoria { get; set; }

    public int? IdTipo { get; set; } // ✅ Campo para el tipo de mueble

    public int? Stock { get; set; }

    public decimal? Precio { get; set; }

    public bool? EsActivo { get; set; }

    public string? Foto { get; set; }
    public string? Usuario { get; set; } // <-- Asegúrate de tenerlo acá también


    public DateTime? FechaRegistro { get; set; }

    // 🔗 Relaciones de navegación
    public virtual Tipo_Mueble? IdTipoNavigation { get; set; }

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual ICollection<Comision_SRI> ComisionesSRI { get; set; } = new List<Comision_SRI>();

    public virtual ICollection<Costo> Costos { get; set; } = new List<Costo>();
    public virtual Materia_Prima? Materia_Prima { get; set; }
}
