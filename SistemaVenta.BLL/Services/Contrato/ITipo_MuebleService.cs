using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Services.Contrato
{
    public interface ITipo_MuebleService
    {
        Task<List<Tipo_MuebleDTO>> Lista();
        Task<Tipo_MuebleDTO> Crear(Tipo_MuebleDTO modelo);
        Task<bool> Editar(Tipo_MuebleDTO modelo);
        Task<bool> Eliminar1(int id);
    }
}
