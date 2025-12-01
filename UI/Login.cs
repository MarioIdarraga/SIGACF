using BLL.BusinessException;
using Domain;
using SL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UI.Helpers;

namespace UI
{
    public partial class Login : Form
    {

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        /// <summary>
        /// Permite arrastrar el formulario desde el panel designado.
        /// </summary>
        private void panelLogin_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        public Login()
        {
            InitializeComponent();

            this.MouseDown += new MouseEventHandler(this.panelLogin_MouseDown);
            this.panel1.MouseDown += new MouseEventHandler(this.panelLogin_MouseDown);
        }

        /// <summary>
        /// Maneja el intento de acceso del usuario desde la UI.
        /// Llama a LoginSLService y actúa según el mensaje devuelto.
        /// </summary>
        private void btnToAccess_Click(object sender, EventArgs e)
        {
            try
            {
                //this.Translate();

                string login = txtUser.Text.Trim();
                string password = txtPass.Text.Trim();

                var loginService = new LoginSLService();

                bool success = loginService.TryLogin(login, password, out User usuario, out string message);

                if (!success)
                {
                    if (message.Contains("cambiar su contraseña"))
                    {
                        MessageBox.Show(
                            message,
                            "Cambio de contraseña requerido",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        var frmChange = new FrmForcePasswordChange(usuario);
                        frmChange.ShowDialog();
                        return;
                    }

                    if (message.ToLower().Contains("bloque"))
                    {
                        MessageBox.Show(
                            message,
                            "Usuario bloqueado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    MessageBox.Show(
                        message,
                        "Error de autenticación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                SL.Helpers.Session.CurrentUser = usuario;

                var permisoService = new SL.Service.PermissionSLService();
                var allowedForms = new HashSet<string>(
                    permisoService.GetPatentesByUser(usuario.UserId)
                                  .Where(p => !string.IsNullOrWhiteSpace(p.FormName))
                                  .Select(p => p.FormName),
                    StringComparer.OrdinalIgnoreCase);

                SL.Helpers.Session.AllowedForms = allowedForms;

                var frm = new barraTitulo();
                frm.Show();

                MessageBox.Show(
                    $"Bienvenido {usuario.FirstName} {usuario.LastName}",
                    "Acceso concedido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Hide();
            }
            catch (BusinessException bx)
            {
                MessageBox.Show(
                    bx.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Ha ocurrido un error inesperado. Intente nuevamente.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void txtUser_Enter(object sender, EventArgs e)
        {
            if (txtUser.Text == "Usuario")
                txtUser.Text = "";
        }

        private void txtUser_Leave(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
                txtUser.Text = "Usuario";
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            if (txtPass.Text == "Contraseña")
            {
                txtPass.Text = "";
                txtPass.UseSystemPasswordChar = true;
            }
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                txtPass.UseSystemPasswordChar = false;
                txtPass.Text = "Contraseña";
            }
        }

        private void lnkPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new FrmRecoverPassword();
            frm.ShowDialog();
        }

        private void btnCerrarLogin_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRestaurarLogin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnMaximizarLogin.Visible = true;
            btnRestaurarLogin.Visible = false;
        }

        private void btnMaximizarLogin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizarLogin.Visible = false;
            btnRestaurarLogin.Visible = true;
        }

        private void btnMinimizarLogin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
