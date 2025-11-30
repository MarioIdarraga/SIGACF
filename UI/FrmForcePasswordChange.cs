using BLL.BusinessException;
using BLL.Service;
using Domain;
using SL.Service;          // CORRECTO: acá vive UserSLService
using System;
using System.Windows.Forms;
using UI.Helpers;          // CORRECTO: acá vive FormHelper

namespace UI
{
    /// <summary>
    /// Formulario para cambiar la contraseña cuando el usuario
    /// debe actualizarla obligatoriamente (estado 0).
    /// </summary>
    public partial class FrmForcePasswordChange : Form
    {
        private readonly User _user;
        private readonly UserSLService _userSLService;

        /// <summary>
        /// Inicializa el formulario y recibe el usuario a modificar.
        /// </summary>
        public FrmForcePasswordChange(User user)
        {
            InitializeComponent();
            _user = user;
            _userSLService = new UserSLService(new UserService());
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Guarda el cambio de contraseña del usuario.
        /// </summary>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string newPass = txtNewPassword.Text.Trim();
                string confirmPass = txtConfirmPassword.Text.Trim();

                if (string.IsNullOrWhiteSpace(newPass))
                    throw new BusinessException("Debe ingresar una nueva contraseña.");

                if (string.IsNullOrWhiteSpace(confirmPass))
                    throw new BusinessException("Debe confirmar la contraseña.");

                if (newPass != confirmPass)
                    throw new BusinessException("Las contraseñas no coinciden.");

                _user.Password = newPass;
                _user.State = 1;

                _userSLService.Update(_user, Guid.Empty);

                MessageBox.Show(
                    "La contraseña se ha actualizado correctamente.",
                    "Operación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Close();
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
                    "Ocurrió un error inesperado al intentar actualizar la contraseña.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCerrarPasswordChange_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizarPasswordChange_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnRestaurarPasswordChange_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }
    }
}

