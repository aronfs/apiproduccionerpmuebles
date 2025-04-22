using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Services.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model.Models;

namespace SistemaVenta.BLL.Services
{
    public class ComisionSRIService : IComisionSRIService
    {
        private readonly IGenericRepository<Comision_SRI> _comisionRepository;
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IGenericRepository<Costo> _costoRepository;
        private readonly IMapper _mapper;

        public ComisionSRIService(
            IGenericRepository<Comision_SRI> comisionRepository,
            IGenericRepository<Producto> productoRepository,
            IGenericRepository<Costo> costoRepository,
            IMapper mapper)
        {
            _comisionRepository = comisionRepository;
            _productoRepository = productoRepository;
            _costoRepository = costoRepository;
            _mapper = mapper;
        }


        public async Task<List<ComisionSRIDTO>> Lista()
        {
            var query = await _comisionRepository.Consultar(i => i.IdComision > 0);

            var queryConIncludes = query
                .Include(i => i.Producto)
                .ThenInclude(p => p.Costos); // Incluimos también los costos del producto

            var listaComisiones = await queryConIncludes.ToListAsync();

            return _mapper.Map<List<ComisionSRIDTO>>(listaComisiones);
        }


        public async Task<ComisionSRIDTO> Crear(ComisionSRIDTO modelo)
        {
            // ⚙️ CÁLCULO DE COMISIÓN SRI (ECUADOR)

            // 1️⃣ Verificar si el producto existe en la base de datos
            var producto = await _productoRepository.Obtener(p => p.IdProducto == modelo.IdProducto);
            if (producto == null)
                throw new Exception("❌ Producto no encontrado");

            // 2️⃣ Obtener el último costo registrado del producto
            var costo = (await _costoRepository.Consultar(c => c.IdProducto == modelo.IdProducto))
                .OrderByDescending(c => c.FechaRegistro)
                .FirstOrDefault();

            if (costo == null)
                throw new Exception("❌ No se encontró un costo asignado a este producto");

            // 3️⃣ Calcular la comisión SRI
            var porcentaje = modelo.Porcentaje / 100m;
            var montoComision = costo.Monto * porcentaje;

            // 4️⃣ Generar descripción automática si no se proporciona
            var descripcion = string.IsNullOrWhiteSpace(modelo.Descripcion)
                ? "Comisión correspondiente a retención aplicada sobre el costo del producto conforme a normativa del SRI en Ecuador."
                : modelo.Descripcion;

            // 5️⃣ Crear la entidad Comision_SRI
            var entidad = new Comision_SRI
            {
                IdProducto = modelo.IdProducto,
                Porcentaje = modelo.Porcentaje,
                MontoCalculado = montoComision,
                FechaRegistro = DateTime.Now,
                Descripcion = descripcion
            };

            // 6️⃣ Guardar en la base de datos
            var comisionCreada = await _comisionRepository.Crear(entidad);

            // 7️⃣ Mapear a DTO
            var dtoResult = _mapper.Map<ComisionSRIDTO>(comisionCreada);
            dtoResult.NombreProducto = producto.Nombre;
            dtoResult.CostoProducto = costo.Monto;

            // 8️⃣ Mostrar resumen tipo factura en consola (opcional)
            var resumen = GenerarFactura(dtoResult);
            Console.WriteLine(resumen);

            return dtoResult;
        }

        public string GenerarFactura(ComisionSRIDTO dto)
        {
            const string codigoComision = "SRI-EC-COM-001";

            return $"🧾 Detalle Comisión SRI - Código: {codigoComision} | Producto: {dto.NombreProducto} (ID: {dto.IdProducto}), " +
                   $"Costo: ${dto.CostoProducto:0.00}, Porcentaje: {dto.Porcentaje}%, Comisión: ${dto.MontoCalculado:0.00}. " +
                   $"Descripción: {dto.Descripcion}. Fecha: {dto.FechaRegistro:yyyy-MM-dd}. TOTAL A REGISTRAR: ${dto.MontoCalculado:0.00}";
        }


        public async Task<bool> Editar(ComisionSRIDTO modelo)
        {
            var comision = await _comisionRepository.Obtener(c => c.IdComision == modelo.IdComision);

            if (comision == null)
                throw new Exception("❌ Comisión no encontrada");

            // Verifica si el producto existe
            var producto = await _productoRepository.Obtener(p => p.IdProducto == modelo.IdProducto);
            if (producto == null)
                throw new Exception("❌ Producto no válido para esta comisión");

            // Busca el último costo del producto
            var costo = (await _costoRepository.Consultar(c => c.IdProducto == modelo.IdProducto))
                .OrderByDescending(c => c.FechaRegistro)
                .FirstOrDefault();

            if (costo == null)
                throw new Exception("❌ No se encontró un costo para el producto");

            // Actualiza los valores
            comision.IdProducto = modelo.IdProducto;
            comision.Porcentaje = modelo.Porcentaje;
            comision.MontoCalculado = costo.Monto * (modelo.Porcentaje / 100m);
            comision.Descripcion = string.IsNullOrWhiteSpace(modelo.Descripcion)
                ? "Comisión actualizada conforme a normativa del SRI en Ecuador."
                : modelo.Descripcion;
            comision.FechaRegistro = DateTime.Now;

            var resultado = await _comisionRepository.Editar(comision);

            return resultado;
        }


        public async Task<bool> Eliminar(int id)
        {
            var comision = await _comisionRepository.Obtener(c => c.IdComision == id);

            if (comision == null)
                throw new Exception("❌ Comisión no encontrada");

            var resultado = await _comisionRepository.Eliminar(comision);
            return resultado;
        }



    }
}
