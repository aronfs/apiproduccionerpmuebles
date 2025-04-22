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
    public class TipoMuebleService : ITipo_MuebleService
    {


        private readonly IGenericRepository<Tipo_Mueble> _tipoMuebleRepository;
        private readonly IMapper _mapper;

        public TipoMuebleService(IGenericRepository<Tipo_Mueble> tipoMuebleRepository, IMapper mapper)
        {
            _tipoMuebleRepository = tipoMuebleRepository;
            _mapper = mapper;
        }
        public async Task<List<Tipo_MuebleDTO>> Lista()
        {
            var query = await _tipoMuebleRepository.Consultar();

            var listaTipoMueble = await query.ToListAsync();

            return _mapper.Map<List<Tipo_MuebleDTO>>(listaTipoMueble);
        }
        public async Task<Tipo_MuebleDTO> Crear(Tipo_MuebleDTO dto)
        {
            // Validación opcional: evitar duplicados por nombre
            var existe = await _tipoMuebleRepository.Obtener(i => i.Nombre.ToLower() == dto.Nombre.ToLower());

            if (existe != null)
                throw new Exception("❌ Ya existe un tipo de mueble con ese nombre.");

            // Mapeo y seteo de fecha
            var entidad = _mapper.Map<Tipo_Mueble>(dto);
            entidad.FechaRegistro = DateTime.Now;

            // Crear en BD
            var tipoMuebleCreado = await _tipoMuebleRepository.Crear(entidad);

            // Consulta final para devolver DTO actualizado
            var query = await _tipoMuebleRepository.Consultar(i => i.IdTipoMueble == tipoMuebleCreado.IdTipoMueble);
            var tipoMuebleFinal = await query.FirstOrDefaultAsync();

            return _mapper.Map<Tipo_MuebleDTO>(tipoMuebleFinal);
        }

        public async Task<bool> Editar(Tipo_MuebleDTO dto)
        {
            // Buscamos el registro existente por ID
            var tipoMuebleExistente = await _tipoMuebleRepository.Obtener(i => i.IdTipoMueble == dto.IdTipoMueble);

            if (tipoMuebleExistente == null)
                throw new Exception("❌ No se encontró el tipo de mueble con el ID especificado");

            // Actualizamos los campos necesarios
            tipoMuebleExistente.Nombre = dto.Nombre;
            tipoMuebleExistente.Descripcion = dto.Descripcion;
            tipoMuebleExistente.FechaRegistro = DateTime.Now; // Fecha de modificación

            var actualizado = await _tipoMuebleRepository.Editar(tipoMuebleExistente);

            if (!actualizado)
                throw new Exception("❌ No se pudo actualizar el tipo de mueble");

            Console.WriteLine($"✅ Tipo de mueble actualizado: {tipoMuebleExistente.Nombre} (ID {tipoMuebleExistente.IdTipoMueble})");

            return true;
        }

        public async Task<bool> Eliminar1(int id)
        {
            // Buscar tipo de mueble por ID
            var tipo = await _tipoMuebleRepository.Obtener(i => i.IdTipoMueble == id);

            if (tipo == null)
            {
                Console.WriteLine($"⚠️ No se encontró tipo de mueble con ID {id} para eliminar.");
                return false;
            }

            var eliminado = await _tipoMuebleRepository.Eliminar(tipo);

            if (!eliminado)
            {
                Console.WriteLine($"❌ No se pudo eliminar el tipo de mueble con ID {id}");
                return false;
            }

            Console.WriteLine($"✅ Tipo de mueble eliminado correctamente: ID {id}");
            return true;
        }





    }
}
