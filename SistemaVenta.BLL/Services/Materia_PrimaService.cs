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
    public class Materia_PrimaService : IMateria_PrimaService
    {

        private readonly IGenericRepository<Materia_Prima> _materiaPrimaRepository;
        private readonly IMapper _mapper;

        public Materia_PrimaService(IGenericRepository<Materia_Prima> materiaPrimaRepository, IMapper mapper)
        {
            _materiaPrimaRepository = materiaPrimaRepository;
            _mapper = mapper;
        }
        public async Task<List<MateriaPrimaDTO>> Lista()
        {
            // Await the result of Consultar() before applying Include and ToListAsync
            var lista = await _materiaPrimaRepository.Consultar()
                .ConfigureAwait(false); // Ensure the IQueryable is resolved

            var listaConInclude = lista.Include(mp => mp.Producto); // Apply Include on IQueryable

            var listaFinal = await listaConInclude.ToListAsync(); // Convert to List asynchronously

            return _mapper.Map<List<MateriaPrimaDTO>>(listaFinal);
        }


        public async Task<MateriaPrimaDTO> Crear(MateriaPrimaDTO modelo)
        {
            try
            {
                // Fix: Use a lambda expression to create the filter for the Consultar method
                var existe = await _materiaPrimaRepository
                    .Consultar(mp => mp.IdProducto == modelo.IdProducto);

                if (existe.Any()) // Check if any record matches the filter
                    throw new Exception("Ya existe una materia prima con ese idproducto");

                var materiaPrimaModelo = _mapper.Map<Materia_Prima>(modelo);
                var materiaPrimaCreada = await _materiaPrimaRepository.Crear(materiaPrimaModelo);

                if (materiaPrimaCreada == null || materiaPrimaCreada.IdProducto == 0)
                    throw new Exception("No se pudo crear la materia prima");

                return _mapper.Map<MateriaPrimaDTO>(materiaPrimaCreada);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al crear la materia prima", ex);
            }
        }



        public async Task<bool> Editar(MateriaPrimaDTO modelo)
        {
            try
            {
                var materiaPrimaModelo = _mapper.Map<Materia_Prima>(modelo);

                var materiaPrimaExiste = await _materiaPrimaRepository.Obtener(mp => mp.IdProducto == materiaPrimaModelo.IdProducto);

                if (materiaPrimaExiste == null)
                    throw new KeyNotFoundException("No se encontró la materia prima para editar");

                // Actualizá solo lo que se puede cambiar
                materiaPrimaExiste.Descripcion = materiaPrimaModelo.Descripcion;
                materiaPrimaExiste.UnidadMedida = materiaPrimaModelo.UnidadMedida;

                bool resultado = await _materiaPrimaRepository.Editar(materiaPrimaExiste);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al editar la materia prima", ex);
            }
        }
        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var materiaPrimaExiste = await _materiaPrimaRepository.Obtener(mp => mp.IdProducto == id);

                if (materiaPrimaExiste == null)
                    throw new KeyNotFoundException("No se encontró la materia prima para eliminar");

                bool resultado = await _materiaPrimaRepository.Eliminar(materiaPrimaExiste);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al eliminar la materia prima", ex);
            }
        }



    }
}
