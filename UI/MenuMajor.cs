using Domain;
using SL;
using SL.Helpers;
using SL.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
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

            // Cargar opciones de idioma en el ComboBox
            cmbLanguage.Items.Add("Español");
            cmbLanguage.Items.Add("Inglés");
            cmbLanguage.SelectedIndex = 0;

            // Mapeo de botones para permisos
            btnClientes.Tag = "MenuFindCustomers";
            btnAlquiler.Tag = "MenuSales";
            btnPay.Tag = "MenuPay";
            btnCan.Tag = "MenuCan";
            bntReportes.Tag = "MenuRep";
            btnAdmin.Tag = "MenuAdmin";

            this.Translate();

            //ApplyAuthorization();  // cuando quieras reactivar permisos

            PermissionSLService permissionSL = new PermissionSLService();
            User currentUser = Session.CurrentUser;
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
    }
}


