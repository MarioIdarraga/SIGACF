using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace UI
{
    public partial class barraTitulo : Form
    {
        public barraTitulo()
        {
            InitializeComponent();
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

        private void btnClientes_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuCustomers(this.panelContenedor));
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
    }
}
