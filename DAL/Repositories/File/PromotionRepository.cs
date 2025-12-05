using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.File
{
    /// <summary>
    /// Repositorio FILE para la entidad Promotion.
    /// Gestiona almacenamiento en archivo de texto delimitado por '|'.
    /// </summary>
    internal class PromotionRepository : IGenericRepository<Promotion>
    {
        private readonly string _filePath;

        /// <summary>
        /// Inicializa el repositorio y asegura la existencia del archivo.
        /// </summary>
        public PromotionRepository()
        {
            try
            {
                string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                _filePath = Path.Combine(folder, "Promotions.txt");

                if (!System.IO.File.Exists(_filePath))
                    System.IO.File.WriteAllText(_filePath, "");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar el repositorio de promociones.", ex);
            }
        }

        #region Helpers

        /// <summary>
        /// Carga todas las promociones desde el archivo.
        /// </summary>
        private List<Promotion> LoadAll()
        {
            try
            {
                var list = new List<Promotion>();

                foreach (var line in System.IO.File.ReadAllLines(_filePath))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var p = line.Split('|');
                    if (p.Length != 6) continue;

                    list.Add(new Promotion
                    {
                        IdPromotion = Guid.Parse(p[0]),
                        Name = p[1],
                        ValidFrom = DateTime.Parse(p[2]),
                        ValidTo = DateTime.Parse(p[3]),
                        PromotionDescripcion = p[4],
                        DiscountPercentage = string.IsNullOrEmpty(p[5]) ? (int?)null : int.Parse(p[5])
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar las promociones.", ex);
            }
        }

        /// <summary>
        /// Guarda todas las promociones en el archivo.
        /// </summary>
        private void SaveAll(List<Promotion> list)
        {
            try
            {
                var lines = list.Select(x =>
                    $"{x.IdPromotion}|{x.Name}|{x.ValidFrom:o}|{x.ValidTo:o}|{x.PromotionDescripcion}|{(x.DiscountPercentage.HasValue ? x.DiscountPercentage.Value.ToString() : "")}");

                System.IO.File.WriteAllLines(_filePath, lines);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar las promociones.", ex);
            }
        }

        #endregion


        #region CRUD

        /// <summary>
        /// Inserta una nueva promoción.
        /// </summary>
        public void Insert(Promotion Object)
        {
            try
            {
                var list = LoadAll();

                if (Object.IdPromotion == Guid.Empty)
                    Object.IdPromotion = Guid.NewGuid();

                list.Add(Object);
                SaveAll(list);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la promoción.", ex);
            }
        }

        /// <summary>
        /// Actualiza una promoción existente.
        /// </summary>
        public void Update(Guid Id, Promotion Object)
        {
            try
            {
                var list = LoadAll();

                var existing = list.FirstOrDefault(x => x.IdPromotion == Id);
                if (existing == null)
                    throw new Exception("La promoción no existe.");

                list.Remove(existing);

                Object.IdPromotion = Id; // mantener ID
                list.Add(Object);

                SaveAll(list);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la promoción.", ex);
            }
        }

        /// <summary>
        /// Elimina una promoción por ID.
        /// </summary>
        public void Delete(Guid Id)
        {
            try
            {
                var list = LoadAll();

                var item = list.FirstOrDefault(x => x.IdPromotion == Id);
                if (item == null)
                    throw new Exception("La promoción no existe.");

                list.Remove(item);
                SaveAll(list);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la promoción.", ex);
            }
        }

        /// <summary>
        /// Obtiene una promoción por ID.
        /// </summary>
        public Promotion GetOne(Guid Id)
        {
            try
            {
                return LoadAll().FirstOrDefault(x => x.IdPromotion == Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la promoción.", ex);
            }
        }

        #endregion


        #region GetAll

        /// <summary>
        /// Obtiene todas las promociones sin filtros.
        /// </summary>
        public IEnumerable<Promotion> GetAll()
        {
            try
            {
                return LoadAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las promociones.", ex);
            }
        }

        /// <summary>
        /// Filtros no aplican en FILE → retorna todas.
        /// </summary>
        public IEnumerable<Promotion> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            return GetAll();
        }

        /// <summary>
        /// Filtros no aplican en FILE → retorna todas.
        /// </summary>
        public IEnumerable<Promotion> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            return GetAll();
        }

        /// <summary>
        /// Filtros no aplican en FILE → retorna todas.
        /// </summary>
        public IEnumerable<Promotion> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state)
        {
            return GetAll();
        }

        /// <summary>
        /// Filtros no aplican en FILE → retorna todas.
        /// </summary>
        public IEnumerable<Promotion> GetAll(DateTime? from, DateTime? to, int state)
        {
            return GetAll();
        }

        #endregion
    }
}
