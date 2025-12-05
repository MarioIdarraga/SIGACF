using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.File
{
    /// <summary>
    /// Repositorio FILE para gestionar pagos (Pay).
    /// Utiliza archivo de texto delimitado por '|'.
    /// </summary>
    internal class PayRepository : IPayRepository
    {
        private readonly string _filePath;

        /// <summary>
        /// Inicializa el repositorio asegurando la existencia del archivo.
        /// </summary>
        public PayRepository()
        {
            try
            {
                string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                _filePath = Path.Combine(folder, "Pays.txt");

                if (!System.IO.File.Exists(_filePath))
                    System.IO.File.WriteAllText(_filePath, "");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar el repositorio de pagos.", ex);
            }
        }

        #region Helpers

        /// <summary>
        /// Carga toda la información desde el archivo.
        /// </summary>
        private List<Pay> LoadAll()
        {
            try
            {
                var list = new List<Pay>();

                foreach (var line in System.IO.File.ReadAllLines(_filePath))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var parts = line.Split('|');
                    if (parts.Length != 7) continue;

                    list.Add(new Pay
                    {
                        IdPay = int.Parse(parts[0]),
                        IdBooking = Guid.Parse(parts[1]),
                        NroDocument = int.Parse(parts[2]),
                        Date = DateTime.Parse(parts[3]),
                        MethodPay = int.Parse(parts[4]),
                        Amount = decimal.Parse(parts[5]),
                        State = int.Parse(parts[6])
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los pagos desde archivo.", ex);
            }
        }

        /// <summary>
        /// Guarda toda la información en el archivo.
        /// </summary>
        private void SaveAll(List<Pay> list)
        {
            try
            {
                var lines = list.Select(x =>
                    $"{x.IdPay}|{x.IdBooking}|{x.NroDocument}|{x.Date:o}|{x.MethodPay}|{x.Amount}|{x.State}");

                System.IO.File.WriteAllLines(_filePath, lines);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar los pagos.", ex);
            }
        }

        #endregion


        #region CRUD

        /// <summary>
        /// Inserta un nuevo pago.
        /// </summary>
        public void Insert(Pay obj)
        {
            try
            {
                var list = LoadAll();

                // Autoincremento básico si IdPay viene en 0
                if (obj.IdPay == 0 && list.Any())
                    obj.IdPay = list.Max(x => x.IdPay) + 1;

                list.Add(obj);
                SaveAll(list);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el pago.", ex);
            }
        }

        /// <summary>
        /// Actualiza un pago existente.
        /// </summary>
        public void Update(int id, Pay obj)
        {
            try
            {
                var list = LoadAll();

                var existing = list.FirstOrDefault(x => x.IdPay == id);

                if (existing == null)
                    throw new Exception("El pago no existe.");

                list.Remove(existing);

                obj.IdPay = id; // aseguramos que se mantiene el mismo ID
                list.Add(obj);

                SaveAll(list);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el pago.", ex);
            }
        }

        /// <summary>
        /// Elimina un pago por ID.
        /// </summary>
        public void Delete(int id)
        {
            try
            {
                var list = LoadAll();

                var item = list.FirstOrDefault(x => x.IdPay == id);

                if (item == null)
                    throw new Exception("El pago no existe.");

                list.Remove(item);
                SaveAll(list);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el pago.", ex);
            }
        }

        /// <summary>
        /// Obtiene un pago por ID.
        /// </summary>
        public Pay GetOne(int id)
        {
            try
            {
                return LoadAll().FirstOrDefault(x => x.IdPay == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el pago.", ex);
            }
        }

        #endregion


        #region GetAll

        /// <summary>
        /// Obtiene todos los pagos sin filtros.
        /// </summary>
        public IEnumerable<Pay> GetAll()
        {
            try
            {
                return LoadAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de pagos.", ex);
            }
        }

        /// <summary>
        /// Obtiene pagos filtrados por fecha (desde/hasta).
        /// </summary>
        public IEnumerable<Pay> GetAll(DateTime? registrationSincePay, DateTime? registrationUntilPay)
        {
            try
            {
                var list = LoadAll();

                if (registrationSincePay.HasValue)
                    list = list.Where(x => x.Date >= registrationSincePay.Value).ToList();

                if (registrationUntilPay.HasValue)
                    list = list.Where(x => x.Date <= registrationUntilPay.Value).ToList();

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener pagos filtrados por fecha.", ex);
            }
        }

        #endregion
    }
}
