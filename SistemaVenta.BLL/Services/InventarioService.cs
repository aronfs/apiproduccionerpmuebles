using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Services.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model.Models;

namespace SistemaVenta.BLL.Services
{
    public class InventarioService : IInventarioService
    {
        private readonly IGenericRepository<Inventario> _repoInventario;
        private readonly IMapper _mapper;

        public InventarioService(IGenericRepository<Inventario> repoInventario, IMapper mapper)
        {
            _repoInventario = repoInventario;
            _mapper = mapper;
        }

        public async Task<List<InventarioDTO>> ListarInventario()
        {
            var query = await _repoInventario.Consultar(i => i.IdInventario > 0);

            var queryConIncludes = query
                .Include(i => i.IdProductoNavigation)
                .Include(i => i.IdBodegaNavigation);

            var listaInventario = await queryConIncludes.ToListAsync();

            return _mapper.Map<List<InventarioDTO>>(listaInventario);
        }




        public async Task<List<InventarioDTO>> FiltrosInventario(string buscarPor, string nombreBodega, string fechaInicio, string fechaFin)
        {
            IQueryable<Inventario> queryInventario = await _repoInventario.Consultar();
            var listaResultado = new List<Inventario>();

            try
            {
                if (buscarPor == "fecha")
                {
                    DateTime fechaInicioConvertida = DateTime.SpecifyKind(
                        DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-EC")),
                        DateTimeKind.Utc);

                    DateTime fechaFinConvertida = DateTime.SpecifyKind(
                        DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-EC")),
                        DateTimeKind.Utc);

                    listaResultado = await queryInventario
                        .Where(v =>
                            v.FechaRegistro >= fechaInicioConvertida.Date &&
                            v.FechaRegistro <= fechaFinConvertida.Date)
                        .Include(v => v.IdProductoNavigation)
                        .Include(v => v.IdBodegaNavigation)
                        .ToListAsync();
                }
                else if (buscarPor == "bodega")
                {
                    listaResultado = await queryInventario
                        .Include(v => v.IdProductoNavigation)
                        .Include(v => v.IdBodegaNavigation)
                        .Where(v => v.IdBodegaNavigation.Nombre.ToLower().Contains(nombreBodega.ToLower()))
                        .ToListAsync();
                }
            }
            catch
            {
                throw; // Aquí podrías agregar logs o un mensaje más detallado si te late
            }

            return _mapper.Map<List<InventarioDTO>>(listaResultado);
        }


        public async Task<InventarioDTO> CrearInventario(InventarioDTO dto)
        {
            var existe = await _repoInventario.Obtener(i =>
                i.IdProducto == dto.IdProducto && i.IdBodega == dto.IdBodega);

            if (existe != null)
                throw new Exception("Ya existe inventario para este producto en esa bodega");

            var entidad = _mapper.Map<Inventario>(dto);

            // Seteo explícito para evitar "FechaRegistro = -infinity"
            entidad.FechaRegistro = DateTime.Now;

            var inventarioCreado = await _repoInventario.Crear(entidad);

            // Consulta con Includes para devolver DTO completo
            var query = await _repoInventario.Consultar(i => i.IdInventario == inventarioCreado.IdInventario);

            var inventarioConNavegacion = await query
                .Include(i => i.IdProductoNavigation)
                .Include(i => i.IdBodegaNavigation)
                .FirstOrDefaultAsync();

            return _mapper.Map<InventarioDTO>(inventarioConNavegacion);
        }


        public async Task<bool> ActualizarInventario(InventarioDTO dto)
        {
            var invExistente = await _repoInventario.Obtener(i => i.IdInventario == dto.IdInventario);

            if (invExistente == null)
                throw new Exception("No se encontró el inventario con el ID especificado");

            // Actualizamos los campos necesarios
            invExistente.Stock = dto.Stock;
            invExistente.FechaRegistro = DateTime.Now; // Fecha de última actualización

            var actualizado = await _repoInventario.Editar(invExistente);

            if (!actualizado)
                throw new Exception("No se pudo actualizar el inventario");

            // Esto es solo para log interno o si decides en un futuro devolver DTO completo
            var query = await _repoInventario.Consultar(i => i.IdInventario == dto.IdInventario);

            var inventarioConNavegacion = await query
                .Include(i => i.IdProductoNavigation)
                .Include(i => i.IdBodegaNavigation)
                .FirstOrDefaultAsync();

            Console.WriteLine($"✅ Inventario actualizado: Producto '{inventarioConNavegacion.IdProductoNavigation.Nombre}', Bodega '{inventarioConNavegacion.IdBodegaNavigation.Nombre}', Fecha '{inventarioConNavegacion.FechaRegistro:yyyy-MM-dd}'");

            return true;
        }



        public async Task<bool> EliminarInventario(int id)
        {
            // Buscar inventario por ID
            var inv = await _repoInventario.Obtener(i => i.IdInventario == id);

            if (inv == null)
            {
                Console.WriteLine($"⚠️ No se encontró inventario con ID {id} para eliminar.");
                return false;
            }

            var eliminado = await _repoInventario.Eliminar(inv);

            if (!eliminado)
            {
                Console.WriteLine($"❌ No se pudo eliminar el inventario con ID {id}");
                return false;
            }

            Console.WriteLine($"✅ Inventario eliminado correctamente: ID {id}");
            return true;
        }

    }


}
