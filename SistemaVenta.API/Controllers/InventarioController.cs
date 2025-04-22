using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.BLL.Services.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad;
using SistemaVenta.BLL.Services;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventarioController : ControllerBase
    {
        private readonly IInventarioService _inventarioService;

        public InventarioController(IInventarioService inventarioService)
        {
            _inventarioService = inventarioService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<InventarioDTO>>();
            try
            {
                response.status = true;
                response.Value = await _inventarioService.ListarInventario();
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
            var response = new Response<List<InventarioDTO>>();
            nombreBodega = nombreBodega is null ? "" : nombreBodega;
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;
            try
            {
                response.status = true;
                response.Value = await _inventarioService.FiltrosInventario(buscarPor, nombreBodega, fechaInicio, fechaFin);
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
        public async Task<IActionResult> Guardar([FromBody] InventarioDTO modelo)
        {
            var response = new Response<InventarioDTO>();
            try
            {
                response.status = true;
                response.Value = await _inventarioService.CrearInventario(modelo);
                response.msg = "Inventario registrado correctamente";
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
        public async Task<IActionResult> Editar([FromBody] InventarioDTO modelo)
        {
            var response = new Response<bool>();
            try
            {
                response.status = true;
                response.Value = await _inventarioService.ActualizarInventario(modelo);
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
                response.Value = await _inventarioService.EliminarInventario(id);
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
