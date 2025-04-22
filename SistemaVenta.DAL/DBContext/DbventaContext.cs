using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.Model.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace SistemaVenta.DAL.DBContext;

public partial class DbventaContext : DbContext
{
    public DbventaContext()
    {
    }

    public DbventaContext(DbContextOptions<DbventaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; } // Cambiado a plural para convención
    public virtual DbSet<DetalleVenta> DetalleVentas { get; set; } // Cambiado a plural
    public virtual DbSet<Menu> Menus { get; set; }
    public virtual DbSet<MenuRol> MenuRols { get; set; }
    public virtual DbSet<NumeroDocumento> NumeroDocumentos { get; set; }
    public virtual DbSet<Producto> Productos { get; set; }
    public virtual DbSet<Rol> Rols { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<Venta> Ventas { get; set; } // Cambiado a plural
    public virtual DbSet<Bodega> Bodega { get; set; }
    public virtual DbSet<Cliente> Cliente { get; set; } // Cambiado a plural
    public virtual DbSet<DiasRemision> DiasRemision { get; set; } // Cambiado a plural    
    public virtual DbSet<Inventario> Inventario { get; set; } // Cambiado a plural
    public virtual DbSet<Presupuesto> Presupuesto { get; set; } // Cambiado a plural
    public virtual DbSet<Proveedor> Proveedor { get; set; } // Cambiado a plural
    public virtual DbSet<Tipo_Mueble> Tipo_Mueble { get; set; } // Cambiado a plural
    public virtual DbSet<Transporte> Transporte { get; set; } // Cambiado a plural|
    public virtual DbSet<Comision_SRI> Comision_SRI { get; set; } // Cambiado a plural
    public virtual DbSet<Costo> Costo { get; set; } // Cambiado a plural
    public virtual DbSet<Cliente> Clientes { get; set; } // Cambiado a plural
    public virtual DbSet<Materia_Prima> Materia_Prima { get; set; } // Cambiado a plural
    public virtual DbSet<OrdenProduccion> OrdenProduccion { get; set; }
    public virtual DbSet<OrdenProduccionDetalle> OrdenProduccionDetalle { get; set; } // Cambiado a plural
    public virtual DbSet<Trazabilidad> Trazabilidad { get; set; } // Cambiado a plural
    public virtual DbSet<Wip> Wip { get; set; } // Cambiado a plural
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp"); // Para soporte de UUID si es necesario

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("pk_categoria");
            entity.ToTable("categoria"); // Nombre de tabla en minúsculas para PostgreSQL

            entity.Property(e => e.IdCategoria).HasColumnName("idcategoria");
            entity.Property(e => e.EsActivo)
                .HasDefaultValue(true)
                .HasColumnName("esactivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("NOW()") // Cambiado a NOW() para PostgreSQL
                .HasColumnName("fecharegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("pk_detalleventa");
            entity.ToTable("detalleventa"); // Nombre de tabla en minúsculas

            entity.Property(e => e.IdDetalleVenta).HasColumnName("iddetalleventa");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdProducto).HasColumnName("idproducto");
            entity.Property(e => e.IdVenta).HasColumnName("idventa");
            entity.Property(e => e.Precio)
                .HasColumnType("numeric(10,2)")
                .HasColumnName("precio");
            entity.Property(e => e.Total)
                .HasColumnType("numeric(10,2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdProductoNavigation)
                .WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("fk_detalleventa_producto");

            entity.HasOne(d => d.IdVentaNavigation)
                .WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("fk_detalleventa_venta");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("pk_menu");
            entity.ToTable("menu");

            entity.Property(e => e.IdMenu).HasColumnName("idmenu");
            entity.Property(e => e.Icono)
                .HasMaxLength(50)
                .HasColumnName("icono");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .HasColumnName("url");
        });

        modelBuilder.Entity<MenuRol>(entity =>
        {
            entity.HasKey(e => e.IdMenuRol).HasName("pk_menurol");
            entity.ToTable("menurol");

            entity.Property(e => e.IdMenuRol).HasColumnName("idmenurol");
            entity.Property(e => e.IdMenu).HasColumnName("idmenu");
            entity.Property(e => e.IdRol).HasColumnName("idrol");

            entity.HasOne(d => d.IdMenuNavigation)
                .WithMany(p => p.MenuRols)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("fk_menurol_menu");

            entity.HasOne(d => d.IdRolNavigation)
                .WithMany(p => p.MenuRols)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("fk_menurol_rol");
        });

        modelBuilder.Entity<NumeroDocumento>(entity =>
        {
            entity.HasKey(e => e.IdNumeroDocumento).HasName("pk_numerodocumento");
            entity.ToTable("numerodocumento");

            entity.Property(e => e.IdNumeroDocumento).HasColumnName("idnumerodocumento");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("NOW()")
                .HasColumnName("fecharegistro");
            entity.Property(e => e.UltimoNumero).HasColumnName("ultimo_numero");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("pk_producto");
            entity.ToTable("producto");

            entity.Property(e => e.IdProducto).HasColumnName("idproducto");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.Property(e => e.IdCategoria).HasColumnName("idcategoria");

            entity.Property(e => e.IdTipo).HasColumnName("idtipo"); // ✅ Nuevo campo agregado

            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.Property(e => e.Precio)
                .HasColumnType("numeric(10,2)")
                .HasColumnName("precio");

            entity.Property(e => e.Foto)
                .HasMaxLength(100)
                .HasColumnName("foto");

            entity.Property(e => e.EsActivo)
                .HasDefaultValue(true)
                .HasColumnName("esactivo");

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("NOW()")
                .HasColumnName("fecharegistro");
            entity.Property(e => e.Usuario)
      .HasColumnName("usuario")
      .HasMaxLength(50);
            // Relación con categoría
            entity.HasOne(d => d.IdCategoriaNavigation)
                .WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("producto_idcategoria_fkey");

            // Relación con tipo (opcional, si existe tabla tipo_mueble)
            entity.HasOne(d => d.IdTipoNavigation)
                .WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdTipo)
                .HasConstraintName("producto_idtipo_fkey");
        });


        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("pk_rol");
            entity.ToTable("rol");

            entity.Property(e => e.IdRol).HasColumnName("idrol");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("NOW()")
                .HasColumnName("fecharegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("pk_usuario");
            entity.ToTable("usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("idusuario");
            entity.Property(e => e.Clave)
                .HasMaxLength(40)
                .HasColumnName("clave");
            entity.Property(e => e.Correo)
                .HasMaxLength(40)
                .HasColumnName("correo");
            entity.Property(e => e.esActivo)
                .HasDefaultValue(true)
                .HasColumnName("esactivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("NOW()")
                .HasColumnName("fecharegistro");
            entity.Property(e => e.IdRol).HasColumnName("idrol");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .HasColumnName("nombrecompleto");

            entity.HasOne(d => d.IdRolNavigation)
                .WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("fk_usuario_rol");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("pk_venta");
            entity.ToTable("venta");

            entity.Property(e => e.IdVenta).HasColumnName("idventa");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("NOW()")
                .HasColumnName("fecharegistro");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(40)
                .HasColumnName("numerodocumento");
            entity.Property(e => e.TipoPago)
                .HasMaxLength(50)
                .HasColumnName("tipopago");
            entity.Property(e => e.Total)
                .HasColumnType("numeric(10,2)")
                .HasColumnName("total");
        });


        modelBuilder.Entity<Bodega>(entity =>
        {
            entity.HasKey(e => e.IdBodega).HasName("pk_bodega");
            entity.ToTable("bodega");
            entity.Property(e => e.IdBodega).HasColumnName("idbodega");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(100)
                .HasColumnName("ubicacion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("NOW()")
                .HasColumnName("fecharegistro");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("pk_cliente");
            entity.ToTable("cliente");
            entity.Property(e => e.IdCliente).HasColumnName("idcliente");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Cedula_Ruc)
            .HasMaxLength(100)
            .HasColumnName("cedula_ruc");
            entity.Property(e => e.FechaRegistro)
               .HasDefaultValueSql("NOW()")
               .HasColumnName("fecharegistro"); entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .HasColumnName("direccion");
            entity.Property(e => e.Telefono)
                .HasMaxLength(100)
                .HasColumnName("telefono");
            entity.Property(e => e.FechaRegistro)
               .HasDefaultValueSql("NOW()")
               .HasColumnName("fecharegistro");
        }

        );

        modelBuilder.Entity<DiasRemision>(entity =>
        {
            entity.HasKey(e => e.IdDiasRemision).HasName("pk_diasremision");
            entity.ToTable("dias_remision");
            entity.Property(e => e.IdDiasRemision).HasColumnName("iddia");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.Fecha)
                .HasColumnName("fecha");
            entity.Property(e => e.Observacion)
                .HasMaxLength(100)
                .HasColumnName("observacion");
        }
        );

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdInventario).HasName("pk_inventario");

            entity.ToTable("inventario");

            entity.Property(e => e.IdInventario).HasColumnName("idinventario");
            entity.Property(e => e.IdBodega).HasColumnName("idbodega");
            entity.Property(e => e.IdProducto).HasColumnName("idproducto");

            entity.Property(e => e.Stock)
                .HasColumnType("numeric(10,2)")
                .HasColumnName("stock");

            entity.Property(e => e.FechaRegistro)
                //.HasDefaultValueSql("NOW()")
                .HasColumnName("fecharegistro");

            // 🔗 Relación con Producto
            entity.HasOne(d => d.IdProductoNavigation)
                .WithMany() // o .WithMany(p => p.Inventarios) si lo tenés
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("fk_inventario_producto");

            // 🔗 Relación con Bodega
            entity.HasOne(d => d.IdBodegaNavigation)
                .WithMany() // o .WithMany(b => b.Inventarios) si lo tenés
                .HasForeignKey(d => d.IdBodega)
                .HasConstraintName("fk_inventario_bodega");
        });


        modelBuilder.Entity<Presupuesto>(entity => {
            entity.HasKey(e => e.IdPresupuesto).HasName("pk_presupuesto");
            entity.ToTable("presupuesto");
            entity.Property(e => e.IdPresupuesto).HasColumnName("idpresupuesto");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Monto)
                .HasColumnType("numeric(10,2)")
                .HasColumnName("monto");
            entity.Property(e => e.IdCliente).HasColumnName("idcliente");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("NOW()")
                .HasColumnName("fecharegistro");
            // Relación con Cliente (si la manejas con navegación)
            entity.HasOne(e => e.Cliente)
                .WithMany()
                .HasForeignKey(e => e.IdCliente)
                .HasConstraintName("fk_presupuesto_cliente");

        });

        modelBuilder.Entity<Proveedor>(entity => { 
            entity.HasKey(e => e.IdProveedor).HasName("pk_proveedor");
            entity.ToTable("proveedor");
            entity.Property(e => e.IdProveedor).HasColumnName("idproveedor");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Contacto)
                .HasMaxLength(100)
                .HasColumnName("contacto");
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .HasColumnName("telefono");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .HasColumnName("direccion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("NOW()")
                .HasColumnName("fecharegistro");

        });

        modelBuilder.Entity<Tipo_Mueble>(entity =>
        {
            entity.HasKey(e => e.IdTipoMueble).HasName("pk_tipo_mueble");
            entity.ToTable("tipo_mueble");
            entity.Property(e => e.IdTipoMueble).HasColumnName("idtipo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("NOW()")
                .HasColumnName("fecharegistro");
        });

        modelBuilder.Entity<Transporte>(entity => { 
            entity.HasKey(e => e.IdTransporte).HasName("pk_transporte");
            entity.ToTable("transporte");
            entity.Property(e => e.IdTransporte).HasColumnName("idtransporte");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Placa)
                .HasMaxLength(100)
                .HasColumnName("placa");
            entity.Property(e => e.Conductor)
                .HasMaxLength(100)
                .HasColumnName("conductor");
            entity.Property(e => e.Telefono)
                .HasMaxLength(100)
                .HasColumnName("telefono");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("NOW()")
                .HasColumnName("fecharegistro");
        });
        modelBuilder.Entity<Comision_SRI>(entity =>
        {
            entity.ToTable("comision_sri");

            entity.HasKey(e => e.IdComision);

            entity.Property(e => e.IdComision)
                .HasColumnName("idcomision");

            entity.Property(e => e.Descripcion)
                .HasColumnName("descripcion")
                .HasColumnType("text");

            entity.Property(e => e.Porcentaje)
                .HasColumnName("porcentaje")
                .HasColumnType("decimal(10,2)");

            entity.Property(e => e.FechaRegistro)
            .HasDefaultValueSql("NOW()")
            .HasColumnName("fecharegistro");

            entity.Property(e => e.IdProducto)
                .HasColumnName("idproducto");

            entity.Property(e => e.MontoCalculado)
                .HasColumnName("montocalculado")
                .HasColumnType("decimal(10,2)");

            // Relación con Producto (si aplica)
            // Esto es lo importante:
            entity.HasOne(e => e.Producto)
                .WithMany(p => p.ComisionesSRI) // asegúrate que existe esa colección
                .HasForeignKey(e => e.IdProducto)
                .HasConstraintName("fk_comision_producto")
                .OnDelete(DeleteBehavior.Restrict); // o Cascade, como prefieras
        });


        modelBuilder.Entity<Costo>(entity =>
        {
            entity.HasKey(e => e.IdCosto).HasName("pk_costo");
            entity.ToTable("costo");

            entity.Property(e => e.IdCosto).HasColumnName("idcosto");
            entity.Property(e => e.IdProducto).HasColumnName("idproducto");
            entity.Property(e => e.Tipo)
                  .HasMaxLength(50)
                  .HasColumnName("tipo");
            entity.Property(e => e.Monto)
                  .HasColumnType("numeric(10,2)")
                  .HasColumnName("monto");
            entity.Property(e => e.FechaRegistro)
                  .HasDefaultValueSql("NOW()")
                  .HasColumnName("fecharegistro");

            entity.HasOne(d => d.Producto)
                .WithMany(p => p.Costos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("fk_costo_producto");
        });

        modelBuilder.Entity<Materia_Prima>(entity =>
        {
            entity.ToTable("materia_prima");

            entity.HasKey(e => e.IdProducto);

            entity.Property(e => e.IdProducto)
                  .HasColumnName("idproducto");

            entity.Property(e => e.UnidadMedida)
                  .HasColumnName("unidad_medida")
                  .HasMaxLength(20)
                  .IsRequired();

            entity.Property(e => e.Descripcion)
                  .HasColumnName("descripcion");
            entity.Property(e => e.Usuario)
      .HasColumnName("usuario")
      .HasMaxLength(50); // o el tamaño que consideres necesario


            // Relación uno a uno con Producto
            entity.HasOne(e => e.Producto)
                  .WithOne(p => p.Materia_Prima)
                  .HasForeignKey<Materia_Prima>(e => e.IdProducto)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("materia_prima_idproducto_fkey");
        });

        // Orden_Produccion
        modelBuilder.Entity<OrdenProduccion>(entity =>
        {
            entity.HasKey(e => e.Idproduccion);
            entity.ToTable("orden_produccion");

            entity.Property(e => e.Idproduccion).HasColumnName("idproduccion");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Fecharegistro).HasColumnName("fecharegistro");
            entity.Property(e => e.Usuario)
                    .HasColumnName("usuario")
                    .HasMaxLength(50);
            // FK Producto con delete en cascada
            entity.HasOne(p => p.Producto)
                  .WithMany()
                  .HasForeignKey(p => p.Idproducto)
                  .OnDelete(DeleteBehavior.Cascade); // <--- Aquí va la magia

            // Relación con detalles (ya lo tenías bien)
            entity.HasMany(e => e.Detalles)
                  .WithOne(d => d.Produccion)
                  .HasForeignKey(d => d.Idproduccion)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrdenProduccionDetalle>(entity =>
        {
            entity.HasKey(e => e.Iddetalle);
            entity.ToTable("orden_produccion_detalle");

            entity.Property(e => e.Iddetalle)
                  .HasColumnName("iddetalle");
            entity.Property(e => e.Idproduccion)
                  .HasColumnName("idproduccion");
            entity.Property(e => e.Idmateriaprima)
                  .HasColumnName("idmateriaprima");
            entity.Property(e => e.CantidadUsada)
                  .HasColumnName("cantidad_usada");

            // 1) Relación con OrdenProduccion — BORRADO EN CASCADA
            entity.HasOne(d => d.Produccion)
                  .WithMany(p => p.Detalles)
                  .HasForeignKey(d => d.Idproduccion)
                  .OnDelete(DeleteBehavior.Cascade);

            // 2) Relación con MateriaPrima — BLOQUEAR BORRADO SI HAY DETALLES
            entity.HasOne(d => d.MateriaPrima)
                  .WithMany()  // o .WithMany(mp => mp.OrdenDetalles) si defines la colección
                  .HasForeignKey(d => d.Idmateriaprima)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Trazabilidad>(entity =>
        {
            entity.ToTable("trazabilidad");

            entity.HasKey(e => e.Idtraza);

            entity.Property(e => e.Idtraza)
                  .HasColumnName("idtraza");

            entity.Property(e => e.Idproduccion)
                  .HasColumnName("idproduccion");

            entity.Property(e => e.Origen)
                  .HasColumnName("origen");

            entity.Property(e => e.Destino)
                  .HasColumnName("destino");

            entity.Property(e => e.Cantidad)
                  .HasColumnName("cantidad");

            entity.Property(e => e.Fecha)
                  .HasColumnName("fecha");

            entity.Property(e => e.Etapa)
                  .HasColumnName("etapa");

            entity.Property(e => e.Accion)
                  .HasColumnName("accion");

            entity.Property(e => e.Descripcion)
                  .HasColumnName("descripcion");

            entity.Property(e => e.Usuario)
                  .HasColumnName("usuario");

            entity.Property(e => e.Cliente)
                  .HasColumnName("cliente");

            // Opcional: si manejas FK con orden_produccion
            // entity.HasOne<OrdenProduccion>()
              //     .WithMany()
                //   .HasForeignKey(e => e.Idproduccion)
                  // .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Wip>(entity =>
        {
            entity.ToTable("wip");

            entity.HasKey(e => e.Idwip);

            entity.Property(e => e.Idwip)
                .HasColumnName("idwip");

            entity.Property(e => e.Idproduccion)
                .HasColumnName("idproduccion");

            entity.Property(e => e.Taller)
                .HasColumnName("taller");

            entity.Property(e => e.IdUsuario).HasColumnName("idusuario"); // 👈 nuevo nombre en la tabla

            entity.Property(e => e.Estado)
                .HasColumnName("estado");

            entity.Property(e => e.Fecha_inicio)
                .HasColumnName("fecha_inicio");

            entity.Property(e => e.Fecha_fin)
                .HasColumnName("fecha_fin");
            entity.Property(e => e.Usuario)
                    .HasColumnName("usuario")
                    .HasMaxLength(50);
            // Relaciones
            entity.HasOne(w => w.OrdenProduccion)
                .WithMany(o => o.Wips)
                .HasForeignKey(w => w.Idproduccion)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(w => w.UsuarioOperador)
                .WithMany()
                .HasForeignKey(w => w.IdUsuario)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Taller>(entity =>
        {
            entity.ToTable("taller");

            entity.HasKey(e => e.Idtaller);

            entity.Property(e => e.Idtaller)
                .HasColumnName("idtaller");

            entity.Property(e => e.Nombre)
                .HasColumnName("nombre")
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Descripcion)
                .HasColumnName("descripcion");

            entity.Property(e => e.Ubicacion)
                .HasColumnName("ubicacion")
                .HasMaxLength(150);

            entity.Property(e => e.Telefono)
                .HasColumnName("telefono")
                .HasMaxLength(20);
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}