using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Services.Contrato
{
    public interface IComisionSRIService
    {
        Task<List<ComisionSRIDTO>> Lista();
        Task<ComisionSRIDTO> Crear(ComisionSRIDTO modelo);
        Task<bool> Editar(ComisionSRIDTO modelo);
        Task<bool> Eliminar(int id);

        string GenerarFactura(ComisionSRIDTO modelo); // Fixed CS1519 and IDE1006
    }
}
