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
    public class BodegaService : IBodegaService
    {
        private readonly IGenericRepository<Bodega> _bodegaRepository;
        private readonly IMapper _mapper;

        public BodegaService(IGenericRepository<Bodega> bodegaRepository, IMapper mapper)
        {
            _bodegaRepository = bodegaRepository;
            _mapper = mapper;
        }
        public async Task<List<BodegaDTO>> ListarBodega()
        {
            try
            {
                Console.WriteLine("📦 Iniciando consulta de bodegas...");

                var queryBodega = await _bodegaRepository.Consultar();
                var listaBodega = queryBodega.ToList();

                Console.WriteLine($"✅ Se encontraron {listaBodega.Count} bodegas registradas.");

                return _mapper.Map<List<BodegaDTO>>(listaBodega);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al listar bodegas: {ex.Message}");
                throw;
            }
        }
        public async Task<List<BodegaDTO>> FiltrosBodega(string buscarPor, string nombreBodega, string fechaInicio, string fechaFin)
        {
            IQueryable<Bodega> queryBodega = await _bodegaRepository.Consultar();
            var listaResultado = new List<Bodega>();

            try
            {
                string filtro = buscarPor?.Trim().ToLower();

                if (filtro == "fecha")
                {
                    if (string.IsNullOrWhiteSpace(fechaInicio) || string.IsNullOrWhiteSpace(fechaFin))
                        throw new Exception("Las fechas no pueden estar vacías.");

                    DateTime fechaInicioConvertida = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-EC"));
                    DateTime fechaFinConvertida = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-EC"));

                    listaResultado = await queryBodega
                        .Where(b =>
                            b.FechaRegistro.HasValue &&
                            b.FechaRegistro.Value.Date >= fechaInicioConvertida.Date &&
                            b.FechaRegistro.Value.Date <= fechaFinConvertida.Date)
                        .ToListAsync();
                }
                else if (filtro == "bodega")
                {
                    if (string.IsNullOrWhiteSpace(nombreBodega))
                        throw new Exception("El nombre de la bodega no puede estar vacío.");

                    string nombreLower = nombreBodega.ToLower().Trim();

                    listaResultado = await queryBodega
                        .Where(b =>
                            !string.IsNullOrEmpty(b.Nombre) &&
                            b.Nombre.ToLower().Contains(nombreLower))
                        .ToListAsync();
                }
                else
                {
                    throw new Exception("Filtro inválido. Usa 'fecha' o 'bodega'.");
                }
            }
            catch (FormatException fe)
            {
                Console.WriteLine($"❌ Error al convertir fechas: {fe.Message}");
                throw new Exception("Formato de fecha inválido. Usa dd/MM/yyyy");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error en filtro de bodega: {ex.Message}");
                throw;
            }

            return _mapper.Map<List<BodegaDTO>>(listaResultado);
        }


        public async Task<BodegaDTO> CrearBodega(BodegaDTO dto)
        {
            // Verificamos si ya existe una bodega con el mismo ID (opcional si el ID es autogenerado)
            var existe = await _bodegaRepository.Obtener(i => i.IdBodega == dto.IdBodega);
            if (existe != null)
                throw new Exception("❌ Esta Bodega ya está registrada.");

            // Mapeo del DTO a la entidad
            var entidad = _mapper.Map<Bodega>(dto);

            // Asignamos la fecha de registro
            entidad.FechaRegistro = DateTime.Now;

            // Guardamos en la base de datos
            var bodegaCreada = await _bodegaRepository.Crear(entidad);

            // Traemos la bodega creada por ID (por si queremos asegurar datos frescos)
            var query = await _bodegaRepository.Consultar(i => i.IdBodega == bodegaCreada.IdBodega);
            var bodegaFinal = await query.FirstOrDefaultAsync();

            if (bodegaFinal == null)
                throw new Exception("❌ No se pudo recuperar la bodega después de la creación.");

            // Log opcional (puedes usar ILogger en vez de Console)
            Console.WriteLine("✅ Bodega creada exitosamente.");

            // Retornamos el DTO final
            return _mapper.Map<BodegaDTO>(bodegaFinal);
        }


        public async Task<bool> ActualizarBodega(BodegaDTO dto)
        {
            try
            {
                Console.WriteLine($"🔄 Intentando actualizar bodega con ID {dto.IdBodega}...");

                var bodegaExistente = await _bodegaRepository.Obtener(b => b.IdBodega == dto.IdBodega);

                if (bodegaExistente == null)
                {
                    Console.WriteLine("❌ No se encontró la bodega con el ID proporcionado.");
                    throw new Exception("No se encontró la bodega con el ID especificado.");
                }

                // Actualizar campos necesarios
                bodegaExistente.Nombre = dto.Nombre;
                bodegaExistente.Ubicacion = dto.Ubicacion;
                bodegaExistente.FechaRegistro = DateTime.Now; // Puede ser FechaActualización si lo prefieres

                bool actualizado = await _bodegaRepository.Editar(bodegaExistente);

                if (!actualizado)
                {
                    Console.WriteLine("❌ No se pudo actualizar la bodega en la base de datos.");
                    throw new Exception("Ocurrió un error al actualizar la bodega.");
                }

                Console.WriteLine("✅ Bodega actualizada correctamente.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al actualizar bodega: {ex.Message}");
                throw;
            }
        }


        public async Task<bool> EliminarBodega(int id)
        {
            try
            {
                Console.WriteLine($"🗑️ Intentando eliminar bodega con ID {id}...");

                var bodegaExistente = await _bodegaRepository.Obtener(b => b.IdBodega == id);

                if (bodegaExistente == null)
                {
                    Console.WriteLine("❌ No se encontró la bodega con el ID proporcionado.");
                    return false;
                }

                bool eliminado = await _bodegaRepository.Eliminar(bodegaExistente);

                if (!eliminado)
                {
                    Console.WriteLine("❌ No se pudo eliminar la bodega.");
                    throw new Exception("Ocurrió un error al intentar eliminar la bodega.");
                }

                Console.WriteLine("✅ Bodega eliminada correctamente.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al eliminar bodega: {ex.Message}");
                throw;
            }
        }




    }
}
