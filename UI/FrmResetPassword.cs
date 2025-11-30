using BLL.BusinessException;
using BLL.Service;
using SL;
using SL.Helpers;
using SL.Services;
using System;
using System.Diagnostics.Tracing;
using System.Windows.Forms;

namespace UI
{
    /// <summary>
    /// Formulario utilizado para restablecer la contraseña del usuario
    /// mediante un token enviado por correo electrónico. Valida datos,
    /// procesa el reseteo y actualiza el estado del usuario en la base.
    /// </summary>
    public partial class FrmResetPassword : Form
    {
        private readonly LoginSLService _loginService;

        /// <summary>
        /// Inicializa el formulario de restablecimiento de contraseña.
        /// </summary>
        public FrmResetPassword()
        {
            InitializeComponent();
            _loginService = new LoginSLService();
            ApplyPlaceholders();
        }

        #region UI Helpers

        /// <summary>
        /// Configura los textos iniciales de los campos (placeholders).
        /// </summary>
        private void ApplyPlaceholders()
        {
            txtToken.Text = "Ingrese el código recibido";
            txtNewPassword.Text = "Nueva contraseña";
            txtConfirmPassword.Text = "Repetir contraseña";
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento del botón Confirmar. Valida los campos, solicita el reseteo de contraseña
        /// y actualiza la base de datos si el token es válido.
        /// </summary>
        private void btnConfirmarFrmResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                string token = txtToken.Text.Trim();
                string pass1 = txtNewPassword.Text.Trim();
                string pass2 = txtConfirmPassword.Text.Trim();

                if (string.IsNullOrWhiteSpace(token))
                    throw new BusinessException("Debe ingresar el código recibido por correo.");

                if (string.IsNullOrWhiteSpace(pass1))
                    throw new BusinessException("Debe ingresar una nueva contraseña.");

                if (string.IsNullOrWhiteSpace(pass2))
                    throw new BusinessException("Debe confirmar la nueva contraseña.");

                if (pass1 != pass2)
                    throw new BusinessException("Las contraseñas no coinciden.");

                // Procesar el reseteo de contraseña
                if (_loginService.ResetPassword(token, pass1, out string message))
                {
                    LoggerService.Log(
                        $"Contraseña restablecida correctamente mediante token: {token}",
                        EventLevel.Informational);

                    MessageBox.Show(
                        message,
                        "Operación exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.Close();
                }
                else
                {
                    LoggerService.Log(
                        $"Fallo al restablecer contraseña: {message}",
                        EventLevel.Warning);

                    MessageBox.Show(
                        message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (BusinessException bx)
            {
                MessageBox.Show(
                    bx.Message,
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error inesperado en FrmResetPassword: {ex.Message}",
                    EventLevel.Error);

                MessageBox.Show(
                    "Ocurrió un error inesperado durante el restablecimiento.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cierra el formulario sin realizar ninguna acción.
        /// </summary>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
