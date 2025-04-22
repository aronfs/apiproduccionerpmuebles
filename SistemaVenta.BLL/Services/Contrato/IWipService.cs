using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface IWipService
    {
        Task<List<WipDTO>> Listar();
        Task<List<WipDTO>> Filtros(string buscarPor, string nombreBodega, string fechaInicio, string fechaFin);
        // Este método reemplaza al Registrar, ya que WIP se crea desde el trigger
        Task<bool> AsignarOperadorOWip(WipDTO dto);
        Task<bool> Actualizar(WipDTO dto); // Puedes usarlo para actualizar estado, fechas, etc.
        Task<bool> Eliminar(int id); // 
    }
}


