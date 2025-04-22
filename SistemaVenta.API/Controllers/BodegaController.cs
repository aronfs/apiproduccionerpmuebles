using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.Utilidad;
using SistemaVenta.BLL.Services.Contrato;
using SistemaVenta.DTO;

namespace SistemaVenta.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BodegaController : ControllerBase
    {

        private readonly IBodegaService _bodegaService;

        public BodegaController(IBodegaService bodegaService)
        {
            _bodegaService = bodegaService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<BodegaDTO>>();
            try
            {
                response.status = true;
                response.Value = await _bodegaService.ListarBodega();
            }
            catch (Exception ex)
            {
                response.status = false;
                response.msg = ex.Message;
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("FiltosBusquedad")]
        public async Task<IActionResult> FiltroBusquedad(string buscarPor, string? nombreBodega, string? fechaInicio, string? fechaFin)
        {
            var response = new Response<List<BodegaDTO>>();
            nombreBodega = nombreBodega is null ? "" : nombreBodega;
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;
            try
            {
                response.status = true;
                response.Value = await _bodegaService.FiltrosBodega(buscarPor, nombreBodega, fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.msg = ex.Message;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] BodegaDTO modelo)
        {
            var response = new Response<BodegaDTO>();
            try
            {
                response.status = true;
                response.Value = await _bodegaService.CrearBodega(modelo);
                response.msg = "Bodega registrado correctamente";
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
        public async Task<IActionResult> Editar([FromBody] BodegaDTO modelo)
        {
            var response = new Response<bool>();
            try
            {
                response.status = true;
                response.Value = await _bodegaService.ActualizarBodega(modelo);
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
                response.Value = await _bodegaService.EliminarBodega(id);
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
