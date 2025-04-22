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
    public class ComisionSRIController : ControllerBase
    {
        private readonly IComisionSRIService _comisionService;

        public ComisionSRIController(IComisionSRIService comisionService)
        {
            _comisionService = comisionService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<ComisionSRIDTO>>();
            try
            {
                response.status = true;
                response.Value = await _comisionService.Lista();
               
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
        public async Task<IActionResult> Guardar([FromBody] ComisionSRIDTO modelo)
        {
            var response = new Response<ComisionSRIDTO>();
            try
            {
                var dtoResult = await _comisionService.Crear(modelo);

                // 🧾 Generar factura tipo ticket
                var facturaTexto = _comisionService.GenerarFactura(dtoResult);

                response.status = true;
                response.Value = dtoResult;
                response.msg = $"Comisión registrada correctamente\n\n{facturaTexto}";
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
        public async Task<IActionResult> Editar([FromBody] ComisionSRIDTO modelo)
        {
            var response = new Response<bool>();
            try
            {
                response.status = true;
                response.Value = await _comisionService.Editar(modelo);
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
                response.Value = await _comisionService.Eliminar(id);
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
