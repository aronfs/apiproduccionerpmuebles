using AutoMapper;
using SistemaVenta.Model.Models;
using SistemaVenta.DTO;
using System.Globalization;

namespace SistemaVenta.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Auth
            CreateMap<AuthDTO, Usuario>().ReverseMap();
            #endregion

            #region Token
            CreateMap<TokenDTO, Usuario>().ReverseMap();
            #endregion

            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion

            #region Usuario
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dest => dest.EsActivo, opt => opt.MapFrom(src => (bool)src.esActivo ? 1 : 0))
                .ForMember(dest => dest.foto, opt => opt.MapFrom(src => src.foto));

            CreateMap<Usuario, SesionDTO>()
                .ForMember(dest => dest.RolDescripcion, opt => opt.MapFrom(src => src.IdRolNavigation != null ? src.IdRolNavigation.Nombre : string.Empty))
                .ForMember(dest => dest.Foto, opt => opt.MapFrom(src => src.foto));

            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(dest => dest.IdRolNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.esActivo, opt => opt.MapFrom(src => src.EsActivo == 1))
                .ForMember(dest => dest.foto, opt => opt.MapFrom(src => src.foto));
            #endregion

            #region Categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            #endregion

            #region Producto

            CreateMap<Producto, ProductoDTO>()
                .ForMember(dest => dest.DescripcionCategoria,
                    opt => opt.MapFrom(src => src.IdCategoriaNavigation != null ? src.IdCategoriaNavigation.Nombre : string.Empty))
                .ForMember(dest => dest.DescripcionTipo,
                    opt => opt.MapFrom(src => src.IdTipoNavigation != null ? src.IdTipoNavigation.Nombre : string.Empty))
                .ForMember(dest => dest.Precio,
                    opt => opt.MapFrom(src => Convert.ToString(src.Precio, new CultureInfo("es-EC"))))
                .ForMember(dest => dest.EsActivo,
                    opt => opt.MapFrom(src => (bool)src.EsActivo ? 1 : 0))
                .ForMember(dest => dest.foto,
                    opt => opt.MapFrom(src => src.Foto));

            CreateMap<ProductoDTO, Producto>()
                .ForMember(dest => dest.IdCategoriaNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.IdTipoNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.Precio,
                    opt => opt.MapFrom(src => Convert.ToDecimal(src.Precio, new CultureInfo("es-EC"))))
                .ForMember(dest => dest.EsActivo,
                    opt => opt.MapFrom(src => src.EsActivo == 1))
                .ForMember(dest => dest.Foto,
                    opt => opt.MapFrom(src => src.foto));

            #endregion


            #region Venta
            CreateMap<Venta, VentaDTO>()
                .ForMember(dest => dest.TotalTexto, opt => opt.MapFrom(src => Convert.ToString(src.Total.GetValueOrDefault(), new CultureInfo("es-EC"))))
                .ForMember(dest => dest.FechaRegistro, opt => opt.MapFrom(src => src.FechaRegistro.HasValue ? src.FechaRegistro.Value.ToString("dd/MM/yyyy") : string.Empty));

            CreateMap<VentaDTO, Venta>()
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => Convert.ToDecimal(src.TotalTexto, new CultureInfo("es-EC"))));
            #endregion

            #region DetalleVenta
            CreateMap<DetalleVenta, DetalleVentaDTO>()
                .ForMember(dest => dest.DescripcionProducto, opt => opt.MapFrom(src => src.IdProductoNavigation != null ? src.IdProductoNavigation.Nombre : string.Empty))
                .ForMember(dest => dest.PrecioTexto, opt => opt.MapFrom(src => Convert.ToString(src.Precio.GetValueOrDefault(), new CultureInfo("es-EC"))))
                .ForMember(dest => dest.TotalTexto, opt => opt.MapFrom(src => Convert.ToString(src.Total.GetValueOrDefault(), new CultureInfo("es-EC"))));

            CreateMap<DetalleVentaDTO, DetalleVenta>()
                .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => Convert.ToDecimal(src.PrecioTexto, new CultureInfo("es-EC"))))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => Convert.ToDecimal(src.TotalTexto, new CultureInfo("es-EC"))));
            #endregion

            #region ReporteVenta
            CreateMap<DetalleVenta, ReporteDTO>()
                .ForMember(dest => dest.FechaRegistro, opt => opt.MapFrom(src =>
                    src.IdVentaNavigation != null && src.IdVentaNavigation.FechaRegistro.HasValue
                        ? src.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy")
                        : string.Empty))
                .ForMember(dest => dest.NumeroDocumento, opt => opt.MapFrom(src =>
                    src.IdVentaNavigation != null ? src.IdVentaNavigation.NumeroDocumento : string.Empty))
                .ForMember(dest => dest.TipoPago, opt => opt.MapFrom(src =>
                    src.IdVentaNavigation != null ? src.IdVentaNavigation.TipoPago : string.Empty))
                .ForMember(dest => dest.TotalVenta, opt => opt.MapFrom(src =>
                    Convert.ToString(src.IdVentaNavigation != null ? src.IdVentaNavigation.Total.GetValueOrDefault() : 0, new CultureInfo("es-EC"))))
                .ForMember(dest => dest.Producto, opt => opt.MapFrom(src =>
                    src.IdProductoNavigation != null ? src.IdProductoNavigation.Nombre : string.Empty))
                .ForMember(dest => dest.Precio, opt => opt.MapFrom(src =>
                    Convert.ToString(src.Precio.GetValueOrDefault(), new CultureInfo("es-EC"))))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src =>
                    Convert.ToString(src.Total.GetValueOrDefault(), new CultureInfo("es-EC"))));
            #endregion

            #region Inventario

            // De DTO a entidad
            CreateMap<InventarioDTO, Inventario>()
                .ForMember(dest => dest.FechaRegistro,
                    opt => opt.MapFrom(src =>
                        !string.IsNullOrEmpty(src.FechaRegistro)
                            ? DateTime.Parse(src.FechaRegistro)
                            : DateTime.Now)) // por si viene vacío
                .ForMember(dest => dest.IdProductoNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.IdBodegaNavigation, opt => opt.Ignore());

            // De entidad a DTO con formato yyyy-MM-dd
            CreateMap<Inventario, InventarioDTO>()
                .ForMember(dest => dest.NombreProducto, opt => opt.MapFrom(src => src.IdProductoNavigation.Nombre))
                .ForMember(dest => dest.NombreBodega, opt => opt.MapFrom(src => src.IdBodegaNavigation.Nombre))
                .ForMember(dest => dest.FechaRegistro,
                    opt => opt.MapFrom(src => src.FechaRegistro.Value.ToString("yyyy-MM-dd")))
             .ForMember(dest => dest.FechaRegistro, opt => opt.MapFrom(src => src.FechaRegistro.HasValue ? src.FechaRegistro.Value.ToString("dd/MM/yyyy") : string.Empty));

            #endregion


            #region Bodega
            CreateMap<Bodega, BodegaDTO>()
                .ForMember(dest => dest.FechaRegistro,
                    opt => opt.MapFrom(src => src.FechaRegistro.HasValue
                        ? src.FechaRegistro.Value.ToString("yyyy-MM-dd")
                        : null));

            CreateMap<BodegaDTO, Bodega>()
                .ForMember(dest => dest.FechaRegistro,
                    opt => opt.MapFrom(src =>
                        !string.IsNullOrEmpty(src.FechaRegistro)
                            ? DateTime.ParseExact(src.FechaRegistro, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            : (DateTime?)null));
            #endregion


            #region Tipo_Mueble
            CreateMap<Tipo_Mueble, Tipo_MuebleDTO>()
                .ForMember(dest => dest.IdTipoMueble, opt => opt.MapFrom(src => src.IdTipoMueble))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.FechaRegistro,
                    opt => opt.MapFrom(src => src.FechaRegistro.ToString("yyyy-MM-dd")));

            CreateMap<Tipo_MuebleDTO, Tipo_Mueble>()
                .ForMember(dest => dest.IdTipoMueble, opt => opt.MapFrom(src => src.IdTipoMueble))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
             .ForMember(dest => dest.FechaRegistro,
                    opt => opt.MapFrom(src =>
                        !string.IsNullOrEmpty(src.FechaRegistro)
                            ? DateTime.ParseExact(src.FechaRegistro, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            : (DateTime?)null));
            #endregion

            #region Comision_SRI

            CreateMap<Comision_SRI, ComisionSRIDTO>()
                .ForMember(dest => dest.NombreProducto,
                    opt => opt.MapFrom(src => src.Producto != null ? src.Producto.Nombre : string.Empty))
                .ForMember(dest => dest.CostoProducto,
                    opt => opt.MapFrom(src => src.Producto != null && src.Producto.Costos.Any()
                        ? src.Producto.Costos.OrderByDescending(c => c.FechaRegistro).First().Monto
                        : 0))
                .ForMember(dest => dest.FechaRegistro,
                    opt => opt.MapFrom(src => src.FechaRegistro.HasValue
                        ? src.FechaRegistro.Value.ToString("yyyy-MM-dd")
                        : string.Empty))
                .ForMember(dest => dest.Descripcion,
                    opt => opt.MapFrom(src => src.Descripcion)); // ✅ Agregado

            CreateMap<ComisionSRIDTO, Comision_SRI>()
                .ForMember(dest => dest.FechaRegistro,
                    opt => opt.MapFrom(src =>
                        !string.IsNullOrEmpty(src.FechaRegistro)
                            ? DateTime.ParseExact(src.FechaRegistro, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            : (DateTime?)null))
                .ForMember(dest => dest.Descripcion,
                    opt => opt.MapFrom(src => src.Descripcion)); // ✅ Agregado también

            #endregion

            #region Materia_Prima
            CreateMap<Materia_Prima, MateriaPrimaDTO>()
            .ForMember(dest => dest.NombreProducto, opt => opt.MapFrom(src => src.Producto.Nombre))
            .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => src.Producto.Precio));

            CreateMap<MateriaPrimaDTO, Materia_Prima>()
                .ForMember(dest => dest.Producto, opt => opt.Ignore()); // Evitamos sobrescribir el producto completo

            #endregion

            #region OrdenProduccion
            // Orden Producción
            CreateMap<OrdenProduccion, OrdenProduccionDTO>()
                .ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.Detalles))
                .ReverseMap();

            // Detalle
            CreateMap<OrdenProduccionDetalle, OrdenProduccionDetalleDTO>().ReverseMap();
            #endregion

            #region Wip
            CreateMap<Wip, WipDTO>()
           .ForMember(dest => dest.NombreOperador, opt => opt.MapFrom(src => src.UsuarioOperador != null ? src.UsuarioOperador.NombreCompleto : null))
           .ForMember(dest => dest.CodigoOrden, opt => opt.MapFrom(src => src.OrdenProduccion != null ? src.OrdenProduccion.Codigo : null))
           .ForMember(dest => dest.Fecha_inicio, opt => opt.MapFrom(src => src.Fecha_inicio.HasValue ? src.Fecha_inicio.Value.ToString("yyyy-MM-dd") : null))
           .ForMember(dest => dest.Fecha_fin, opt => opt.MapFrom(src => src.Fecha_fin.HasValue ? src.Fecha_fin.Value.ToString("yyyy-MM-dd") : null));

            CreateMap<WipDTO, Wip>()
                .ForMember(dest => dest.Fecha_inicio, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Fecha_inicio) ? DateTime.Parse(src.Fecha_inicio) : (DateTime?)null))
                .ForMember(dest => dest.Fecha_fin, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Fecha_fin) ? DateTime.Parse(src.Fecha_fin) : (DateTime?)null));

            #endregion

            #region Taller
            CreateMap<Taller, TallerDTO>().ReverseMap();
            #endregion


            #region Trazabilidad
            CreateMap<Trazabilidad, TrazabilidadDTO>()
        .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha.ToString("yyyy-MM-dd")));

            CreateMap<TrazabilidadDTO, Trazabilidad>()
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => DateTime.Parse(src.Fecha)));


            #endregion

        }
    }

}
