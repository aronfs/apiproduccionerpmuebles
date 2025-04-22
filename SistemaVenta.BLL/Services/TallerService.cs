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
    public class TallerService : ITallerService
    {
        private readonly IGenericRepository<Taller> _tallerRepository;
        private readonly IMapper _mapper;

        public TallerService(IGenericRepository<Taller> tallerRepository, IMapper mapper)
        {
            _tallerRepository = tallerRepository;
            _mapper = mapper;
        }

        public async Task<List<TallerDTO>> Lista()
        {
            try
            {
                var lista = await _tallerRepository.Consultar();
                var listaDTO = _mapper.Map<List<TallerDTO>>(lista);
                return listaDTO;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener la lista de talleres", ex);
            }
        }

        public async Task<TallerDTO> Crear(TallerDTO modelo)
        {
            try
            {
                var entidad = _mapper.Map<Taller>(modelo);
                var tallerCreado = await _tallerRepository.Crear(entidad);

                if (tallerCreado.Idtaller == 0)
                    throw new TaskCanceledException("No se pudo crear el taller");

                return _mapper.Map<TallerDTO>(tallerCreado);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al crear el taller", ex);
            }
        }

        public async Task<bool> Editar(TallerDTO modelo)
        {
            try
            {
                var entidad = await _tallerRepository.Obtener(t => t.Idtaller == modelo.Idtaller);
                if (entidad == null)
                    throw new KeyNotFoundException("No se encontró el taller");

                // Actualizamos campos
                entidad.Nombre = modelo.Nombre;
                entidad.Descripcion = modelo.Descripcion;
                entidad.Ubicacion = modelo.Ubicacion;
                entidad.Telefono = modelo.Telefono;

                return await _tallerRepository.Editar(entidad);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al editar el taller", ex);
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var entidad = await _tallerRepository.Obtener(t => t.Idtaller == id);
                if (entidad == null)
                    throw new KeyNotFoundException("No se encontró el taller");

                return await _tallerRepository.Eliminar(entidad);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al eliminar el taller", ex);
            }
        }


    }
}
