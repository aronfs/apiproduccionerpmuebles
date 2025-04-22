using System;
using System.Collections.Generic;
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
    public class OrdenProduccionService : IOrdenProduccionService
    {
        private readonly IGenericRepository<OrdenProduccion> _ordenProduccionRepository;
        private readonly IMapper _mapper;

        public OrdenProduccionService(IGenericRepository<OrdenProduccion> ordenProduccionRepository, IMapper mapper)
        {
            _ordenProduccionRepository = ordenProduccionRepository;
            _mapper = mapper;
        }

        public async Task<List<OrdenProduccionDTO>> Lista()
        {
            var query = await _ordenProduccionRepository.Consultar(i => i.Idproduccion > 0);

            var queryConIncludes = query
                .Include(i => i.Detalles);

            var lista = await queryConIncludes.ToListAsync();

            return _mapper.Map<List<OrdenProduccionDTO>>(lista);
        }

        public async Task<OrdenProduccionDTO> Crear(OrdenProduccionDTO modelo)
        {
            try
            {
                var ordenProduccionModelo = _mapper.Map<OrdenProduccion>(modelo);

                // Asignar fecha actual
                ordenProduccionModelo.Fecharegistro = DateTime.Now;

                var objetoCreado = await _ordenProduccionRepository.Crear(ordenProduccionModelo);

                if (objetoCreado == null || objetoCreado.Idproduccion == 0)
                    throw new Exception("No se pudo crear la orden");

                return _mapper.Map<OrdenProduccionDTO>(objetoCreado);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al crear la orden", ex);
            }
        }


        public async Task<bool> Editar(OrdenProduccionDTO modelo)
        {
            try
            {
                var ordenExistente = await _ordenProduccionRepository.Obtener(u => u.Idproduccion == modelo.IdProduccion);

                if (ordenExistente == null)
                    throw new Exception("La orden no existe");

                // Mapear solo los campos que quieres actualizar
                ordenExistente.Codigo = modelo.Codigo;
                ordenExistente.Idproducto = modelo.IdProducto;
                ordenExistente.Cantidad = (int)modelo.Cantidad;
                ordenExistente.Estado = modelo.Estado;
                ordenExistente.Fecharegistro = modelo.FechaRegistro;

                bool respuesta = await _ordenProduccionRepository.Editar(ordenExistente);

                return respuesta;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al editar la orden de producción", ex);
            }
        }

        public async Task<bool> Eliminar(int idProduccion)
        {
            try
            {
                var ordenExistente = await _ordenProduccionRepository.Obtener(u => u.Idproduccion == idProduccion);

                if (ordenExistente == null)
                    throw new Exception("La orden no existe");

                bool respuesta = await _ordenProduccionRepository.Eliminar(ordenExistente);

                return respuesta;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al eliminar la orden de producción", ex);
            }
        }


        public async Task<OrdenProduccionDTO> ObtenerPorId(int idProduccion)
        {
            try
            {
                var query = await _ordenProduccionRepository.Consultar(o => o.Idproduccion == idProduccion);

                var ordenConDetalles = await query
                    .Include(o => o.Detalles)
                    .FirstOrDefaultAsync();

                if (ordenConDetalles == null)
                    throw new KeyNotFoundException("Orden de producción no encontrada");

                return _mapper.Map<OrdenProduccionDTO>(ordenConDetalles);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener la orden de producción por ID", ex);
            }
        }

    }
}
