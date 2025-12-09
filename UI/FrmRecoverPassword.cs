using SL;
using SL.Service;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UI.Helpers;

namespace UI
{
    public partial class FrmRecoverPassword : Form
    {
        /// <summary>
        /// Texto placeholder mostrado dentro del campo de usuario/correo.
        /// </summary>
        private const string PlaceholderText = "Ingrese su usuario o correo (*)";

        /// <summary>
        /// Importación Win32 para permitir arrastrar el formulario.
        /// </summary>
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        /// <summary>
        /// Importación Win32 para enviar mensajes a la ventana del formulario.
        /// </summary>
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        /// <summary>
        /// Inicializa el formulario y configura el placeholder.
        /// </summary>
        public FrmRecoverPassword()
        {
            InitializeComponent();
            this.Translate(); 

            this.MouseDown += new MouseEventHandler(this.FrmRecoverPassword_MouseDown);

            txtDato.Text = PlaceholderText;
            txtDato.ForeColor = Color.SeaGreen;

            this.Shown += (s, e) => this.ActiveControl = null;
        }

        /// <summary>
        /// Permite mover el formulario haciendo clic y arrastrando.
        /// </summary>
        private void FrmRecoverPassword_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        /// <summary>
        /// Cierra la ventana de recuperación.
        /// </summary>
        private void btnCerrarRecoveryPassword_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Maximiza la ventana y muestra botón de restaurar.
        /// </summary>
        private void btnMaximizarLogin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizarLogin.Visible = false;
            btnRestaurarRecoveryPassword.Visible = true;
        }

        /// <summary>
        /// Restaura el tamaño normal de la ventana y alterna los botones.
        /// </summary>
        private void btnRestaurarLogin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnMaximizarLogin.Visible = true;
            btnRestaurarRecoveryPassword.Visible = false;
        }

        /// <summary>
        /// Minimiza la ventana.
        /// </summary>
        private void btnMinimizarRecoveryPassword_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Envía una solicitud de recuperación de contraseña.
        /// Valida datos, registra logs y maneja excepciones.
        /// </summary>
        private void btnEnviarRecoveryPassword_Click(object sender, EventArgs e)
        {
            try
            {
                string dato = txtDato.Text.Trim();

                if (string.IsNullOrWhiteSpace(dato) ||
                    dato == PlaceholderText ||
                    dato.Length < 3)
                {
                    MessageBox.Show(
                        "Debe ingresar un usuario o correo válido.",
                        "Dato inválido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                var service = new SL.Service.PasswordRecoverySLService();
                service.GenerateRecoveryRequest(dato);

                MessageBox.Show(
                    "Si los datos ingresados son correctos, recibirá un correo con instrucciones.",
                    "Solicitud enviada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Hide();

                var frm = new FrmResetPassword();
                frm.ShowDialog();

                this.Close();
            }
            catch (BLL.BusinessException.BusinessException ex)
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
                    $"Error inesperado en FrmRecoverPassword: {ex}",
                    System.Diagnostics.Tracing.EventLevel.Error,
                    SL.Helpers.Session.CurrentUser?.LoginName);

                MessageBox.Show(
                    "Ocurrió un error inesperado. Intente nuevamente.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Limpia el placeholder al ingresar al campo de texto.
        /// </summary>
        private void txtDato_Enter(object sender, EventArgs e)
        {
            if (txtDato.Text == PlaceholderText)
            {
                txtDato.Text = string.Empty;
                txtDato.ForeColor = Color.SeaGreen;
            }
        }

        /// <summary>
        /// Restaura el placeholder cuando el campo queda vacío.
        /// </summary>
        private void txtDato_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDato.Text))
            {
                txtDato.Text = PlaceholderText;
                txtDato.ForeColor = Color.SeaGreen; 
            }
        }
    }
}

