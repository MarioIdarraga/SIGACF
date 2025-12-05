using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.Memory
{
    /// <summary>
    /// Repositorio en memoria para la entidad Promotion.
    /// Utilizado para modo demo, pruebas o fallback sin SQL ni FILE.
    /// </summary>
    internal class PromotionRepository : IGenericRepository<Promotion>
    {
        /// <summary>
        /// Lista estática con promociones precargadas.
        /// </summary>
        private static readonly List<Promotion> _promotions = new List<Promotion>
        {
            new Promotion
            {
                IdPromotion = Guid.Parse("7c6e3fd0-3fa2-49c7-b1c0-d7de00000001"),
                Name = "Promo Verano",
                ValidFrom = DateTime.Parse("2024-12-01"),
                ValidTo = DateTime.Parse("2025-03-01"),
                PromotionDescripcion = "Descuento por temporada",
                DiscountPercentage = 15
            },
            new Promotion
            {
                IdPromotion = Guid.Parse("7c6e3fd0-3fa2-49c7-b1c0-d7de00000002"),
                Name = "Promo 2x1",
                ValidFrom = DateTime.Parse("2025-01-01"),
                ValidTo = DateTime.Parse("2025-01-31"),
                PromotionDescripcion = "Solo días lunes",
                DiscountPercentage = 50
            }
        };

        #region CRUD

        /// <summary>
        /// Inserta una nueva promoción en memoria.
        /// </summary>
        public void Insert(Promotion Object)
        {
            try
            {
                if (Object.IdPromotion == Guid.Empty)
                    Object.IdPromotion = Guid.NewGuid();

                _promotions.Add(Object);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la promoción en memoria.", ex);
            }
        }

        /// <summary>
        /// Actualiza una promoción existente.
        /// </summary>
        public void Update(Guid Id, Promotion Object)
        {
            try
            {
                var existing = _promotions.FirstOrDefault(x => x.IdPromotion == Id);

                if (existing == null)
                    throw new Exception("La promoción no existe.");

                _promotions.Remove(existing);

                Object.IdPromotion = Id;
                _promotions.Add(Object);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la promoción en memoria.", ex);
            }
        }

        /// <summary>
        /// Elimina una promoción de memoria.
        /// </summary>
        public void Delete(Guid Id)
        {
            try
            {
                var item = _promotions.FirstOrDefault(x => x.IdPromotion == Id);

                if (item == null)
                    throw new Exception("La promoción no existe.");

                _promotions.Remove(item);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la promoción en memoria.", ex);
            }
        }

        #endregion

        #region SELECT

        /// <summary>
        /// Obtiene una promoción por su identificador.
        /// </summary>
        public Promotion GetOne(Guid Id)
        {
            try
            {
                return _promotions.FirstOrDefault(x => x.IdPromotion == Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la promoción en memoria.", ex);
            }
        }

        /// <summary>
        /// Obtiene todas las promociones.
        /// </summary>
        public IEnumerable<Promotion> GetAll()
        {
            try
            {
                return _promotions.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las promociones en memoria.", ex);
            }
        }

        // Las siguientes sobrecargas no aplican para promociones.
        // Se devuelven todas, igual que en FILE.

        public IEnumerable<Promotion> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
            => GetAll();

        public IEnumerable<Promotion> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
            => GetAll();

        public IEnumerable<Promotion> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state)
            => GetAll();

        public IEnumerable<Promotion> GetAll(DateTime? from, DateTime? to, int state)
            => GetAll();

        #endregion
    }
}
