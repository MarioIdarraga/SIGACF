using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using BLL.Service;
using Domain;
using SL.Helpers;
using DAL.Factory;
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
                // Calculo del DVH
                field.DVH = DVHHelper.CalcularDVH(field);

                // Insertar usando BLL
                _fieldService.Insert(field);

                // Recalcular DVV desde SL
                var repo = DAL.Factory.Factory.Current.GetFieldRepository();
                var fields = repo.GetAll();
                new DVVService().RecalcularDVV(fields, "Canchas");

                LoggerService.Log("Cancha registrada correctamente.", EventLevel.Informational, Session.CurrentUser?.LoginName);
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al registrar cancha: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }


        public void Update(Guid idField, Field field)
        {
            LoggerService.Log("Inicio de modificación de cancha.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                _fieldService.Update(field);
                LoggerService.Log("Cancha modificada correctamente.", EventLevel.Informational, Session.CurrentUser?.LoginName);
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al modificar cancha: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }

        //public List<Field> GetAll(string name = null, int? capacity = null, int? fieldType = null)
        //{
        //    LoggerService.Log("Inicio búsqueda de canchas.", EventLevel.Informational, Session.CurrentUser?.LoginName);

        //    try
        //    {
        //        var result = _fieldService.GetAll(name, capacity, fieldType);
        //        LoggerService.Log($"Fin búsqueda de canchas. Resultados: {result.Count}", EventLevel.Informational, Session.CurrentUser?.LoginName);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        LoggerService.Log($"Error al buscar canchas: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
        //        throw;
        //    }
        //}
    }
}

