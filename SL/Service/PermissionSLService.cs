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
            // 1. Traemos todos los componentes (patentes + familias)
            var allComponents = _repo.GetAllPermissionComponents();

            // 2. Traemos TODAS las relaciones padre‑hijo
            var relaciones = _repo.GetAllPermissionRelations();

            // 3. Diccionario [Guid → PermissionComponent] p/ acceso rápido
            var dict = allComponents.ToDictionary(pc => pc.IdComponent, pc => pc);

            // 4. Re‑construimos la jerarquía
            foreach (var rel in relaciones)          // rel.ParentId / rel.ChildId
            {
                if (dict.TryGetValue(rel.ParentId, out var padre) && padre is Familia familia &&
                    dict.TryGetValue(rel.ChildId, out var hijo))
                {
                    familia.Add(hijo);
                }
            }

            // 5. Solo devolvemos las familias
            return allComponents.OfType<Familia>().ToList();
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

                var existentes = _repo.GetAllPermissionComponents();
                bool yaExiste = existentes
                    .OfType<Familia>()
                    .Any(f => string.Equals(f.Name, familia.Name, StringComparison.OrdinalIgnoreCase));

                if (yaExiste)
                    throw new Exception($"Ya existe una familia con el nombre '{familia.Name}'.");

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

        public void UpdateFamily(Familia familia)
        {
            LoggerService.Log("Inicio modificación de familia.", EventLevel.Informational,
                              Session.CurrentUser?.LoginName);

            try
            {
                if (string.IsNullOrWhiteSpace(familia.Name))
                    throw new ArgumentException("El nombre de la familia es obligatorio.");

                if (familia.GetChild() == 0)
                    throw new ArgumentException("La familia debe contener al menos un permiso.");

                // Recalcular DVH
                familia.DVH = DVHHelper.CalcularDVH(familia);

                // Persistir cambios
                _repo.UpdateFamily(familia);

                // Recalcular DVV
                var all = _repo.GetAllPermissionComponents();
                new DVVService().RecalcularDVV(all, "PermissionComponent");

                LoggerService.Log("Familia modificada correctamente.", EventLevel.Informational,
                                  Session.CurrentUser?.LoginName);
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al modificar familia: {ex.Message}", EventLevel.Error,
                                  Session.CurrentUser?.LoginName);
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

        public Patente FindPatentByFormName(string formName)
        {
            if (string.IsNullOrWhiteSpace(formName)) return null;

            foreach (var fam in GetAllFamilies())
            {
                var found = FindPatentDFS(fam, formName);
                if (found != null) return found;
            }
            return null;
        }

        private Patente FindPatentDFS(Familia fam, string formName)
        {
            foreach (var child in fam.GetChildren())
            {
                var asPat = child as Patente;
                if (asPat != null && string.Equals(asPat.FormName, formName, StringComparison.OrdinalIgnoreCase))
                    return asPat;

                var asFam = child as Familia;
                if (asFam != null)
                {
                    var r = FindPatentDFS(asFam, formName);
                    if (r != null) return r;
                }
            }
            return null;
        }

    }
}




