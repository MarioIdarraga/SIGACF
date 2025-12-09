using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Domain;
using SL.Composite;
using SL.Domain;
using SL.Factory;
using SL.Helpers;

namespace SL.Services
{
    /// <summary>
    /// Servicio encargado de gestionar el cálculo, actualización y verificación
    /// del Dígito Verificador Vertical (DVV) en las tablas del sistema.
    /// Utiliza repositorios genéricos y el repositorio de DVV para asegurar integridad.
    /// </summary>
    public class DVVService
    {
        private IGenericRepository<User> _userRepo;
        private IGenericRepository<Booking> _reservaRepo;
        private IGenericRepository<Field> _canchaRepo;
        private IGenericRepository<Log> _logRepo;
        private IGenericRepository<PermissionComponent> _permisoRepo;

        /// <summary>
        /// Recalcula el DVV de una tabla tomando la lista de entidades,
        /// concatenando sus DVH y generando un SHA-256.
        /// </summary>
        /// <typeparam name="T">Tipo de entidad que implementa <see cref="IDVH"/>.</typeparam>
        /// <param name="lista">Lista de entidades con DVH calculado.</param>
        /// <param name="tabla">Nombre de la tabla sobre la cual se actualiza el DVV.</param>
        public void RecalcularDVV<T>(IEnumerable<T> lista, string tabla) where T : IDVH
        {
            string concatenado = string.Join("", lista.Select(e => e.DVH));
            string nuevoDVV = HashHelper.CalculateSHA256(concatenado);

            var dvvRepo = SLFactory.Current.GetVerificadorVerticalRepository();
            dvvRepo.SetDVV(tabla, nuevoDVV);
        }

        /// <summary>
        /// Actualiza el DVV de una tabla directamente con el valor especificado.
        /// </summary>
        /// <param name="tabla">Nombre de la tabla.</param>
        /// <param name="nuevoDVV">Nuevo valor del DVV a almacenar.</param>
        public void UpdateDVV(string tabla, string nuevoDVV)
        {
            var dvvRepo = SLFactory.Current.GetVerificadorVerticalRepository();
            dvvRepo.SetDVV(tabla, nuevoDVV);
        }

        /// <summary>
        /// Verifica la integridad completa del sistema comparando los DVH calculados
        /// con el DVV almacenado para cada tabla crítica.
        /// </summary>
        /// <returns>
        /// true si todas las tablas mantienen integridad;
        /// false si alguna presenta alteraciones.
        /// </returns>
        public bool VerificarIntegridadCompleta()
        {
            bool todoOk = true;

            todoOk &= VerificarTabla<User>(_userRepo, "Users");
            todoOk &= VerificarTabla<Booking>(_reservaRepo, "Bookings");
            todoOk &= VerificarTabla<Field>(_canchaRepo, "Fields");
            todoOk &= VerificarTabla<PermissionComponent>(_permisoRepo, "Permissions");

            return todoOk;
        }

        /// <summary>
        /// Verifica la integridad de una tabla específica comparando
        /// el DVV almacenado con el DVV recalculado desde los DVH.
        /// </summary>
        /// <typeparam name="T">Tipo de entidad que implementa <see cref="IDVH"/>.</typeparam>
        /// <param name="repo">Repositorio genérico que permite obtener todas las entidades.</param>
        /// <param name="tabla">Nombre de la tabla a validar.</param>
        /// <returns>
        /// true si la tabla está íntegra;
        /// false si el DVV no coincide con el esperado.
        /// </returns>
        private bool VerificarTabla<T>(IGenericRepository<T> repo, string tabla) where T : IDVH
        {
            var lista = repo.GetAll();

            string concatenado = string.Join("", lista.Select(e => e.DVH));
            string dvvCalculado = HashHelper.CalculateSHA256(concatenado);

            var dvvRepo = SLFactory.Current.GetVerificadorVerticalRepository();
            string dvvAlmacenado = dvvRepo.GetDVV(tabla);

            return dvvCalculado == dvvAlmacenado;
        }
    }
}
