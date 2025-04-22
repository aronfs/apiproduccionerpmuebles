using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Services.Contrato
{
    public interface IOrdenProduccionService
    {
        // Crear una orden de producción con sus detalles
        Task<OrdenProduccionDTO> Crear(OrdenProduccionDTO modelo);

        // Obtener lista de órdenes CON detalles
        Task<List<OrdenProduccionDTO>> Lista();

        // Obtener una orden específica con todos los detalles
        Task<OrdenProduccionDTO> ObtenerPorId(int idProduccion);

        // Editar orden y sus detalles (si aplica)
        Task<bool> Editar(OrdenProduccionDTO modelo);

        // Eliminar una orden y sus detalles
        Task<bool> Eliminar(int idProduccion);
    }

}
