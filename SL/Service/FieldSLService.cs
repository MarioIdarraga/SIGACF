using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using BLL.Service;
using Domain;
using SL.Helpers;
using SL.Services;

namespace SL
{
    public class FieldSLService
    {
        private readonly FieldService _fieldService;

        public FieldSLService(FieldService fieldService)
        {
            _fieldService = fieldService;
        }

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

