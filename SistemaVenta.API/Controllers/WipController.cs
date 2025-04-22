using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.Utilidad;
using SistemaVenta.BLL.Services;
using SistemaVenta.BLL.Services.Contrato;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;

namespace SistemaVenta.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WipController : ControllerBase
    {

        private readonly IWipService _wipService;
        public WipController(IWipService wipService)
        {
            _wipService = wipService;
        }


        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<WipDTO>>();
            try
            {
                response.status = true;
                response.Value = await _wipService.Listar();

            }
            catch (Exception ex)
            {
                response.status = false;
                response.msg = ex.Message;
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("AsignarOperador")]
        public async Task<IActionResult> AsignarOperadorOWip([FromBody] WipDTO modelo)
        {
            var response = new Response<bool>();
            try
            {
                response.status = true;
                response.Value = await _wipService.AsignarOperadorOWip(modelo);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.msg = ex.Message;
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] WipDTO modelo)
        {
            var response = new Response<bool>();
            try
            {
                response.status = true;
                response.Value = await _wipService.Actualizar(modelo);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.msg = ex.Message;
            }
            return Ok(response);
        }


        [HttpGet]
        [Route("Filtros")]
        public async Task<IActionResult> Filtros(string buscarPor, string? nombre, string? fechaInicio, string? fechaFin)
        {
            var response = new Response<List<WipDTO>>();
            nombre = nombre is null ? "" : nombre;
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;
            try
            {
                response.status = true;
                response.Value = await _wipService.Filtros(buscarPor, nombre, fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.msg = ex.Message;
            }
            return Ok(response);
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var response = new Response<bool>();
            try
            {
                response.status = true;
                response.Value = await _wipService.Eliminar(id);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.msg = ex.Message;
            }
            return Ok(response);
        }

    }
}