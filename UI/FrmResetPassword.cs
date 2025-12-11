using BLL.BusinessException;
using SL;
using SL.Helpers;
using SL.Service;
using SL.Services;
using System;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UI.Helpers;

namespace UI
{
    /// <summary>
    /// Formulario utilizado para restablecer la contraseña del usuario mediante
    /// un token enviado por correo electrónico.
    /// Maneja placeholders, validaciones, reseteo y logs.
    /// </summary>
    public partial class FrmResetPassword : Form
    {
        private readonly LoginSLService _loginService;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        /// <summary>
        /// Permite mover el formulario desde cualquier parte del mismo.
        /// </summary>
        private void FrmResetPassword_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        /// <summary>
        /// Inicializa el formulario, aplica traducciones y configura placeholders.
        /// </summary>
        public FrmResetPassword()
        {
            InitializeComponent();
            this.Translate();
            _loginService = new LoginSLService();

            ApplyPlaceholders();

            // Habilitar drag en el formulario y botones
            this.MouseDown += FrmResetPassword_MouseDown;
            btnCerrarResetPassword.MouseDown += FrmResetPassword_MouseDown;
        }

        /// <summary>
        /// Configura los textos iniciales (placeholders) en todos los campos del formulario.
        /// </summary>
        private void ApplyPlaceholders()
        {
            SetInitialPlaceholder(txtToken, "Ingrese aquí el código recibido");
            SetInitialPlaceholder(txtNewPassword, "Nueva Contraseña");
            SetInitialPlaceholder(txtConfirmPassword, "Repetir Contraseña");
        }

        /// <summary>
        /// Establece el placeholder inicial para un TextBox específico.
        /// </summary>
        private void SetInitialPlaceholder(TextBox txt, string placeholder)
        {
            txt.Text = placeholder;
            txt.ForeColor = Color.SeaGreen;

            txt.Enter += (s, e) => RemovePlaceholder(txt, placeholder);
            txt.Leave += (s, e) => SetPlaceholder(txt, placeholder);
        }

        /// <summary>
        /// Quita el placeholder al ingresar al TextBox.
        /// </summary>
        private void RemovePlaceholder(TextBox txt, string placeholder)
        {
            if (txt.Text == placeholder)
            {
                txt.Text = "";
                txt.ForeColor = Color.White;

                if (txt == txtNewPassword || txt == txtConfirmPassword)
                    txt.PasswordChar = '*';
            }
        }

        /// <summary>
        /// Restaura el placeholder si el TextBox queda vacío.
        /// </summary>
        private void SetPlaceholder(TextBox txt, string placeholder)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                txt.PasswordChar = '\0';
                txt.Text = placeholder;
                txt.ForeColor = Color.SeaGreen;
            }
        }

        /// <summary>
        /// Valida los datos ingresados y realiza el reseteo de contraseña utilizando el token.
        /// Muestra mensajes informativos y registra logs.
        /// </summary>
        private void btnConfirmarFrmResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                string token = txtToken.Text.Trim();
                string pass1 = txtNewPassword.Text.Trim();
                string pass2 = txtConfirmPassword.Text.Trim();

                if (token == "" || token == "Ingrese aquí el código recibido")
                    throw new BusinessException("Debe ingresar el código recibido por correo.");

                if (pass1 == "" || pass1 == "Nueva Contraseña")
                    throw new BusinessException("Debe ingresar una nueva contraseña.");

                if (pass2 == "" || pass2 == "Repetir Contraseña")
                    throw new BusinessException("Debe confirmar la nueva contraseña.");

                if (pass1 != pass2)
                    throw new BusinessException("Las contraseñas no coinciden.");

                string message;

                if (_loginService.ResetPassword(token, pass1, out message))
                {
                    LoggerService.Log("Contraseña restablecida correctamente.", EventLevel.Informational);
                    MessageBox.Show(message, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error inesperado en FrmResetPassword: {ex}", EventLevel.Error);
                MessageBox.Show("Ocurrió un error inesperado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cierra el formulario sin realizar ninguna acción.
        /// </summary>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Minimiza el formulario.
        /// </summary>
        private void btnMinimizarResetPassword_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Restaura el formulario desde modo maximizado.
        /// </summary>
        private void btnRestaurarResetPassword_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnMaximizarLogin.Visible = true;
            btnRestaurarResetPassword.Visible = false;
        }

        /// <summary>
        /// Variante adicional que restaura el formulario a tamaño normal.
        /// </summary>
        private void btnRestaurarReset_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnMaximizarLogin.Visible = true;
            btnRestaurarResetPassword.Visible = false;
        }

        /// <summary>
        /// Maximiza el formulario.
        /// </summary>
        private void btnMaximizarRecoveryPassword_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizarLogin.Visible = false;
            btnMaximizarLogin.Visible = true;
        }

        /// <summary>
        /// Cierra el formulario.
        /// </summary>
        private void btnCerrarRecoveryPassword_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}


