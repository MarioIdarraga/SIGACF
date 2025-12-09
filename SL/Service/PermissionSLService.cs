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
    /// <summary>
    /// Servicio de capa SL encargado de gestionar permisos, familias y patentes.
    /// Orquesta la lógica de DVH/DVV, validación, logging y persistencia 
    /// mediante el repositorio de permisos.
    /// </summary>
    public class PermissionSLService
    {
        private readonly IPermissionRepository _repo;

        /// <summary>
        /// Inicializa el servicio obteniendo el repositorio configurado 
        /// según el backend definido (File o SqlServer).
        /// </summary>
        public PermissionSLService()
        {
            _repo = SLFactory.Current.GetPermissionRepository();
        }

        /// <summary>
        /// Obtiene todas las patentes asociadas a un usuario,
        /// aplicando Flatten para eliminar jerarquías.
        /// </summary>
        public List<Patente> GetPatentesByUser(Guid userId)
        {
            var permisos = _repo.GetPermissionsForUser(userId);
            return PermissionHelper.FlattenPermissions(permisos);
        }

        /// <summary>
        /// Obtiene todas las familias con su jerarquía reconstruida
        /// a partir de la tabla de relaciones ParentId / ChildId.
        /// </summary>
        public List<Familia> GetAllFamilies()
        {
            var allComponents = _repo.GetAllPermissionComponents();
            var relaciones = _repo.GetAllPermissionRelations();

            var dict = allComponents.ToDictionary(pc => pc.IdComponent, pc => pc);

            foreach (var rel in relaciones)
            {
                if (dict.TryGetValue(rel.ParentId, out var padre) &&
                    padre is Familia familia &&
                    dict.TryGetValue(rel.ChildId, out var hijo))
                {
                    familia.Add(hijo);
                }
            }

            return allComponents.OfType<Familia>().ToList();
        }

        /// <summary>
        /// Asigna familias a un usuario, removiendo previamente las existentes.
        /// Realiza logging y maneja excepciones.
        /// </summary>
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

        /// <summary>
        /// Registra una nueva familia de permisos verificando nombre único,
        /// cantidad mínima de hijos y recalculando DVH/DVV.
        /// </summary>
        public void SaveFamily(Familia familia)
        {
            LoggerService.Log("Inicio de registro de familia.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                if (string.IsNullOrWhiteSpace(familia.Name))
                    throw new ArgumentException("El nombre de la familia es obligatorio.");

                var existentes = _repo.GetAllPermissionComponents();
                bool yaExiste = existentes.OfType<Familia>().Any(f =>
                    string.Equals(f.Name, familia.Name, StringComparison.OrdinalIgnoreCase));

                if (yaExiste)
                    throw new Exception($"Ya existe una familia con el nombre '{familia.Name}'.");

                if (familia.GetChild() == 0)
                    throw new ArgumentException("La familia debe contener al menos un permiso.");

                familia.DVH = DVHHelper.CalcularDVH(familia);

                _repo.SaveFamily(familia);

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

        /// <summary>
        /// Modifica una familia existente, recalculando DVH/DVV
        /// y validando sus valores.
        /// </summary>
        public void UpdateFamily(Familia familia)
        {
            LoggerService.Log("Inicio modificación de familia.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                if (string.IsNullOrWhiteSpace(familia.Name))
                    throw new ArgumentException("El nombre de la familia es obligatorio.");

                if (familia.GetChild() == 0)
                    throw new ArgumentException("La familia debe contener al menos un permiso.");

                familia.DVH = DVHHelper.CalcularDVH(familia);

                _repo.UpdateFamily(familia);

                var all = _repo.GetAllPermissionComponents();
                new DVVService().RecalcularDVV(all, "PermissionComponent");

                LoggerService.Log("Familia modificada correctamente.", EventLevel.Informational, Session.CurrentUser?.LoginName);
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al modificar familia: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todas las patentes existentes.
        /// </summary>
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

        /// <summary>
        /// Obtiene todos los componentes de permiso (familias + patentes).
        /// </summary>
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

        /// <summary>
        /// Registra una patente nueva, calcula DVH/DVV y valida datos.
        /// </summary>
        public void SavePatent(Patente patente)
        {
            LoggerService.Log("Inicio de registro de patente.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                if (string.IsNullOrWhiteSpace(patente.Name))
                    throw new ArgumentException("El nombre de la patente es obligatorio.");

                patente.DVH = DVHHelper.CalcularDVH(patente);

                _repo.SavePatent(patente);

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

        /// <summary>
        /// Modifica una patente existente recalculando DVH/DVV.
        /// </summary>
        public void UpdatePatent(Patente patente)
        {
            LoggerService.Log("Inicio de modificación de patente.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                if (string.IsNullOrWhiteSpace(patente.Name))
                    throw new ArgumentException("El nombre de la patente es obligatorio.");

                patente.DVH = DVHHelper.CalcularDVH(patente);

                _repo.UpdatePatent(patente);

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

        /// <summary>
        /// Busca recursivamente en familias hasta encontrar una patente cuyo FormName coincida.
        /// </summary>
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

        /// <summary>
        /// Búsqueda DFS (Depth-First Search) dentro de una jerarquía de familias.
        /// </summary>
        private Patente FindPatentDFS(Familia fam, string formName)
        {
            foreach (var child in fam.GetChildren())
            {
                if (child is Patente p &&
                    string.Equals(p.FormName, formName, StringComparison.OrdinalIgnoreCase))
                    return p;

                if (child is Familia f)
                {
                    var found = FindPatentDFS(f, formName);
                    if (found != null) return found;
                }
            }
            return null;
        }

        /// <summary>
        /// Obtiene la lista de FormName permitidos para un usuario,
        /// utilizados para habilitar o deshabilitar elementos UI.
        /// </summary>
        public List<string> GetAllowedComponentsForUser(Guid userId)
        {
            LoggerService.Log("Inicio obtención de permisos del usuario.",
                              EventLevel.Informational,
                              Session.CurrentUser?.LoginName);

            try
            {
                var patentes = GetPatentesByUser(userId);

                if (patentes == null || patentes.Count == 0)
                    return new List<string>();

                var componentes = patentes
                    .Where(p => !string.IsNullOrWhiteSpace(p.FormName))
                    .Select(p => p.FormName)
                    .Distinct()
                    .ToList();

                LoggerService.Log($"Permisos obtenidos correctamente. Total: {componentes.Count}",
                                  EventLevel.Informational,
                                  Session.CurrentUser?.LoginName);

                return componentes;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al obtener permisos del usuario: {ex.Message}",
                                  EventLevel.Error,
                                  Session.CurrentUser?.LoginName);
                throw;
            }
        }
    }
}
