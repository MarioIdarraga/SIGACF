using DAL.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DAL.Repositories.File
{
    /// <summary>
    /// Repositorio FILE para métodos de pago (PaymentMethod).
    /// Usa almacenamiento en archivo de texto delimitado por '|'.
    /// </summary>
    internal class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly string _filePath;

        /// <summary>
        /// Inicializa el repositorio y asegura la existencia del archivo.
        /// </summary>
        public PaymentMethodRepository()
        {
            try
            {
                string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                _filePath = Path.Combine(folder, "PaymentMethods.txt");

                if (!System.IO.File.Exists(_filePath))
                    System.IO.File.WriteAllText(_filePath, "");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar el repositorio de métodos de pago.", ex);
            }
        }

        #region Helpers

        /// <summary>
        /// Carga todos los métodos de pago desde el archivo.
        /// </summary>
        private List<PaymentMethod> LoadAll()
        {
            try
            {
                var list = new List<PaymentMethod>();

                foreach (var line in System.IO.File.ReadAllLines(_filePath))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var parts = line.Split('|');
                    if (parts.Length != 2) continue;

                    list.Add(new PaymentMethod
                    {
                        IdPayMethod = int.Parse(parts[0]),
                        Description = parts[1]
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los métodos de pago.", ex);
            }
        }

        /// <summary>
        /// Guarda todos los métodos de pago en el archivo.
        /// </summary>
        private void SaveAll(List<PaymentMethod> list)
        {
            try
            {
                var lines = list.Select(x => $"{x.IdPayMethod}|{x.Description}");
                System.IO.File.WriteAllLines(_filePath, lines);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar los métodos de pago.", ex);
            }
        }

        #endregion


        #region Métodos públicos

        /// <summary>
        /// Obtiene todos los métodos de pago.
        /// </summary>
        public IEnumerable<PaymentMethod> GetAll()
        {
            try
            {
                return LoadAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los métodos de pago.", ex);
            }
        }

        /// <summary>
        /// Obtiene un método de pago por su identificador.
        /// </summary>
        public PaymentMethod GetOne(int id)
        {
            try
            {
                var list = LoadAll();
                return list.FirstOrDefault(x => x.IdPayMethod == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el método de pago.", ex);
            }
        }

        #endregion
    }
}

