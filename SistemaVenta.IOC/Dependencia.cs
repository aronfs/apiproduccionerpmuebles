using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DAL.Repositorios;
using SistemaVenta.Utility;
using SistemaVenta.BLL.Services.Contrato;
using SistemaVenta.BLL.Services;
using SistemaVenta.BLL.Servicios.Contrato;
namespace SistemaVenta.IOC
{
    public static class Dependencia
    {

        public static void InyectarDepedencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbventaContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("cadenaSQL"));
            });
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IVentaRepository, VentaRepository>();
            object value = services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IVentaService, VentaService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IInventarioService, InventarioService>();
            services.AddScoped<IBodegaService, BodegaService>();
            services.AddScoped<ITipo_MuebleService, TipoMuebleService>();
            services.AddScoped<IComisionSRIService, ComisionSRIService>();
            services.AddScoped<IMateria_PrimaService, Materia_PrimaService>();
            services.AddScoped<IOrdenProduccionService, OrdenProduccionService>();
            services.AddScoped<IWipService, WipService>();
            services.AddScoped<ITallerService, TallerService>();
            services.AddScoped<ITrazabilidadService, TrazabilidadService>();
        }
    }
}
