using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Service;
using Domain;
using SL.Helpers;

namespace SL.Service.Extension
{
    public class UserSLService
    {
        private readonly UserService _userService;

        public UserSLService(UserService userService)
        {
            _userService = userService;
        }

        public void Insert(User user)
        {
            LoggerService.Log("Inicio de registro de Usuario.");

            try
            {
                _userService.RegisterUser(user);
                LoggerService.Log("Usuario registrado correctamente.");
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al registrar cliente: {ex.Message}", EventLevel.Error);
                throw;
            }
        }

        public void Update(Guid idUser, User user)
        {
            LoggerService.Log("Inicio de modificación de usuario.");

            try
            {
                _userService.UpdateUser(user);
                LoggerService.Log("Usuario modificado correctamente.");
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al modificar el usuario: {ex.Message}", EventLevel.Error);
                throw;
            }
        }

        public List<User> GetAll(int? nroDocumento, string firstName, string lastName, string telephone, string mail)
        {
            LoggerService.Log("Inicio búsqueda de usuarios.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                var result = _userService.GetAll(nroDocumento, firstName, lastName, telephone, mail);
                LoggerService.Log($"Fin búsqueda de usuarios. Resultados: {result.Count}", EventLevel.Informational, Session.CurrentUser?.LoginName);
                return result;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al buscar usuarios: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }

    }
}