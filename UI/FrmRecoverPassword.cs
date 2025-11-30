using SL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class FrmRecoverPassword : Form
    {
        public FrmRecoverPassword()
        {
            InitializeComponent();
        }


        private void btnCerrarRecoveryPassword_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnMaximizarLogin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizarLogin.Visible = false;
            btnRestaurarRecoveryPassword.Visible = true;
        }

        /// <summary>
        /// Restaura la ventana de login a tamaño normal y alterna
        /// la visibilidad de los botones de maximizar/restaurar.
        /// </summary>
        private void btnRestaurarLogin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnMaximizarLogin.Visible = true;
            btnRestaurarRecoveryPassword.Visible = false;
        }

        /// <summary>
        /// Minimiza la ventana de login.
        /// </summary>
        private void btnMinimizarRecoveryPassword_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Maneja el evento del botón "Enviar Solicitud".
        /// Envía la solicitud de recuperación mediante la capa SL.
        /// Registra logs y maneja excepciones.
        /// </summary>
        private void btnEnviarRecoveryPassword_Click(object sender, EventArgs e)
        {
            try
            {
                string dato = txtDato.Text.Trim();

                if (string.IsNullOrWhiteSpace(dato) || dato.Length < 3)
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
                    SL.Helpers.Session.CurrentUser.LoginName);

                MessageBox.Show(
                    "Ocurrió un error inesperado. Intente nuevamente.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnMaximizarRecoveryPassword_Click_1(object sender, EventArgs e)
        {

        }
    }
}
