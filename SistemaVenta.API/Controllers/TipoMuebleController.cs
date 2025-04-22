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
    public class TipoMuebleController : ControllerBase
    {


        private readonly ITipo_MuebleService _tipoMuebleService;

        public TipoMuebleController(ITipo_MuebleService tipoMuebleService)
        {
            _tipoMuebleService = tipoMuebleService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<Tipo_MuebleDTO>>();
            try
            {
                response.status = true;
                response.Value = await _tipoMuebleService.Lista();
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
        public async Task<IActionResult> Guardar([FromBody] Tipo_MuebleDTO modelo)
        {
            var response = new Response<Tipo_MuebleDTO>();
            try
            {
                response.status = true;
                response.Value = await _tipoMuebleService.Crear(modelo);
                response.msg = "Tipo Mueble registrado correctamente";
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
        public async Task<IActionResult> Editar([FromBody] Tipo_MuebleDTO modelo)
        {
            var response = new Response<bool>();
            try
            {
                response.status = true;
                response.Value = await _tipoMuebleService.Editar(modelo);
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
                response.Value = await _tipoMuebleService.Eliminar1(id);
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
