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
    public partial class barraTitulo : Form
    {
        private readonly HashSet<string> _allowedForms = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Guarda el tipo del formulario hijo actualmente abierto,
        /// para poder recargarlo cuando cambia el idioma.
        /// </summary>
        private Type _currentChildFormType;

        public void SetAllowedForms(IEnumerable<string> forms)
        {
            _allowedForms.Clear();
            foreach (var f in forms)
                if (!string.IsNullOrWhiteSpace(f))
                    _allowedForms.Add(f);
        }

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
        /// Maneja el cambio de idioma desde el combo.
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
        /// Recarga el formulario hijo actual con la cultura/idioma vigentes.
        /// </summary>
        private void ReloadCurrentChild()
        {
            if (_currentChildFormType == null)
                return;

            Form newForm = (Form)Activator.CreateInstance(_currentChildFormType, this.panelContenedor);
            OpenFormChild(newForm);
        }

        /// <summary>
        /// Oculta o muestra los botones dependiendo de las patentes/familias del usuario.
        /// </summary>
        private void ApplyAuthorization()
        {
            try
            {
                var user = Session.CurrentUser;
                if (user == null)
                    return;

                PermissionSLService permissionSL = new PermissionSLService();

                // Obtiene los permisos del usuario (lista de strings: "MenuPay", "MenuSales", etc.)
                IEnumerable<string> allowed = permissionSL.GetAllowedComponentsForUser(user.UserId);

                // Guarda internamente para usar más adelante si querés
                SetAllowedForms(allowed);

                // Recorre todos los botones del menú que tienen un Tag con el nombre del formulario
                foreach (Control ctrl in menuVertical.Controls)
                {
                    if (ctrl is Button btn && btn.Tag != null)
                    {
                        string component = btn.Tag.ToString();

                        // Si el usuario NO tiene permiso → lo ocultamos
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


        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnMaximizar.Visible = true;
            btnRestaurar.Visible = false;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor y guarda su tipo.
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

        private void btnClientes_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindCustomers(this.panelContenedor));
        }

        private void btnAlquiler_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuSales(this.panelContenedor));
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuPay(this.panelContenedor));
        }

        private void btnCan_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuCan(this.panelContenedor));
        }

        private void bntReportes_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRep(this.panelContenedor));
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuAdmin(this.panelContenedor));
        }

        private void btnManuals_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuManuals(this.panelContenedor));
        }

        /// <summary>
        /// Abre el archivo de ayuda (CHM) en el idioma actual.
        /// </summary>
        private void ShowHelpSection()
        {
            try
            {
                string language = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

                // Carpeta base: donde está el EXE en tiempo de ejecución
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
        /// Devuelve la ruta del tema de ayuda asociado a este formulario según el idioma actual.
        /// (Por ahora no se usa, pero queda listo si luego querés abrir un tópico puntual.)
        /// </summary>
        private string GetHelpTopic(string language)
        {
            string prefix = language == "en" ? "en/" : "es/";
            return prefix + "menu.html";
        }

        /// <summary>
        /// Captura la tecla F1 y abre la ayuda correspondiente.
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


