using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.Utilidad;
using SistemaVenta.BLL.Services;
using SistemaVenta.BLL.Services.Contrato;
using SistemaVenta.DTO;

namespace SistemaVenta.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenProduccionController : ControllerBase
    {
        private readonly IOrdenProduccionService _ordenProduccionService;

        public OrdenProduccionController(IOrdenProduccionService ordenProduccionService)
        {
            _ordenProduccionService = ordenProduccionService;
        }


        [HttpGet("ObtenerIDOrdenProduccion")]
        public async Task<IActionResult> ObtenerPorIdProduccion(int id)
        {
            var busquedad = await _ordenProduccionService.ObtenerPorId(id);

            if (busquedad != null)
            {
                return Ok(new { status = true, busquedad });
            }

            return NotFound(new { status = false, message = "No encontrado" });
        }


        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<OrdenProduccionDTO>>();
            try
            {
                response.status = true;
                response.Value = await _ordenProduccionService.Lista();

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
        public async Task<IActionResult> Guardar([FromBody] OrdenProduccionDTO modelo)
        {
            var response = new Response<OrdenProduccionDTO>();
            try
            {

                response.status = true;
                response.Value = await _ordenProduccionService.Crear(modelo);

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
        public async Task<IActionResult> Editar([FromBody] OrdenProduccionDTO modelo)
        {
            var response = new Response<bool>();
            try
            {
                response.status = true;
                response.Value = await _ordenProduccionService.Editar(modelo);
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
                response.Value = await _ordenProduccionService.Eliminar(id);
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
