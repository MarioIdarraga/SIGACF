using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using BLL.Service;
using Domain;
using SL.Helpers;
using SL.Services;

namespace SL.Service
{
    /// <summary>
    /// Service Layer encargado de gestionar operaciones de negocio relacionadas
    /// con las canchas (Fields), agregando cálculo de DVH, actualización de DVV
    /// y registro de logs.
    /// </summary>
    public class FieldSLService
    {
        private readonly FieldService _fieldService;

        /// <summary>
        /// Inicializa el servicio de lógica de negocio para canchas.
        /// </summary>
        /// <param name="fieldService">Servicio BLL responsable de operaciones CRUD sobre Field.</param>
        public FieldSLService(FieldService fieldService)
        {
            _fieldService = fieldService;
        }

        /// <summary>
        /// Inserta una nueva cancha en el sistema, calcula el DVH correspondiente
        /// y actualiza el DVV de la tabla Fields. Registra todos los eventos en el Logger.
        /// </summary>
        /// <param name="field">Entidad Field a registrar.</param>
        public void Insert(Field field)
        {
            LoggerService.Log("Inicio de registro de cancha.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                field.DVH = DVHHelper.CalcularDVH(field);                                       // Calculo del DVH

                _fieldService.Insert(field);                                                    // Inserta

                var repo = global::DAL.Factory.Factory.Current.GetFieldRepository();            // Recalcular DVV
                var fields = repo.GetAll();
                new DVVService().RecalcularDVV(fields, "Fields");

                LoggerService.Log("Cancha registrada correctamente.", EventLevel.Informational, Session.CurrentUser?.LoginName);
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al registrar cancha: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }

        /// <summary>
        /// Actualiza los datos de una cancha existente, recalculando el DVH
        /// y actualizando el DVV de la tabla Fields para mantener integridad referencial.
        /// </summary>
        /// <param name="field">Entidad Field con los datos actualizados.</param>
        public void Update(Field field)
        {
            LoggerService.Log("Inicio de actualización de cancha.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                field.DVH = DVHHelper.CalcularDVH(field);                                       // Recalcular el DVH del campo modificado

                _fieldService.Update(field);                                                    // Actualiza

                var repo = global::DAL.Factory.Factory.Current.GetFieldRepository();            // Recalcular DVV para la tabla
                var fields = repo.GetAll();
                new DVVService().RecalcularDVV(fields, "Fields");

                LoggerService.Log("Cancha actualizada correctamente.", EventLevel.Informational, Session.CurrentUser?.LoginName);
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al actualizar cancha: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }

        /// <summary>
        /// Obtiene la lista de canchas filtradas por nombre, capacidad, tipo y estado.
        /// Registra los eventos de inicio y fin de consulta.
        /// </summary>
        /// <param name="name">Nombre de la cancha a filtrar (opcional).</param>
        /// <param name="capacity">Capacidad de la cancha (opcional).</param>
        /// <param name="fieldType">Tipo de cancha (opcional).</param>
        /// <param name="fieldState">Estado actual de la cancha (opcional).</param>
        /// <returns>Listado de canchas que cumplen los criterios de búsqueda.</returns>
        public List<Field> GetAll(string name = null, int? capacity = null, int? fieldType = null, int? fieldState = null)
        {
            LoggerService.Log("Inicio búsqueda de canchas.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                var result = _fieldService.GetAll(name, capacity, fieldType, fieldState);
                LoggerService.Log($"Fin búsqueda de canchas. Resultados: {result.Count}", EventLevel.Informational, Session.CurrentUser?.LoginName);
                return result;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al buscar canchas: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }
    }
}

