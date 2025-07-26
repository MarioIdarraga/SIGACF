using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Contracts;
using Domain;

namespace BLL.Service
{
    public class FieldService
    {
        private readonly IGenericRepository<Field> _fieldRepo;

        public FieldService(IGenericRepository<Field> fieldRepo)
        {
            _fieldRepo = fieldRepo;
        }

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

        public List<Field> GetAll(string name = null, int? capacity = null, int? fieldType = null, int? fieldState = null)
        {
            return _fieldRepo.GetAll()
                             .Where(f => (string.IsNullOrEmpty(name) || f.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0) &&
                                         (!capacity.HasValue || f.Capacity == capacity.Value) &&
                                         (!fieldType.HasValue || f.FieldType == fieldType.Value) &&
                                         (!fieldState.HasValue || f.IdFieldState == fieldState.Value))
                             .ToList();
        }

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

