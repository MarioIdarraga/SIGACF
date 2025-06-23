using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;
using SL;
using SL.BLL;
using SL.Service;
using UI.Helpers;

namespace UI
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.Translate();
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
                Session.User = usuario;

                Session.User = usuario;

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
