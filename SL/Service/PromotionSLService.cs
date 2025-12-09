using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using BLL.Service;
using DAL.Factory;
using Domain;
using SL.Helpers;
using SL.Service;
using SL.Services;

namespace SL
{
    /// <summary>
    /// Service Layer para la gestión de promociones.
    /// Encapsula logging, acceso a la capa BLL y utilidades para la UI.
    /// </summary>
    public class PromotionSLService
    {
        private readonly PromotionService _promotionService;

        /// <summary>
        /// Crea una nueva instancia de <see cref="PromotionSLService"/>.
        /// </summary>
        /// <param name="promotionService">
        /// Servicio de negocio asociado a promociones.
        /// </param>
        public PromotionSLService(PromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        /// <summary>
        /// Obtiene todas las promociones disponibles.
        /// </summary>
        /// <returns>
        /// Lista de objetos <see cref="Promotion"/> activos en el sistema.
        /// </returns>
        public List<Promotion> GetAll()
        {
            LoggerService.Log(
                "Inicio obtención de promociones.",
                EventLevel.Informational,
                Session.CurrentUser?.LoginName);

            try
            {
                var list = _promotionService.GetAll();

                LoggerService.Log(
                    $"Fin obtención de promociones. Total: {list.Count}.",
                    EventLevel.Informational,
                    Session.CurrentUser?.LoginName);

                return list;
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error al obtener promociones: {ex.Message}",
                    EventLevel.Error,
                    Session.CurrentUser?.LoginName);

                throw;
            }
        }

        /// <summary>
        /// Devuelve un diccionario útil para la UI donde la clave es el IdPromotion
        /// y el valor es la descripción de la promoción.
        /// </summary>
        /// <returns>
        /// Diccionario id → descripción.
        /// </returns>
        public IDictionary<Guid, string> GetPromotionsLookup()
        {
            LoggerService.Log(
                "Inicio carga lookup de promociones.",
                EventLevel.Informational,
                Session.CurrentUser?.LoginName);

            try
            {
                var list = GetAll();

                var lookup = list.ToDictionary(
                    p => p.IdPromotion,
                    p => p.PromotionDescripcion);

                LoggerService.Log(
                    $"Fin carga lookup de promociones. Cantidad: {lookup.Count}.",
                    EventLevel.Informational,
                    Session.CurrentUser?.LoginName);

                return lookup;
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error al cargar lookup de promociones: {ex.Message}",
                    EventLevel.Error,
                    Session.CurrentUser?.LoginName);

                throw;
            }
        }
    }
}

