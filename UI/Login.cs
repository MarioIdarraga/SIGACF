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
using SL.Helpers;
using SL.BLL;
using SL.Service;
using UI.Helpers;

namespace UI
{
    public partial class Login : Form
    {
        private object _panelContenedor;
        private object panelContenedor;

        public Login() // eliminá el parámetro si no se usa
        {
            InitializeComponent();

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
                    State = 1,
                };

                defaultUser.DVH = DVHHelper.CalcularDVH(defaultUser);
                userSLService.Insert(defaultUser);

                MessageBox.Show($"Se creó un usuario administrador por defecto:\nUsuario: {defaultUsername}\nContraseña: {defaultPassword}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtUser_Enter(object sender, EventArgs e)
        {
            if (txtUser.Text == "Usuario")
            {
                txtUser.Text = "";
                txtUser.ForeColor = Color.DarkGreen;
            }
        }

        private void txtUser_Leave(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
            {
                txtUser.Text = "Usuario";
                txtUser.ForeColor = Color.SeaGreen;
            }

        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            if (txtPass.Text == "Contraseña")
            {
                txtPass.Text = "";
                txtPass.ForeColor = Color.DarkGreen;
                txtPass.UseSystemPasswordChar = true;
            }
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                txtPass.Text = "Contraseña";
                txtPass.ForeColor = Color.SeaGreen;
                txtPass.UseSystemPasswordChar = false;
            }
        }

        private void btnToAccess_Click(object sender, EventArgs e)
        {
            this.Translate();

            string login = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();

            LoginService service = new LoginService();
            User usuario;
            string message;

            if (service.TryLogin(login, password, out usuario, out message))
            {

                var permisoService = new PermissionSLService();
                UI.Helpers.Session.User = usuario;

                MessageBox.Show($"Bienvenido {usuario.FirstName} {usuario.LastName}", "Acceso concedido");

                var frm = new barraTitulo();
                frm.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show(message, "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCerrarLogin_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximizarLogin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizarLogin.Visible = false;
            btnRestaurarLogin.Visible = true;
        }

        private void btnRestaurarLogin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnMaximizarLogin.Visible = true;
            btnRestaurarLogin.Visible = false;
        }

        private void btnMinimizarLogin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
