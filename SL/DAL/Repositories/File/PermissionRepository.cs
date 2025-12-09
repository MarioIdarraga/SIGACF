using SL.Composite;
using SL.DAL.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;


namespace SL.DAL.Repositories.File
{
    /// <summary>
    /// Repositorio FILE para almacenar permisos, familias, patentes, 
    /// relaciones padre-hijo y asignaciones de usuario, basado en archivos JSON.
    /// Simula el comportamiento del repositorio SQL manteniendo consistencia estructural.
    /// </summary>
    internal class PermissionRepository : IPermissionRepository
    {
        private readonly string basePath;
        private readonly string pathComponents;
        private readonly string pathRelations;
        private readonly string pathUserFamilies;

        private readonly JavaScriptSerializer json = new JavaScriptSerializer();

        /// <summary>
        /// Constructor del repositorio FILE de permisos.
        /// Inicializa rutas, crea carpetas y genera archivos JSON vacíos si no existen.
        /// </summary>
        public PermissionRepository()
        {
            basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileRepo");

            pathComponents = Path.Combine(basePath, "PermissionComponents.json");
            pathRelations = Path.Combine(basePath, "PermissionRelations.json");
            pathUserFamilies = Path.Combine(basePath, "UserFamilies.json");

            Directory.CreateDirectory(basePath);

            if (!System.IO.File.Exists(pathComponents)) System.IO.File.WriteAllText(pathComponents, "[]");
            if (!System.IO.File.Exists(pathRelations)) System.IO.File.WriteAllText(pathRelations, "[]");
            if (!System.IO.File.Exists(pathUserFamilies)) System.IO.File.WriteAllText(pathUserFamilies, "[]");
        }


        /// <summary>
        /// Carga todos los componentes (Patentes y Familias) desde el archivo JSON.
        /// </summary>
        private List<PermissionComponent> LoadComponents()
        {
            var raw = System.IO.File.ReadAllText(pathComponents);
            var list = json.Deserialize<List<PermissionComponentDTO>>(raw) ?? new List<PermissionComponentDTO>();

            return list
                .Select<PermissionComponentDTO, PermissionComponent>(dto =>
                {
                    if (dto.ComponentType == "Patente")
                    {
                        return (PermissionComponent)new Patente
                        {
                            IdComponent = dto.IdComponent,
                            Name = dto.Name,
                            FormName = dto.FormName,
                            ComponentType = dto.ComponentType,
                            DVH = dto.DVH
                        };
                    }
                    else
                    {
                        return (PermissionComponent)new Familia(dto.Name, null)
                        {
                            IdComponent = dto.IdComponent,
                            ComponentType = dto.ComponentType,
                            DVH = dto.DVH
                        };
                    }
                })
                .ToList();
        }

        /// <summary>
        /// Guarda la lista de componentes (Patentes y Familias) en archivo JSON.
        /// </summary>
        private void SaveComponents(List<PermissionComponent> components)
        {
            var list = components.Select(c => new PermissionComponentDTO
            {
                IdComponent = c.IdComponent,
                Name = c.Name,
                FormName = c.FormName,
                ComponentType = c.ComponentType,
                DVH = c.DVH
            }).ToList();

            System.IO.File.WriteAllText(pathComponents, json.Serialize(list));
        }

        /// <summary>
        /// Carga todas las relaciones padre-hijo del JSON.
        /// </summary>
        private List<(Guid ParentId, Guid ChildId)> LoadRelations()
        {
            var raw = System.IO.File.ReadAllText(pathRelations);
            var list = json.Deserialize<List<RelationDTO>>(raw) ?? new List<RelationDTO>();
            return list.Select(r => (r.ParentId, r.ChildId)).ToList();
        }

        /// <summary>
        /// Guarda las relaciones padre-hijo en archivo JSON.
        /// </summary>
        private void SaveRelations(List<(Guid ParentId, Guid ChildId)> items)
        {
            var list = items.Select(r => new RelationDTO
            {
                ParentId = r.ParentId,
                ChildId = r.ChildId
            }).ToList();

            System.IO.File.WriteAllText(pathRelations, json.Serialize(list));
        }

        /// <summary>
        /// Carga asignaciones de familias por usuario.
        /// </summary>
        private List<UserFamilyDTO> LoadUserFamilies()
        {
            var raw = System.IO.File.ReadAllText(pathUserFamilies);
            return json.Deserialize<List<UserFamilyDTO>>(raw) ?? new List<UserFamilyDTO>();
        }

        /// <summary>
        /// Guarda las asignaciones de familias por usuario.
        /// </summary>
        private void SaveUserFamilies(List<UserFamilyDTO> items)
        {
            System.IO.File.WriteAllText(pathUserFamilies, json.Serialize(items));
        }



        /// <summary>
        /// Inserta una nueva familia y registra sus relaciones con componentes hijos.
        /// </summary>
        public void SaveFamily(Familia family)
        {
            var comps = LoadComponents();
            var rels = LoadRelations();

            if (family.IdComponent == Guid.Empty)
                family.IdComponent = Guid.NewGuid();

            family.ComponentType = "Familia";

            comps.Add(family);

            foreach (var child in family.GetChildren())
                rels.Add((family.IdComponent, child.IdComponent));

            SaveComponents(comps);
            SaveRelations(rels);
        }

        /// <summary>
        /// Actualiza una familia y reemplaza sus relaciones para reflejar cambios.
        /// </summary>
        public void UpdateFamily(Familia familia)
        {
            var comps = LoadComponents();
            var rels = LoadRelations();

            var existing = comps.First(x => x.IdComponent == familia.IdComponent);

            existing.Name = familia.Name;
            existing.DVH = familia.DVH;

            rels.RemoveAll(r => r.ParentId == familia.IdComponent);

            foreach (var child in familia.GetChildren())
                rels.Add((familia.IdComponent, child.IdComponent));

            SaveComponents(comps);
            SaveRelations(rels);
        }

        /// <summary>
        /// Guarda una patente en el repositorio FILE.
        /// </summary>
        public void SavePatent(Patente patente)
        {
            var comps = LoadComponents();

            if (patente.IdComponent == Guid.Empty)
                patente.IdComponent = Guid.NewGuid();

            patente.ComponentType = "Patente";

            comps.Add(patente);

            SaveComponents(comps);
        }

        /// <summary>
        /// Actualiza una patente existente.
        /// </summary>
        public void UpdatePatent(Patente patente)
        {
            var comps = LoadComponents();

            var existing = comps.First(x => x.IdComponent == patente.IdComponent);

            existing.Name = patente.Name;
            existing.FormName = patente.FormName;
            existing.DVH = patente.DVH;

            SaveComponents(comps);
        }

        /// <summary>
        /// Obtiene todos los componentes (Familias y Patentes).
        /// </summary>
        public List<PermissionComponent> GetAllPermissionComponents()
        {
            return LoadComponents();
        }

        /// <summary>
        /// Obtiene todas las relaciones entre componentes padre-hijo.
        /// </summary>
        public List<(Guid ParentId, Guid ChildId)> GetAllPermissionRelations()
        {
            return LoadRelations();
        }

        /// <summary>
        /// Devuelve todas las familias existentes.
        /// </summary>
        public List<Familia> GetAllFamilies()
        {
            return LoadComponents()
                .Where(c => c.ComponentType == "Familia")
                .Select(c => (Familia)c)
                .ToList();
        }

        /// <summary>
        /// Devuelve todos los permisos asignados a un usuario,
        /// incluyendo permisos heredados por composición recursiva.
        /// </summary>
        public List<PermissionComponent> GetPermissionsForUser(Guid userId)
        {
            var userFamilies = LoadUserFamilies()
                .Where(u => u.UserId == userId)
                .Select(u => u.FamilyId)
                .ToList();

            if (!userFamilies.Any())
                return new List<PermissionComponent>();

            var comps = LoadComponents();
            var rels = LoadRelations();

            var result = new List<PermissionComponent>();

            foreach (var familyId in userFamilies)
                RecursivelyCollect(familyId, comps, rels, result);

            return result.Where(c => c.ComponentType == "Patente").ToList();
        }

        /// <summary>
        /// Recorre recursivamente la jerarquía de permisos para obtener todos los hijos.
        /// </summary>
        private void RecursivelyCollect(Guid parentId, List<PermissionComponent> comps,
                                        List<(Guid ParentId, Guid ChildId)> rels,
                                        List<PermissionComponent> result)
        {
            var children = rels.Where(r => r.ParentId == parentId).Select(r => r.ChildId).ToList();

            foreach (var childId in children)
            {
                var comp = comps.First(c => c.IdComponent == childId);
                result.Add(comp);

                if (comp is Familia)
                    RecursivelyCollect(childId, comps, rels, result);
            }
        }

        /// <summary>
        /// Asigna familias a un usuario, reemplazando asignaciones previas.
        /// </summary>
        public void AssignFamiliesToUser(Guid userId, List<Guid> familyIds)
        {
            var list = LoadUserFamilies();

            list.RemoveAll(x => x.UserId == userId);

            foreach (var id in familyIds)
                list.Add(new UserFamilyDTO { UserId = userId, FamilyId = id });

            SaveUserFamilies(list);
        }

        /// <summary>
        /// Elimina todas las asignaciones de permisos de un usuario.
        /// </summary>
        public void RemoveFamiliesFromUser(Guid userId)
        {
            var list = LoadUserFamilies();
            list.RemoveAll(x => x.UserId == userId);
            SaveUserFamilies(list);
        }



        /// <summary>
        /// DTO para almacenar componentes en archivo JSON.
        /// </summary>
        private class PermissionComponentDTO
        {
            public Guid IdComponent { get; set; }
            public string Name { get; set; }
            public string FormName { get; set; }
            public string ComponentType { get; set; }
            public string DVH { get; set; }
        }

        /// <summary>
        /// DTO para almacenar relaciones padre-hijo.
        /// </summary>
        private class RelationDTO
        {
            public Guid ParentId { get; set; }
            public Guid ChildId { get; set; }
        }

        /// <summary>
        /// DTO para almacenar asignaciones de familias a usuarios.
        /// </summary>
        private class UserFamilyDTO
        {
            public Guid UserId { get; set; }
            public Guid FamilyId { get; set; }
        }
    }
}