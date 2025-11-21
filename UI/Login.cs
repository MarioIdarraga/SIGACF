using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Service;
using DAL.Contracts;
using DAL.Factory;
using Domain;
using SL;
using SL.BLL;
using SL.Composite;
using SL.Helpers;
using SL.Service;
using UI.Helpers;
using BLL.BusinessException; // para BusinessException / DatabaseAccessException

namespace UI
{
    /// <summary>
    /// Formulario de inicio de sesión del sistema SIGACF.
    /// Se encarga de autenticar al usuario, inicializar la configuración
    /// y crear un usuario administrador por defecto si no existen usuarios.
    /// </summary>
    public partial class Login : Form
    {
        private object _panelContenedor;
        private object panelContenedor;

        /// <summary>
        /// Constructor del formulario de Login.
        /// Inicializa los componentes visuales y, si no existen usuarios,
        /// crea un usuario administrador por defecto con su familia y patentes.
        /// Maneja errores de negocio y errores inesperados mediante LoggerService.
        /// </summary>
        public Login()
        {
            InitializeComponent();

            try
            {
                var repo = Factory.Current.GetUserRepository();
                var userService = new UserService(repo);
                var userSLService = new UserSLService(userService);

                if (!userSLService.AnyUsersExist())
                {
                    string defaultUsername = ConfigurationManager.AppSettings["DefaultAdminUser"];
                    string defaultPassword = ConfigurationManager.AppSettings["DefaultAdminPassword"];

                    var defaultUser = new User
                    {
                        UserId = Guid.NewGuid(),
                        LoginName = defaultUsername,
                        Password = defaultPassword,
                        NroDocument = 12345678,
                        FirstName = "Admin",
                        LastName = "Principal",
                        Position = "Administrador",
                        Mail = "admin@miapp.com",
                        Address = "Dirección por defecto",
                        Telephone = "11112222",
                        State = 1
                    };

                    defaultUser.DVH = DVHHelper.CalcularDVH(defaultUser);

                    var permissionSLService = new PermissionSLService();

                    // Buscar o crear familia "Administrador"
                    var familias = permissionSLService.GetAllFamilies();
                    var adminFamily = familias.FirstOrDefault(f => f.Name == "Administrador");

                    if (adminFamily == null)
                    {
                        adminFamily = new Familia
                        {
                            IdComponent = Guid.NewGuid(),
                            Name = "Administrador"
                        };

                        permissionSLService.SaveFamily(adminFamily);

                        // Crear patente
                        var patenteRegPatent = new Patente
                        {
                            IdComponent = Guid.NewGuid(),
                            Name = "MenuRegPatent",
                            FormName = "MenuRegPatent"
                        };

                        permissionSLService.SavePatent(patenteRegPatent);
                        permissionSLService.AssignFamiliesToUser(defaultUser.UserId, new List<Guid> { adminFamily.IdComponent });
                    }

                    // Insertar usuario con la familia creada
                    userSLService.Insert(defaultUser, adminFamily.IdComponent);

                    MessageBox.Show(
                        $"Se creó un usuario administrador por defecto:\nUsuario: {defaultUsername}\nContraseña: {defaultPassword}",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error inesperado al inicializar la pantalla de Login: {ex}",
                    System.Diagnostics.Tracing.EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al inicializar el sistema. " +
                    "Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Limpia el texto de placeholder del campo Usuario
        /// y cambia el color cuando el control recibe el foco.
        /// </summary>
        private void txtUser_Enter(object sender, EventArgs e)
        {
            if (txtUser.Text == "Usuario")
            {
                txtUser.Text = "";
                txtUser.ForeColor = Color.DarkGreen;
            }
        }

        /// <summary>
        /// Restaura el texto de placeholder del campo Usuario
        /// cuando el control pierde el foco y está vacío.
        /// </summary>
        private void txtUser_Leave(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
            {
                txtUser.Text = "Usuario";
                txtUser.ForeColor = Color.SeaGreen;
            }
        }

        /// <summary>
        /// Limpia el texto de placeholder del campo Contraseña,
        /// activa el modo de contraseña y cambia el color al recibir foco.
        /// </summary>
        private void txtPass_Enter(object sender, EventArgs e)
        {
            if (txtPass.Text == "Contraseña")
            {
                txtPass.Text = "";
                txtPass.ForeColor = Color.DarkGreen;
                txtPass.UseSystemPasswordChar = true;
            }
        }

        /// <summary>
        /// Restaura el placeholder de la contraseña y desactiva el modo oculto
        /// cuando el campo está vacío al perder el foco.
        /// </summary>
        private void txtPass_Leave(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                txtPass.Text = "Contraseña";
                txtPass.ForeColor = Color.SeaGreen;
                txtPass.UseSystemPasswordChar = false;
            }
        }

        /// <summary>
        /// Maneja el evento de click del botón Acceder.
        /// Realiza el intento de login, guarda el usuario en sesión,
        /// carga los permisos y abre el formulario principal.
        /// Maneja errores de negocio y errores inesperados.
        /// </summary>
        private void btnToAccess_Click(object sender, EventArgs e)
        {
            this.Translate();

            var login = txtUser.Text.Trim();
            var password = txtPass.Text.Trim();

            try
            {
                var service = new LoginService();
                if (service.TryLogin(login, password, out var usuario, out var message))
                {
                    // Guardar usuario en sesión
                    UI.Helpers.Session.User = usuario;

                    // 1) Traer patentes planas del usuario y armar el set de formularios permitidos
                    var permisoService = new SL.Service.PermissionSLService();
                    var allowedForms = new HashSet<string>(
                        permisoService.GetPatentesByUser(usuario.UserId)
                                      .Where(p => !string.IsNullOrWhiteSpace(p.FormName))
                                      .Select(p => p.FormName),
                        StringComparer.OrdinalIgnoreCase);

                    // 2) Abrir el menú principal y aplicar autorización
                    var frm = new barraTitulo();
                    // frm.SetAllowedForms(allowedForms);
                    // frm.ApplyAuthorization();
                    frm.Show();

                    MessageBox.Show(
                        $"Bienvenido {usuario.FirstName} {usuario.LastName}",
                        "Acceso concedido");

                    this.Hide();
                }
                else
                {
                    MessageBox.Show(
                        message,
                        "Error de autenticación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error inesperado al intentar iniciar sesión: {ex}",
                    System.Diagnostics.Tracing.EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al intentar iniciar sesión. " +
                    "Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cierra la aplicación desde el botón de cierre del formulario.
        /// </summary>
        private void btnCerrarLogin_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Maximiza la ventana de login y alterna la visibilidad
        /// de los botones de maximizar/restaurar.
        /// </summary>
        private void btnMaximizarLogin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizarLogin.Visible = false;
            btnRestaurarLogin.Visible = true;
        }

        /// <summary>
        /// Restaura la ventana de login a tamaño normal y alterna
        /// la visibilidad de los botones de maximizar/restaurar.
        /// </summary>
        private void btnRestaurarLogin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnMaximizarLogin.Visible = true;
            btnRestaurarLogin.Visible = false;
        }

        /// <summary>
        /// Minimiza la ventana de login.
        /// </summary>
        private void btnMinimizarLogin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        /// <summary>
        /// Permite arrastrar la ventana haciendo click y arrastre sobre el formulario.
        /// </summary>
        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}

