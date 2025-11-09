using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;
using SL;
using SL.Service;
using UI.Helpers;

namespace UI
{
    public partial class barraTitulo : Form
    {

        private readonly HashSet<string> _allowedForms = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        public void SetAllowedForms(IEnumerable<string> forms) { _allowedForms.Clear(); foreach (var f in forms) if (!string.IsNullOrWhiteSpace(f)) _allowedForms.Add(f); }

        public barraTitulo()
        {
            InitializeComponent();

            // Cargar opciones de idioma en el ComboBox
            cmbLanguage.Items.Add("Español");
            cmbLanguage.Items.Add("Inglés");
            cmbLanguage.Items.Add("Portugués");
            cmbLanguage.SelectedIndex = 0;

            //Mapeo de botones para permisos
            btnClientes.Tag = "MenuFindCustomers";
            btnAlquiler.Tag = "MenuSales";
            btnPay.Tag = "MenuPay";
            btnCan.Tag = "MenuCan";
            bntReportes.Tag = "MenuRep";
            btnAdmin.Tag = "MenuAdmin";

            this.Translate();

            //ApplyAuthorization();


            PermissionSLService permissionSL = new PermissionSLService();
            User currentUser = Session.User;

        }

        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLanguage = cmbLanguage.SelectedItem.ToString();

            if (selectedLanguage == "Español")
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-AR");
            else if (selectedLanguage == "Inglés")
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            else if (selectedLanguage == "Portugués")
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");

            this.Translate(); // Método de extensión para traducir textos
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

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void OpenFormChild(object formchild)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
            Form fh = formchild as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fh);
            this.panelContenedor.Tag = fh;
            fh.Show();
        
        }

        //private void ShowIfAllowed(Button btn)
        //{
        //    var formName = btn.Tag as string;
        //    btn.Visible = (!string.IsNullOrWhiteSpace(formName) && _allowedForms.Contains(formName));
        //}

        //private void HideAllMenuItems()
        //{
        //    foreach (Control c in menuVertical.Controls)
        //    {
        //        // Ocultamos todo lo que “parece” un ítem de menú (tenga Tag o sea boton-like)
        //        if (c is ButtonBase || c.Tag != null)
        //            c.Visible = false;
        //    }
        //}

        //public void ApplyAuthorization()
        //{
        //    // Debug de AllowedForms y Tags
        //    var sb = new StringBuilder();
        //    sb.AppendLine("AllowedForms:");
        //    foreach (var f in _allowedForms) sb.AppendLine(" - " + f);
        //    sb.AppendLine("Tags:");
        //    foreach (Control c in menuVertical.Controls)
        //        sb.AppendLine($" - {c.Name} -> {c.Tag}");
        //    MessageBox.Show(sb.ToString(), "DEBUG permisos");

        //    HideAllMenuItems();

        //    // Mostrar solo los que corresponde (llama con tus controles reales)
        //    ShowIfAllowed(btnClientes);
        //    ShowIfAllowed(btnAlquiler);
        //    ShowIfAllowed(btnPay);
        //    ShowIfAllowed(btnCan);
        //    ShowIfAllowed(bntReportes);
        //    ShowIfAllowed(btnAdmin);
        //}


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
