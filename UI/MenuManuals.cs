using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Factory;
using SL.Service.Extension;
using UI.Helpers;

namespace UI
{
    public partial class MenuManuals : Form
    {
        private Panel _panelContenedor;
        public MenuManuals(Panel panelContenedor)
        {

            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); //Traducir

        }

        private void OpenFormChild(object formchild)
        {
            if (_panelContenedor.Controls.Count > 0)
                _panelContenedor.Controls.RemoveAt(0);
            Form fh = formchild as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            _panelContenedor.Controls.Add(fh);
            _panelContenedor.Tag = fh;
            fh.Show();
        }

        /// <summary>
        /// Guarda un manual PDF permitiendo al usuario seleccionar la ubicación.
        /// </summary>
        /// <param name="manualName">Nombre del archivo PDF dentro de la carpeta Manuals.</param>
        private void SaveManual(string manualName)
        {
            try
            {
                // Ruta donde está el PDF dentro del ejecutable
                string sourcePath = Path.Combine(Application.StartupPath, "Manuals", manualName);

                if (!File.Exists(sourcePath))
                {
                    MessageBox.Show("No se encontró el manual especificado: " + manualName);
                    return;
                }

                // Diálogo de guardado
                using (SaveFileDialog saveFile = new SaveFileDialog())
                {
                    saveFile.FileName = manualName;
                    saveFile.Filter = "PDF files (*.pdf)|*.pdf";

                    if (saveFile.ShowDialog() == DialogResult.OK)
                    {
                        File.Copy(sourcePath, saveFile.FileName, true);
                        MessageBox.Show("Manual guardado en: " + saveFile.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el manual. " + ex.Message);
            }
        }
        private void btnUserManual_Click(object sender, EventArgs e)
        {
            SaveManual("ManualUsuario.pdf");
        }

        private void btnDeveloperManual_Click(object sender, EventArgs e)
        {
            SaveManual("ManualDesarrollador.pdf");
        }

        private void btnInstallManual_Click(object sender, EventArgs e)
        {
            SaveManual("ManualInstalación.pdf");
        }
    }
}
