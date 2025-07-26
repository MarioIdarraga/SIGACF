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
            ValidateField(field);
            _fieldRepo.Insert(field);
        }

        public void Update(Field field)
        {
            if (field == null)
                throw new ArgumentNullException(nameof(field), "La cancha no puede ser nula.");

            if (field.IdField == Guid.Empty)
                throw new ArgumentException("El ID de la cancha es obligatorio para modificar.");

            ValidateField(field);

            var existingField = _fieldRepo.GetOne(field.IdField);
            if (existingField == null)
                throw new InvalidOperationException("No se encontró la cancha a modificar.");

            _fieldRepo.Update(field.IdField, field);
        }

        //public List<Field> GetAll(string name = null, int? capacity = null, int? fieldType = null)
        //{
        //    return _fieldRepo.GetAll(name, capacity, fieldType).ToList();
        //}

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

