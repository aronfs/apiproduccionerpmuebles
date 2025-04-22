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
    public class Materia_PrimaController : ControllerBase
    {
        private readonly IMateria_PrimaService _materiaPrimaService;

        public Materia_PrimaController(IMateria_PrimaService materiaPrimaService)
        {
            _materiaPrimaService = materiaPrimaService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<MateriaPrimaDTO>>();
            try
            {
                response.status = true;
                response.Value = await _materiaPrimaService.Lista();

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
        public async Task<IActionResult> Guardar([FromBody] MateriaPrimaDTO modelo)
        {
            var response = new Response<MateriaPrimaDTO>();
            try
            {
             
                response.status = true;
                response.Value = await _materiaPrimaService.Crear(modelo);
                
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
        public async Task<IActionResult> Editar([FromBody] MateriaPrimaDTO modelo)
        {
            var response = new Response<bool>();
            try
            {
                response.status = true;
                response.Value = await _materiaPrimaService.Editar(modelo);
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
                response.Value = await _materiaPrimaService.Eliminar(id);
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
