using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Services.Contrato
{
    public interface IBodegaService
    {

            Task<List<BodegaDTO>> ListarBodega();
            Task<List<BodegaDTO>> FiltrosBodega(string buscarPor, string nombreBodega, string fechaInicio, string fechaFin);
            Task<BodegaDTO> CrearBodega(BodegaDTO dto);
            Task<bool> ActualizarBodega(BodegaDTO dto);
            Task<bool> EliminarBodega(int id);
        
    }
}
