using Domain;
using SL;
using SL.Helpers;
using SL.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using UI.Helpers;

namespace UI
{
    /// <summary>
    /// Formulario principal de navegación del sistema.
    /// Gestiona idioma, permisos, carga de formularios hijos y acceso a la ayuda.
    /// </summary>
    public partial class barraTitulo : Form
    {
        private readonly HashSet<string> _allowedForms = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Guarda el tipo del formulario hijo actualmente abierto,
        /// para poder recargarlo cuando cambia el idioma.
        /// </summary>
        private Type _currentChildFormType;

        /// <summary>
        /// Establece los formularios permitidos según las patentes del usuario.
        /// </summary>
        public void SetAllowedForms(IEnumerable<string> forms)
        {
            _allowedForms.Clear();
            foreach (var f in forms)
                if (!string.IsNullOrWhiteSpace(f))
                    _allowedForms.Add(f);
        }

        /// <summary>
        /// Constructor principal. Inicializa componentes, idioma, eventos,
        /// y configura las etiquetas de permisos para los botones del menú.
        /// </summary>
        public barraTitulo()
        {
            InitializeComponent();

            this.KeyPreview = true;

            cmbLanguage.Items.Add("Español");
            cmbLanguage.Items.Add("Inglés");
            cmbLanguage.SelectedIndex = 0;

            btnClientes.Tag = "MenuFindCustomers";
            btnAlquiler.Tag = "MenuSales";
            btnPay.Tag = "MenuPay";
            btnCan.Tag = "MenuCan";
            bntReportes.Tag = "MenuRep";
            btnAdmin.Tag = "MenuAdmin";

            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);

            this.Translate();

            PermissionSLService permissionSL = new PermissionSLService();
            User currentUser = Session.CurrentUser;

            ApplyAuthorization();
        }

        /// <summary>
        /// Maneja el cambio de idioma desde el ComboBox.
        /// Cambia la cultura, traduce el formulario y recarga el formulario hijo actual.
        /// </summary>
        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedLanguage = cmbLanguage.SelectedItem.ToString();

                if (selectedLanguage == "Español")
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-AR");
                else if (selectedLanguage == "Inglés")
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

                this.Translate();
                ReloadCurrentChild();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cambiar idioma: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Recarga el formulario hijo actualmente cargado aplicando el nuevo idioma.
        /// </summary>
        private void ReloadCurrentChild()
        {
            if (_currentChildFormType == null)
                return;

            Form newForm = (Form)Activator.CreateInstance(_currentChildFormType, this.panelContenedor);
            OpenFormChild(newForm);
        }

        /// <summary>
        /// Aplica permisos a los botones del menú vertical según las patentes del usuario.
        /// </summary>
        private void ApplyAuthorization()
        {
            try
            {
                var user = Session.CurrentUser;
                if (user == null)
                    return;

                PermissionSLService permissionSL = new PermissionSLService();

                IEnumerable<string> allowed = permissionSL.GetAllowedComponentsForUser(user.UserId);

                SetAllowedForms(allowed);

                foreach (Control ctrl in menuVertical.Controls)
                {
                    if (ctrl is Button btn && btn.Tag != null)
                    {
                        string component = btn.Tag.ToString();
                        btn.Visible = allowed.Contains(component);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al aplicar permisos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cierra completamente la aplicación.
        /// </summary>
        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Maximiza el formulario principal.
        /// </summary>
        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
        }

        /// <summary>
        /// Restaura el formulario desde modo maximizado.
        /// </summary>
        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnMaximizar.Visible = true;
            btnRestaurar.Visible = false;
        }

        /// <summary>
        /// Minimiza el formulario.
        /// </summary>
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        /// <summary>
        /// Permite mover el formulario arrastrando el panel superior.
        /// </summary>
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        /// <summary>
        /// Abre un formulario hijo en el panel contenedor y guarda su tipo para recargas posteriores.
        /// </summary>
        private void OpenFormChild(Form formchild)
        {
            _currentChildFormType = formchild.GetType();

            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);

            formchild.TopLevel = false;
            formchild.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(formchild);
            this.panelContenedor.Tag = formchild;
            formchild.Show();
        }

        /// <summary>
        /// Abre el formulario de búsqueda de clientes.
        /// </summary>
        private void btnClientes_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindCustomers(this.panelContenedor));
        }

        /// <summary>
        /// Abre el formulario de alquileres / ventas.
        /// </summary>
        private void btnAlquiler_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuSales(this.panelContenedor));
        }

        /// <summary>
        /// Abre el menú de pagos.
        /// </summary>
        private void btnPay_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuPay(this.panelContenedor));
        }

        /// <summary>
        /// Abre el formulario para cancelar reservas.
        /// </summary>
        private void btnCan_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuCan(this.panelContenedor));
        }

        /// <summary>
        /// Abre el menú de reportes.
        /// </summary>
        private void bntReportes_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRep(this.panelContenedor));
        }

        /// <summary>
        /// Abre el menú de administración general.
        /// </summary>
        private void btnAdmin_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuAdmin(this.panelContenedor));
        }

        /// <summary>
        /// Abre el menú de manuales del sistema.
        /// </summary>
        private void btnManuals_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuManuals(this.panelContenedor));
        }

        /// <summary>
        /// Abre el archivo de ayuda (CHM) correspondiente al idioma actual.
        /// </summary>
        private void ShowHelpSection()
        {
            try
            {
                string language = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

                string basePath = System.IO.Path.Combine(Application.StartupPath, "Help");

                string helpFileName = language == "en"
                    ? "sigacf-help-en.chm"
                    : "sigacf-help-es.chm";

                string helpPath = System.IO.Path.Combine(basePath, helpFileName);

                if (!System.IO.File.Exists(helpPath))
                {
                    MessageBox.Show(
                        "No se encontró el archivo de ayuda: " + helpPath,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                Help.ShowHelp(this, helpPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible cargar la ayuda. " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Obtiene la ruta del tópico de ayuda asociado al formulario según el idioma.
        /// (Método auxiliar por si luego deseas abrir temas específicos.)
        /// </summary>
        private string GetHelpTopic(string language)
        {
            string prefix = language == "en" ? "en/" : "es/";
            return prefix + "menu.html";
        }

        /// <summary>
        /// Captura la tecla F1 y abre la sección de ayuda correspondiente.
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == Keys.F1)
                {
                    ShowHelpSection();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir la ayuda. " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}



