using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using BLL.Service;
using Domain;
using SL;
using SL.Composite;
using SL.Helpers;
using SL.Service;
using SL.Services;

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

        if (user.Password != encryptedPassword)
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


    public void Insert(User user, Guid familyId)
    {
        LoggerService.Log("Inicio de registro de Usuario con familia asignada.");

        try
        {
            // 1. Encriptar contraseña
            user.Password = AesEncryptionHelper.Encrypt(user.Password);

            // 2. Calcular DVH
            user.DVH = DVHHelper.CalcularDVH(user);

            // 3. Insertar usuario
            _userService.RegisterUser(user);

            // 4. Recalcular DVV
            var repo = DAL.Factory.Factory.Current.GetUserRepository();
            var usuarios = repo.GetAll();
            new DVVService().RecalcularDVV(usuarios, "Users");

            // 5. Relacionar usuario con familia de permisos
            var permissionService = new PermissionSLService();
            permissionService.AssignFamiliesToUser(user.UserId, new List<Guid> { familyId });

            LoggerService.Log("Usuario registrado correctamente con familia asignada.");
        }
        catch (Exception ex)
        {
            LoggerService.Log($"Error al registrar usuario con familia: {ex.Message}", EventLevel.Error);
            throw;
        }
    }


    public void Update(User user, Guid familyId)
    {
        LoggerService.Log("Inicio de modificación de Usuario.");

        try
        {
            // Encriptar contraseña si fue modificada
            user.Password = AesEncryptionHelper.Encrypt(user.Password);

            // Recalcular DVH
            user.DVH = DVHHelper.CalcularDVH(user);

            // Actualizar usuario
            _userService.UpdateUser(user);

            // Recalcular DVV
            var repo = DAL.Factory.Factory.Current.GetUserRepository();
            var usuarios = repo.GetAll();
            new DVVService().RecalcularDVV(usuarios, "Users");

            // 🔁 Actualizar relación con la familia (permiso)
            var permissionService = new PermissionSLService();
            permissionService.AssignFamiliesToUser(user.UserId, new List<Guid> { familyId });

            LoggerService.Log("Usuario modificado correctamente.");
        }
        catch (Exception ex)
        {
            LoggerService.Log($"Error al modificar usuario: {ex.Message}", EventLevel.Error);
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

    public bool AnyUsersExist()
    {
        return _userService
               .GetAll(null, null, null, null, null)
               .Any();
    }
}
