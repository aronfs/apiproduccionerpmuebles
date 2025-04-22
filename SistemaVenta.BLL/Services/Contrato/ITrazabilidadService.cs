using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Services.Contrato
{
    public interface ITrazabilidadService
    {
        Task<List<TrazabilidadDTO>> Lista();
        Task<TrazabilidadDTO> Crear(TrazabilidadDTO modelo);
        Task<bool> Editar(TrazabilidadDTO modelo);
        Task<bool> Eliminar(int id);
    }
}
