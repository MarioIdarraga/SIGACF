using BLL.BusinessException;
using BLL.Service;
using Domain;
using SL.Service;          
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UI.Helpers;          

namespace UI
{
    /// <summary>
    /// Formulario para cambio obligatorio de contraseña.
    /// </summary>
    public partial class FrmForcePasswordChange : Form
    {
        private readonly User _user;
        private readonly UserSLService _userSLService;

        private const string PlaceholderNew = "Ingrese su nueva contraseña";
        private const string PlaceholderConfirm = "Re ingrese su nueva contraseña";

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        /// <summary>
        /// Permite mover el formulario desde zonas habilitadas.
        /// </summary>
        private void FrmForcePasswordChange_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        /// <summary>
        /// Inicializa y establece placeholders.
        /// </summary>
        public FrmForcePasswordChange(User user)
        {
            InitializeComponent();
            this.Translate(); // Localización

            if (user == null)
                throw new BusinessException("No se pudo cargar el usuario para cambiar la contraseña.");

            _user = user;
            _userSLService = new UserSLService(new UserService());

            txtNewPassword.Text = PlaceholderNew;
            txtNewPassword.ForeColor = Color.SeaGreen;

            txtConfirmPassword.Text = PlaceholderConfirm;
            txtConfirmPassword.ForeColor = Color.SeaGreen;

            btnCerrarPasswordChange.MouseDown += FrmForcePasswordChange_MouseDown;
            btnMinimizarPasswordChange.MouseDown += FrmForcePasswordChange_MouseDown;
            btnRestaurarPasswordChange.MouseDown += FrmForcePasswordChange_MouseDown;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Guarda el cambio de contraseña.
        /// </summary>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string newPass = txtNewPassword.Text.Trim();
                string confirmPass = txtConfirmPassword.Text.Trim();

                if (newPass == PlaceholderNew)
                    throw new BusinessException("Debe ingresar una nueva contraseña.");

                if (confirmPass == PlaceholderConfirm)
                    throw new BusinessException("Debe confirmar la contraseña.");

                if (newPass != confirmPass)
                    throw new BusinessException("Las contraseñas no coinciden.");

                _user.Password = newPass;
                _user.State = 1;

                _userSLService.Update(_user, null);

                MessageBox.Show(
                    "La contraseña se ha actualizado correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Close();
            }
            catch (BusinessException bx)
            {
                MessageBox.Show(bx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch
            {
                MessageBox.Show(
                    "Ocurrió un error inesperado al actualizar la contraseña.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCerrarPasswordChange_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRestaurarPasswordChanged_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnMaximizarLogin.Visible = true;
            btnRestaurarPasswordChange.Visible = false;
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizarLogin.Visible = false;
            btnRestaurarPasswordChange.Visible = true;
        }

        private void txtNewPassword_Enter(object sender, EventArgs e)
        {
            if (txtNewPassword.Text == PlaceholderNew)
            {
                txtNewPassword.Text = "";
                txtNewPassword.ForeColor = Color.LightGray;
                txtNewPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtNewPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                txtNewPassword.UseSystemPasswordChar = false;
                txtNewPassword.Text = PlaceholderNew;
                txtNewPassword.ForeColor = Color.SeaGreen;
            }
        }


        private void txtConfirmPassword_Enter(object sender, EventArgs e)
        {
            if (txtConfirmPassword.Text == PlaceholderConfirm)
            {
                txtConfirmPassword.Text = "";
                txtConfirmPassword.ForeColor = Color.LightGray;
                txtConfirmPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtConfirmPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                txtConfirmPassword.UseSystemPasswordChar = false;
                txtConfirmPassword.Text = PlaceholderConfirm;
                txtConfirmPassword.ForeColor = Color.SeaGreen;
            }
        }

        private void btnMaximizarPasswordChanged_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizarLogin.Visible = false;
            btnMaximizarLogin.Visible = true;
        }

        private void btnRestaurarPasswordChange_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestaurarPasswordChange.Visible = true;
            btnRestaurarPasswordChange.Visible = false;
        }
    }
}


