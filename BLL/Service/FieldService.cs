using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Contracts;
using Domain;

namespace BLL.Service
{
    /// <summary>
    /// Servicio de negocio encargado de gestionar la lógica relacionada
    /// con las canchas (Field). Valida datos y delega operaciones al repositorio.
    /// </summary>
    public class FieldService
    {
        private readonly IGenericRepository<Field> _fieldRepo;

        /// <summary>
        /// Inicializa el servicio con el repositorio de canchas.
        /// </summary>
        /// <param name="fieldRepo">Repositorio genérico para la entidad Field.</param>
        public FieldService(IGenericRepository<Field> fieldRepo)
        {
            _fieldRepo = fieldRepo;
        }

        /// <summary>
        /// Inserta una nueva cancha luego de validar sus datos.
        /// </summary>
        /// <param name="field">Entidad Field a insertar.</param>
        /// <exception cref="ArgumentNullException">Si la cancha es nula.</exception>
        /// <exception cref="ArgumentException">Si algún campo obligatorio es inválido.</exception>
        /// <exception cref="Exception">Si ocurre un error inesperado en la BLL.</exception>
        public void Insert(Field field)
        {
            try
            {
                ValidateField(field);
                _fieldRepo.Insert(field);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la cancha en la capa BLL", ex);
            }
        }

        /// <summary>
        /// Actualiza una cancha existente luego de validar sus datos.
        /// </summary>
        /// <param name="field">Entidad Field con los datos actualizados.</param>
        /// <exception cref="ArgumentNullException">Si la cancha es nula.</exception>
        /// <exception cref="ArgumentException">Si algún campo obligatorio es inválido.</exception>
        /// <exception cref="Exception">Si ocurre un error inesperado en la BLL.</exception>
        public void Update(Field field)
        {
            try
            {
                ValidateField(field);
                _fieldRepo.Update(field.IdField, field);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la cancha en la capa BLL", ex);
            }
        }

        /// <summary>
        /// Obtiene todas las canchas filtrando por nombre, capacidad,
        /// tipo de cancha y estado, en caso de especificarse filtros.
        /// </summary>
        /// <param name="name">Nombre parcial o completo de la cancha.</param>
        /// <param name="capacity">Capacidad exacta de la cancha.</param>
        /// <param name="fieldType">Tipo de cancha.</param>
        /// <param name="fieldState">Estado de la cancha.</param>
        /// <returns>Listado filtrado de canchas.</returns>
        public List<Field> GetAll(string name = null, int? capacity = null, int? fieldType = null, int? fieldState = null)
        {
            return _fieldRepo.GetAll()
                             .Where(f =>
                                 (string.IsNullOrEmpty(name) || f.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0) &&
                                 (!capacity.HasValue || f.Capacity == capacity.Value) &&
                                 (!fieldType.HasValue || f.FieldType == fieldType.Value) &&
                                 (!fieldState.HasValue || f.IdFieldState == fieldState.Value))
                             .ToList();
        }

        /// <summary>
        /// Valida que los datos de la cancha sean correctos antes de insertarla o actualizarla.
        /// </summary>
        /// <param name="field">Entidad Field a validar.</param>
        /// <exception cref="ArgumentNullException">Si la cancha es nula.</exception>
        /// <exception cref="ArgumentException">Si algún campo obligatorio no cumple las reglas.</exception>
        private void ValidateField(Field field)
        {
            if (field == null)
                throw new ArgumentNullException(nameof(field), "La cancha no puede ser nula.");

            if (string.IsNullOrWhiteSpace(field.Name))
                throw new ArgumentException("El nombre de la cancha es obligatorio.");

            if (field.Capacity <= 0)
                throw new ArgumentException("La capacidad debe ser un número mayor a cero.");

            if (field.HourlyCost <= 0)
                throw new ArgumentException("El costo por hora debe ser mayor a cero.");

            if (field.FieldType <= 0)
                throw new ArgumentException("El tipo de cancha es inválido.");

            if (field.IdFieldState < 0)
                throw new ArgumentException("El estado de la cancha es inválido.");
        }
    }
}

