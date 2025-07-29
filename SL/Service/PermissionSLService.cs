using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using SL.Composite;
using SL.DAL.Contracts;
using SL.Factory;
using SL.Helpers;
using SL.Services;

namespace SL.Service
{
    public class PermissionSLService
    {
        private readonly IPermissionRepository _repo;

        public PermissionSLService()
        {
            _repo = SLFactory.Current.GetPermissionRepository();
        }

        public List<Patente> GetPatentesByUser(Guid userId)
        {
            var permisos = _repo.GetPermissionsForUser(userId);
            return PermissionHelper.FlattenPermissions(permisos);
        }

        public List<Familia> GetAllFamilies()
        {
            return _repo.GetAllFamilies();
        }

        public void AssignFamiliesToUser(Guid userId, List<Guid> familyIds)
        {
            LoggerService.Log("Inicio asignación de familias a usuario.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                _repo.RemoveFamiliesFromUser(userId);
                _repo.AssignFamiliesToUser(userId, familyIds);

                LoggerService.Log("Asignación de familias finalizada.", EventLevel.Informational, Session.CurrentUser?.LoginName);
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error en asignación de familias: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }

        public void SaveFamily(Familia familia)
        {
            LoggerService.Log("Inicio de registro de familia.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                if (string.IsNullOrWhiteSpace(familia.Name))
                    throw new ArgumentException("El nombre de la familia es obligatorio.");

                if (familia.GetChild() == 0)
                    throw new ArgumentException("La familia debe contener al menos un permiso.");

                // Calcular DVH
                familia.DVH = DVHHelper.CalcularDVH(familia);

                // Guardar
                _repo.SaveFamily(familia);

                // Recalcular DVV
                var all = _repo.GetAllPermissionComponents();
                new DVVService().RecalcularDVV(all, "PermissionComponent");

                LoggerService.Log("Familia registrada correctamente.", EventLevel.Informational, Session.CurrentUser?.LoginName);
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al registrar familia: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }

        public List<Patente> GetAllPatents()
        {
            LoggerService.Log("Inicio búsqueda de patentes.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                var permisos = _repo.GetAllPermissionComponents();
                var patentes = permisos.OfType<Patente>().ToList();

                LoggerService.Log("Patentes encontradas correctamente.", EventLevel.Informational, Session.CurrentUser?.LoginName);

                return patentes;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al buscar patentes: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }
        public List<PermissionComponent> GetAllPermissionComponents()
        {
            LoggerService.Log("Obteniendo todos los componentes de permiso.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                var permisos = _repo.GetAllPermissionComponents();

                LoggerService.Log("Componentes de permiso obtenidos correctamente.", EventLevel.Informational, Session.CurrentUser?.LoginName);

                return permisos;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al obtener componentes de permiso: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }

        //Patentes:

        public void SavePatent(Patente patente)
        {
            LoggerService.Log("Inicio de registro de patente.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                if (string.IsNullOrWhiteSpace(patente.Name))
                    throw new ArgumentException("El nombre de la patente es obligatorio.");

                // Calcular DVH
                patente.DVH = DVHHelper.CalcularDVH(patente);

                // Guardar
                _repo.SavePatent(patente);

                // Recalcular DVV
                var all = _repo.GetAllPermissionComponents();
                new DVVService().RecalcularDVV(all, "PermissionComponent");

                LoggerService.Log("Patente registrada correctamente.", EventLevel.Informational, Session.CurrentUser?.LoginName);
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al registrar patente: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }

        public void UpdatePatent(Patente patente)
        {
            LoggerService.Log("Inicio de modificación de patente.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                if (string.IsNullOrWhiteSpace(patente.Name))
                    throw new ArgumentException("El nombre de la patente es obligatorio.");

                // Calcular DVH con los datos modificados
                patente.DVH = DVHHelper.CalcularDVH(patente);

                // Guardar en repositorio
                _repo.UpdatePatent(patente);

                // Recalcular DVV
                var all = _repo.GetAllPermissionComponents();
                new DVVService().RecalcularDVV(all, "PermissionComponent");

                LoggerService.Log("Patente modificada correctamente.", EventLevel.Informational, Session.CurrentUser?.LoginName);
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al modificar patente: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }

    }
}




