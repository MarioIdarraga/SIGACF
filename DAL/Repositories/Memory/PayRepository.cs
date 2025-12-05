using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.Memory
{
    /// <summary>
    /// Repositorio en memoria para gestionar pagos (Pay).
    /// </summary>
    internal class PayRepository : IPayRepository
    {
        /// <summary>
        /// Lista estática que almacena los pagos en memoria.
        /// </summary>
        private static readonly List<Pay> _pays = new List<Pay>();

        #region CRUD

        /// <summary>
        /// Inserta un nuevo pago en memoria.
        /// </summary>
        public void Insert(Pay obj)
        {
            try
            {
                // Simula autoincremento como en FILE
                if (obj.IdPay == 0)
                    obj.IdPay = _pays.Any() ? _pays.Max(x => x.IdPay) + 1 : 1;

                _pays.Add(obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el pago en memoria.", ex);
            }
        }

        /// <summary>
        /// Actualiza un pago existente.
        /// </summary>
        public void Update(int id, Pay obj)
        {
            try
            {
                var existing = _pays.FirstOrDefault(x => x.IdPay == id);

                if (existing == null)
                    throw new Exception("El pago no existe.");

                _pays.Remove(existing);

                obj.IdPay = id; // aseguramos que mantiene el mismo ID
                _pays.Add(obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el pago en memoria.", ex);
            }
        }

        /// <summary>
        /// Elimina un pago por su identificador.
        /// </summary>
        public void Delete(int id)
        {
            try
            {
                var existing = _pays.FirstOrDefault(x => x.IdPay == id);

                if (existing == null)
                    throw new Exception("El pago no existe.");

                _pays.Remove(existing);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el pago en memoria.", ex);
            }
        }

        #endregion

        #region SELECT

        /// <summary>
        /// Obtiene un pago por su identificador.
        /// </summary>
        public Pay GetOne(int id)
        {
            try
            {
                return _pays.FirstOrDefault(x => x.IdPay == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el pago en memoria.", ex);
            }
        }

        /// <summary>
        /// Obtiene todos los pagos sin filtros.
        /// </summary>
        public IEnumerable<Pay> GetAll()
        {
            try
            {
                return _pays.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de pagos en memoria.", ex);
            }
        }

        /// <summary>
        /// Obtiene pagos filtrados por fecha, igual que el repositorio FILE.
        /// </summary>
        public IEnumerable<Pay> GetAll(DateTime? registrationSincePay, DateTime? registrationUntilPay)
        {
            try
            {
                var list = _pays.AsEnumerable();

                if (registrationSincePay.HasValue)
                    list = list.Where(x => x.Date >= registrationSincePay.Value);

                if (registrationUntilPay.HasValue)
                    list = list.Where(x => x.Date <= registrationUntilPay.Value);

                return list.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener pagos filtrados por fecha en memoria.", ex);
            }
        }

        #endregion
    }
}
