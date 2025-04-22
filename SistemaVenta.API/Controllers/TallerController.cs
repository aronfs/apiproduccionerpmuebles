using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.Utilidad;
using SistemaVenta.BLL.Services.Contrato;
using SistemaVenta.DTO;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TallerController : ControllerBase
    {
        private readonly ITallerService _tallerService;

        public TallerController(ITallerService tallerService)
        {
            _tallerService = tallerService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<TallerDTO>>();
            try
            {
                response.status = true;
                response.Value = await _tallerService.Lista();
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
        public async Task<IActionResult> Guardar([FromBody] TallerDTO modelo)
        {
            var response = new Response<TallerDTO>();
            try
            {
                response.status = true;
                response.Value = await _tallerService.Crear(modelo);
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
        public async Task<IActionResult> Editar([FromBody] TallerDTO modelo)
        {
            var response = new Response<bool>();
            try
            {
                response.status = true;
                response.Value = await _tallerService.Editar(modelo);
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
                response.Value = await _tallerService.Eliminar(id);
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
