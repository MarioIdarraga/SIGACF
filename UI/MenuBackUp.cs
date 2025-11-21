using BLL.BusinessException;
using SL;
using SL.Factory;
using SL.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Helpers;

namespace UI
{
    /// <summary>
    /// Formulario para la gestión de copias de seguridad.
    /// Permite realizar un backup completo de la base de datos
    /// y restaurar un backup existente desde un archivo .bak.
    /// </summary>
    public partial class MenuBackUp : Form
    {
        private Panel _panelContenedor;

        /// <summary>
        /// Constructor del formulario de backup.
        /// Recibe el panel contenedor donde se incrusta el formulario.
        /// </summary>
        /// <param name="panelContenedor">Panel principal del layout.</param>
        public MenuBackUp(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); // Localización de textos
        }

        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor,
        /// removiendo cualquier control previo.
        /// </summary>
        /// <param name="formchild">Formulario hijo a mostrar.</param>
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
        /// Habilita o deshabilita los controles de backup/restore
        /// y muestra el cursor de espera mientras se ejecuta la operación.
        /// </summary>
        /// <param name="enabled">
        /// true para habilitar la UI; false para deshabilitarla.
        /// </param>
        private void ToggleBackupUi(bool enabled)
        {
            btnBackUp.Enabled = enabled;
            btnRestore.Enabled = enabled;
            this.Cursor = enabled ? Cursors.Default : Cursors.WaitCursor;
        }

        /// <summary>
        /// Evento del botón "BackUp Full".
        /// Ejecuta la creación y verificación de un backup completo
        /// de la base de datos usando el servicio de la capa SL.
        /// </summary>
        private async void btnBackUp_Click(object sender, EventArgs e)
        {
            ToggleBackupUi(false);
            try
            {
                var svc = new BackupSLService(SLFactory.Current.GetBackupRepository());

                // Carpeta por defecto donde se guardarán los .bak
                var backupFolder = @"C:\SqlBackups";

                // 1) Crear backup: devuelve la ruta COMPLETA del .bak
                var bakPath = await svc.CrearBackupAsync(backupFolder, "ArenaBackup");

                // (Opcional) comprobar en código que exista
                if (!System.IO.File.Exists(bakPath))
                {
                    MessageBox.Show(
                        "El archivo de backup no se encuentra en disco:\n" + bakPath,
                        "Backup",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                // 2) Verificar ese MISMO archivo
                await svc.VerificarBackupAsync(bakPath);

                MessageBox.Show(
                    "Backup creado y verificado:\n" + bakPath,
                    "Backup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (BusinessException ex)
            {
                // Errores esperados de negocio (por ejemplo, permisos, rutas inválidas, etc.)
                MessageBox.Show(
                    ex.Message,
                    "Backup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // Errores inesperados: se loguean y se muestra mensaje genérico
                LoggerService.Log(
                    $"Error inesperado realizando backup: {ex}",
                    EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al realizar el backup. " +
                    "Intente nuevamente o contacte al administrador.",
                    "Backup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                ToggleBackupUi(true);
            }
        }

        /// <summary>
        /// Evento del botón "Restore".
        /// Permite seleccionar un archivo .bak y restaurar la base de datos
        /// desde dicho backup, reemplazando la base actual si se confirma.
        /// </summary>
        private async void btnRestore_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog
            {
                Filter = "Backups (*.bak)|*.bak",
                Title = "Seleccionar archivo .bak"
            })
            {
                if (ofd.ShowDialog() != DialogResult.OK) return;

                // Confirmación (REPLACE true sobre la DB actual)
                if (MessageBox.Show(
                        "Esto restaurará la base actual desde el backup seleccionado.\n¿Continuar?",
                        "Confirmar restore",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                ToggleBackupUi(false);
                try
                {
                    var svc = new BackupSLService(SLFactory.Current.GetBackupRepository());

                    string dataDir = null;   
                    string logDir = null;    

                    await svc.RestaurarAsync(ofd.FileName, replace: true, dataDir: dataDir, logDir: logDir);

                    MessageBox.Show(
                        "Restore completado. La aplicación se reiniciará.",
                        "Restore",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    Application.Restart(); // recomendable tras un restore
                }
                catch (BusinessException ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Restore",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    LoggerService.Log(
                        $"Error inesperado realizando restore: {ex}",
                        EventLevel.Critical);

                    MessageBox.Show(
                        "Ocurrió un error inesperado al realizar el restore. " +
                        "Intente nuevamente o contacte al administrador.",
                        "Restore",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                finally
                {
                    ToggleBackupUi(true);
                }
            }
        }
    }
}

