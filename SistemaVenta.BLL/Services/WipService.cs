using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SistemaVenta.BLL.Services.Contrato;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model.Models;

namespace SistemaVenta.BLL.Services
{
    public class WipService : IWipService
    {
       
        private readonly ITallerService _tallerRepository;
        private readonly IGenericRepository<Wip> _wipRepository;
        private readonly IMapper _mapper;

        public WipService(IGenericRepository<Wip> wipRepository, IMapper mapper, ITallerService tallerRepository)
        {
            _wipRepository = wipRepository;
            _mapper = mapper;
            _tallerRepository = tallerRepository;
        }

        public async Task<List<WipDTO>> Listar()
        {
            try
            {
                var lista = await _wipRepository.Consultar();

                // Reemplazar el operador de propagación NULL con una verificación explícita
                var listaMapeada = lista.Select(w => new WipDTO
                {
                    Idwip = w.Idwip,
                    Idproduccion = w.Idproduccion,
                    Taller = w.Taller,
                    IdUsuario = w.IdUsuario,
                    Estado = w.Estado,
                    Fecha_inicio = w.Fecha_inicio.HasValue ? w.Fecha_inicio.Value.ToString("yyyy-MM-dd") : null,
                    Fecha_fin = w.Fecha_fin.HasValue ? w.Fecha_fin.Value.ToString("yyyy-MM-dd") : null,

                    // Datos relacionados (opcional, si están disponibles)
                    NombreOperador = w.UsuarioOperador != null ? w.UsuarioOperador.NombreCompleto : null,
                    CodigoOrden = w.OrdenProduccion != null ? w.OrdenProduccion.Codigo : null
                }).ToList();

                return listaMapeada;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al listar registros WIP", ex);
            }
        }
        public async Task<bool> Actualizar(WipDTO dto)
        {
            try
            {
                var wipModelo = await _wipRepository.Obtener(w => w.Idwip == dto.Idwip);

                if (wipModelo == null)
                    throw new Exception("No se encontró el registro WIP");

                if (dto.Fecha_inicio != null)
                {
                    if (DateTime.TryParse(dto.Fecha_inicio, out var fechaInicio))
                        wipModelo.Fecha_inicio = fechaInicio;
                }

                if (dto.Fecha_fin != null)
                {
                    if (DateTime.TryParse(dto.Fecha_fin, out var fechaFin))
                        wipModelo.Fecha_fin = fechaFin;
                }

                bool respuesta = await _wipRepository.Editar(wipModelo);
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al actualizar fechas del WIP", ex);
            }
        }


        public async Task<bool> AsignarOperadorOWip(WipDTO dto)
        {
            try
            {
                if (dto.Taller.HasValue)
                {
                    var listaTalleres = await _tallerRepository.Lista();
                    var existeTaller = listaTalleres.FirstOrDefault(t => t.Idtaller == dto.Taller.Value);
                    if (existeTaller == null)
                        throw new KeyNotFoundException($"No existe un taller con id = {dto.Taller}");
                }

                var wipModelo = await _wipRepository.Obtener(w => w.Idwip == dto.Idwip);
                if (wipModelo == null)
                    throw new Exception("No se encontró el registro WIP");

                wipModelo.IdUsuario = dto.IdUsuario;
                wipModelo.Taller = dto.Taller;
                wipModelo.Usuario = dto.Usuario; // 💥 ¡Aquí sí se guarda el usuario que hace el cambio!

                if (dto.Fecha_inicio != null && DateTime.TryParse(dto.Fecha_inicio, out var fechaInicio))
                    wipModelo.Fecha_inicio = fechaInicio;

                if (dto.Fecha_fin != null && DateTime.TryParse(dto.Fecha_fin, out var fechaFin))
                    wipModelo.Fecha_fin = fechaFin;

                return await _wipRepository.Editar(wipModelo);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al asignar operador al WIP", ex);
            }
        }



        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var wipModelo = await _wipRepository.Obtener(w => w.Idwip == id);

                if (wipModelo == null)
                    throw new Exception("No se encontró el registro WIP");

                bool respuesta = await _wipRepository.Eliminar(wipModelo);
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al eliminar el WIP", ex);
            }
        }
        public async Task<List<WipDTO>> Filtros(string buscarPor, string nombre, string fechaInicio, string fechaFin)
        {
            try
            {
                DateTime? fechaInicioParsed = null;
                DateTime? fechaFinParsed = null;

                if (DateTime.TryParse(fechaInicio, out var fi))
                    fechaInicioParsed = fi;

                if (DateTime.TryParse(fechaFin, out var ff))
                    fechaFinParsed = ff;

                var lista = await _wipRepository.Consultar(w =>
                    (string.IsNullOrWhiteSpace(nombre) || (buscarPor == "operador" && w.UsuarioOperador.NombreCompleto.ToLower().Contains(nombre.ToLower()))) &&
                    (
                        (!fechaInicioParsed.HasValue || !w.Fecha_inicio.HasValue || w.Fecha_inicio.Value.Date >= fechaInicioParsed.Value.Date) &&
                        (!fechaFinParsed.HasValue || !w.Fecha_fin.HasValue || w.Fecha_fin.Value.Date <= fechaFinParsed.Value.Date)
                    )
                );

                var listaMapeada = lista.Select(w => new WipDTO
                {
                    Idwip = w.Idwip,
                    Idproduccion = w.Idproduccion,
                    Taller = w.Taller,
                    IdUsuario = w.IdUsuario,
                    Estado = w.Estado,
                    Fecha_inicio = w.Fecha_inicio.HasValue ? w.Fecha_inicio.Value.ToString("yyyy-MM-dd") : null,
                    Fecha_fin = w.Fecha_fin.HasValue ? w.Fecha_fin.Value.ToString("yyyy-MM-dd") : null,
                    NombreOperador = w.UsuarioOperador != null ? w.UsuarioOperador.NombreCompleto : null,
                    CodigoOrden = w.OrdenProduccion != null ? w.OrdenProduccion.Codigo : null
                }).ToList();

                return listaMapeada;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al filtrar registros WIP", ex);
            }
        }




    }
}
