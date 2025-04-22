using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Services.Contrato
{
    public interface ITallerService
    {
        Task<List<TallerDTO>> Lista();
        Task<TallerDTO> Crear(TallerDTO modelo);
        Task<bool> Editar(TallerDTO modelo);
        Task<bool> Eliminar(int id);

    }
}
