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
using System.IO.Compression;

namespace UI
{
    /// <summary>
    /// Formulario que permite la descarga de manuales del sistema (Usuario, Desarrollador, Instalación).
    /// Gestiona la selección de ubicación y copia de archivos PDF incluidos con la aplicación.
    /// </summary>
    public partial class MenuManuals : Form
    {
        private Panel _panelContenedor;

        /// <summary>
        /// Constructor. Inicializa componentes, asigna panel contenedor y aplica traducciones.
        /// </summary>
        /// <param name="panelContenedor">Panel donde se abrirán formularios secundarios.</param>
        public MenuManuals(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); // Traducir interfaz
        }

        /// <summary>
        /// Carga un formulario hijo dentro del panel contenedor.
        /// Reemplaza cualquier formulario previamente abierto.
        /// </summary>
        /// <param name="formchild">Formulario a mostrar.</param>
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

        /// <summary>
        /// Descarga el manual de usuario en formato PDF.
        /// </summary>
        private void btnUserManual_Click(object sender, EventArgs e)
        {
            SaveManual("ManualUsuario.pdf");
        }

        /// <summary>
        /// Descarga el manual del desarrollador en formato PDF.
        /// </summary>
        private void btnDeveloperManual_Click(object sender, EventArgs e)
        {
            SaveDeveloperManualPackage();
        }

        /// <summary>
        /// Descarga el manual de instalación en formato PDF.
        /// </summary>
        private void btnInstallManual_Click(object sender, EventArgs e)
        {
            SaveManual("ManualInstalación.pdf");
        }

        private void SaveDeveloperManualPackage()
        {
            try
            {
                string folderPath = Path.Combine(Application.StartupPath, "Manuals", "DocumentacionProgramador");

                if (!Directory.Exists(folderPath))
                {
                    MessageBox.Show("No se encontró la documentación completa del programador.");
                    return;
                }

                using (SaveFileDialog save = new SaveFileDialog())
                {
                    save.Filter = "Archivo ZIP (*.zip)|*.zip";
                    save.FileName = "ManualDesarrollador_SIGACF.zip";

                    if (save.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(save.FileName))
                            File.Delete(save.FileName);

                        ZipFile.CreateFromDirectory(folderPath, save.FileName, CompressionLevel.Optimal, true);

                        MessageBox.Show("Paquete generado correctamente:\n" + save.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el paquete del manual del desarrollador: " + ex.Message);
            }
        }
    }
}

