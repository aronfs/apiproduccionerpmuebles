using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;
using SistemaVenta.Model.Models;

namespace SistemaVenta.BLL.Services.Contrato
{
    public interface IInventarioService
    {
        Task<List<InventarioDTO>> ListarInventario();
        Task<List<InventarioDTO>> FiltrosInventario(string buscarPor, string nombreBodega, string fechaInicio, string fechaFin);
        Task<InventarioDTO> CrearInventario(InventarioDTO dto);
        Task<bool> ActualizarInventario(InventarioDTO dto);
        Task<bool> EliminarInventario(int id);
    }

}
