using BLL.Service;
using Domain;
using SL;
using SL.Composite;
using SL.Helpers;
using SL.Service;
using SL.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;

/// <summary>
/// Servicio de lógica de aplicación para operaciones de usuario.
/// Coordina la comunicación entre la UI y la BLL,
/// manejando encriptación, DVH/DVV, permisos y logging.
/// </summary>
public class UserSLService
{
    private readonly UserService _userService;

    public UserSLService(UserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Realiza el intento de login, validando credenciales y estado del usuario.
    /// </summary>
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

        if (user.State != 2)
        {
            message = "El usuario no está activo.";
            user = null;
            return false;
        }

        return true;
    }

    /// <summary>
    /// Inserta un usuario aplicando encriptación, DVH/DVV y asignación de familia.
    /// Las excepciones de negocio se relanzan directamente.
    /// </summary>
    public void Insert(User user, Guid familyId)
    {
        LoggerService.Log("Inicio de registro de Usuario con familia asignada.");

        try
        {
            user.Password = AesEncryptionHelper.Encrypt(user.Password);
            user.DVH = DVHHelper.CalcularDVH(user);

            _userService.RegisterUser(user);

            var repo = DAL.Factory.Factory.Current.GetUserRepository();
            var usuarios = repo.GetAll();
            new DVVService().RecalcularDVV(usuarios, "Users");

            var permissionService = new PermissionSLService();
            permissionService.AssignFamiliesToUser(user.UserId, new List<Guid> { familyId });

            LoggerService.Log("Usuario registrado correctamente con familia asignada.");
        }
        catch (BLL.BusinessException.BusinessException bx)
        {
            LoggerService.Log(bx.Message, EventLevel.Warning);
            throw;
        }
        catch (ArgumentException ax)
        {
            LoggerService.Log(ax.Message, EventLevel.Warning);
            throw;
        }
        catch (Exception ex)
        {
            LoggerService.Log($"Error al registrar usuario: {ex.Message}", EventLevel.Error);
            throw new Exception("Se produjo un error inesperado al registrar el usuario.", ex);
        }
    }

    /// <summary>
    /// Actualiza un usuario, aplicando encriptación, DVH/DVV y reasignación de familia.
    /// Las excepciones de negocio y validación se relanzan intactas.
    /// </summary>
    public void Update(User user, Guid? familyId = null)
    {
        LoggerService.Log("Inicio de modificación de Usuario.");

        // 1. Obtener el usuario existente para preservar la contraseña encriptada.
        // Usamos GetByLogin para encontrarlo, pero si la UI cambió el LoginName, esto podría fallar.
        // Lo más seguro es usar el ID del usuario para obtener el objeto existente.
        User existingUser = _userService.GetOne(user.UserId);

        if (existingUser == null)
        {
            throw new InvalidOperationException("No se encontró el usuario a modificar.");
        }

        try
        {
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                user.Password = existingUser.Password;
            }
            else
            {
                user.Password = AesEncryptionHelper.Encrypt(user.Password);
            }


            user.DVH = DVHHelper.CalcularDVH(user);

            _userService.UpdateUser(user);

            var repo = DAL.Factory.Factory.Current.GetUserRepository();
            var usuarios = repo.GetAll();
            new DVVService().RecalcularDVV(usuarios, "Users");

            if (familyId.HasValue)
            {
                var permissionService = new PermissionSLService();
                permissionService.AssignFamiliesToUser(user.UserId, new List<Guid> { familyId.Value });
            }

            LoggerService.Log("Usuario modificado correctamente.");
        }
        catch (BLL.BusinessException.BusinessException bx)
        {
            LoggerService.Log(bx.Message, EventLevel.Warning);
            throw;
        }
        catch (ArgumentException ax)
        {
            LoggerService.Log(ax.Message, EventLevel.Warning);
            throw;
        }
        catch (Exception ex)
        {
            LoggerService.Log($"Error inesperado al modificar usuario: {ex.Message}", EventLevel.Error);
            throw new Exception("Se produjo un error inesperado al modificar el usuario.", ex);
        }
    }

    /// <summary>
    /// Devuelve usuarios filtrados según los parámetros proporcionados.
    /// </summary>
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
            throw new Exception("Se produjo un error inesperado al buscar usuarios.", ex);
        }
    }

    /// <summary>
    /// Indica si existen usuarios registrados en el sistema.
    /// </summary>
    public bool AnyUsersExist()
    {
        return _userService
            .GetAll(null, null, null, null, null)
            .Any();
    }
}

