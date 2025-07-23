using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using BLL.Service;
using Domain;
using SL.Helpers;
using SL.Services;

namespace SL.Service.Extension
{
    public class UserSLService
    {
        private readonly UserService _userService;

        public UserSLService(UserService userService)
        {
            _userService = userService;
        }

        public bool TryLogin(string loginName, string password, out User user, out string message)
        {
            user = null;
            message = string.Empty;

            if (string.IsNullOrWhiteSpace(loginName) || string.IsNullOrWhiteSpace(password))
            {
                message = "El usuario y la contraseña son requeridos.";
                return false;
            }

            user = _userService.GetByLoginName(loginName);
            if (user == null)
            {
                message = "Usuario o contraseña incorrectos";
                return false;
            }

            string encryptedPassword = AesEncryptionHelper.Encrypt(password);

            if (user.Password == password)
            {
                // Migración automática
                user.Password = encryptedPassword;
                Update(user); // se recalcula DVH/DVV automáticamente
            }
            else if (user.Password != encryptedPassword)
            {
                message = "Contraseña incorrecta.";
                user = null;
                return false;
            }

            if (user.State != 1)
            {
                message = "El usuario no está activo.";
                user = null;
                return false;
            }

            return true;
        }

        public void Insert(User user)
        {
            LoggerService.Log("Inicio de registro de Usuario.");

            try
            {
                user.Password = AesEncryptionHelper.Encrypt(user.Password);
                string textoDVH = $"{user.LoginName}|{user.Password}|{user.NroDocument}";
                user.DVH = HashHelper.CalculateSHA256(textoDVH);

                _userService.RegisterUser(user);

                var usuarios = _userService.GetAll(null, null, null, null, null);
                RecalcularDVV(usuarios, "Usuario");

                LoggerService.Log("Usuario registrado correctamente.");
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al registrar usuario: {ex.Message}", EventLevel.Error);
                throw;
            }
        }

        public void Update(User user)
        {
            LoggerService.Log("Inicio de actualización de Usuario.");

            try
            {
                user.Password = AesEncryptionHelper.Encrypt(user.Password);
                string textoDVH = $"{user.LoginName}|{user.Password}|{user.NroDocument}";
                user.DVH = HashHelper.CalculateSHA256(textoDVH);

                _userService.UpdateUser(user);

                var usuarios = _userService.GetAll(null, null, null, null, null);
                RecalcularDVV(usuarios, "Usuario");

                LoggerService.Log("Usuario actualizado correctamente.");
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al actualizar usuario: {ex.Message}", EventLevel.Error);
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

        public void RecalcularDVV<T>(IEnumerable<T> lista, string tabla) where T : IDVH
        {
            string concatenado = string.Join("", lista.Select(e => e.DVH));
            string nuevoDVV = HashHelper.CalculateSHA256(concatenado);

            var dvvService = new DVVService();
            dvvService.UpdateDVV(tabla, nuevoDVV);
        }
    }
}
