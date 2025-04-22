using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SistemaVenta.BLL.Services.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model.Models;

namespace SistemaVenta.BLL.Services
{
    public class TrazabilidadService : ITrazabilidadService
    {
        private readonly IGenericRepository<Trazabilidad> _trazabilidadRepository;
        private readonly IMapper _mapper;

        public TrazabilidadService(IGenericRepository<Trazabilidad> trazabilidadRepository, IMapper mapper)
        {
            _trazabilidadRepository = trazabilidadRepository;
            _mapper = mapper;
        }

        public async Task<TrazabilidadDTO> Crear(TrazabilidadDTO modelo)
        {
            try
            {
                var entidad = _mapper.Map<Trazabilidad>(modelo);
                var trazaCreada = await _trazabilidadRepository.Crear(entidad);

                if (trazaCreada.Idtraza == 0)
                    throw new TaskCanceledException("No se pudo crear el registro de trazabilidad");

                return _mapper.Map<TrazabilidadDTO>(trazaCreada);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al crear la trazabilidad", ex);
            }
        }

        public async Task<bool> Editar(TrazabilidadDTO modelo)
        {
            try
            {
                var entidad = await _trazabilidadRepository.Obtener(t => t.Idtraza == modelo.Idtraza);
                if (entidad == null)
                    throw new Exception("No se encontró la trazabilidad");

                // Actualización de campos
                entidad.Origen = modelo.Origen;
                entidad.Destino = modelo.Destino;
                entidad.Cantidad = modelo.Cantidad;
                entidad.Idproduccion = modelo.Idproduccion;

                if (DateTime.TryParse(modelo.Fecha, out var fecha))
                    entidad.Fecha = fecha;

                return await _trazabilidadRepository.Editar(entidad);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al editar la trazabilidad", ex);
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var entidad = await _trazabilidadRepository.Obtener(t => t.Idtraza == id);
                if (entidad == null)
                    throw new Exception("No se encontró la trazabilidad");

                return await _trazabilidadRepository.Eliminar(entidad);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al eliminar la trazabilidad", ex);
            }
        }

        public async Task<List<TrazabilidadDTO>> Lista()
        {
            try
            {
                var lista = await _trazabilidadRepository.Consultar();

                return lista.Select(t => new TrazabilidadDTO
                {
                    Idtraza = t.Idtraza,
                    Idproduccion = t.Idproduccion,
                    Origen = t.Origen,
                    Destino = t.Destino,
                    Cantidad = t.Cantidad,
                    Fecha = t.Fecha.ToString("yyyy-MM-dd HH:mm:ss"),
                    Etapa = t.Etapa,
                    Accion = t.Accion,
                    Descripcion = t.Descripcion,
                    Usuario = t.Usuario,
                    Cliente = t.Cliente,
                    //CodigoOrden = t.OrdenProduccion != null ? t.OrdenProduccion.Idproduccion : null
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al listar trazabilidad", ex);
            }
        }

    }
}
