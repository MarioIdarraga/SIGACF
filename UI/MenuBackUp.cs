using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SL.Factory;
using SL.Service;
using UI.Helpers;

namespace UI
{
    public partial class MenuBackUp : Form
    {

        private Panel _panelContenedor;
        public MenuBackUp(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); // Assuming you have a Translate method for localization
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

        private void ToggleBackupUi(bool enabled)
        {
            btnBackUp.Enabled = enabled;
            btnRestore.Enabled = enabled;
            this.Cursor = enabled ? Cursors.Default : Cursors.WaitCursor;
        }

        private async void btnBackUp_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog { SelectedPath = @"C:\Arena\Backups" })
            {
                if (fbd.ShowDialog() != DialogResult.OK) return;

                ToggleBackupUi(false);
                try
                {
                    var svc = new BackupSLService(SLFactory.Current.GetBackupRepository());

                    // nombre base del archivo .bak (se le agrega timestamp adentro del repo)
                    var bakPath = await svc.CrearBackupAsync(fbd.SelectedPath, "ArenaBackup");
                    await svc.VerificarBackupAsync(bakPath);

                    MessageBox.Show("Backup creado y verificado:\n" + bakPath, "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error realizando backup:\n" + ex.Message, "Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    ToggleBackupUi(true);
                }
            }
        }

        private async void btnRestore_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog { Filter = "Backups (*.bak)|*.bak", Title = "Seleccionar archivo .bak" })
            {
                if (ofd.ShowDialog() != DialogResult.OK) return;

                // Confirmación (REPLACE true sobre la DB actual)
                if (MessageBox.Show("Esto restaurará la base actual desde el backup seleccionado.\n¿Continuar?",
                                    "Confirmar restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                ToggleBackupUi(false);
                try
                {
                    var svc = new BackupSLService(SLFactory.Current.GetBackupRepository());

                    // Si el .bak viene de otra máquina y las rutas físicas no existen,
                    // pasá dataDir/logDir (carpetas de datos de tu SQL) para hacer WITH MOVE:
                    string dataDir = null;   // p.ej. @"C:\SQLData"
                    string logDir = null;   // p.ej. @"C:\SQLLogs"

                    await svc.RestaurarAsync(ofd.FileName, replace: true, dataDir: dataDir, logDir: logDir);

                    MessageBox.Show("Restore completado. La aplicación se reiniciará.", "Restore", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Restart(); // opcional pero recomendable tras un restore
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error realizando restore:\n" + ex.Message, "Restore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    ToggleBackupUi(true);
                }
            }
        }
    }
}
