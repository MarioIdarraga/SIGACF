using SL.BLL;
using SL.Composite;
using SL.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Helpers;

namespace UI
{
    public partial class MenuFindPatents : Form
    {

        private Panel _panelContenedor;

        private readonly PermissionSLService _permissionSLService;

        public MenuFindPatents(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); 

            _permissionSLService = new PermissionSLService();

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

        private void btnMenuUsers_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindUsers(_panelContenedor));
        }
        private void btnRegPatent_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegPatent(_panelContenedor));
        }

        private void btnModPatent_Click(object sender, EventArgs e)
        {
            if (dataGridViewPatents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una patente para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedPatent = (Patente)dataGridViewPatents.SelectedRows[0].DataBoundItem;

            OpenFormChild(new MenuModPatent(_panelContenedor, selectedPatent));
        }


        /// <summary>
        /// Traduce los encabezados de las columnas del DataGridView
        /// utilizando el mismo sistema de idiomas del resto de la aplicación.
        /// Usa el Name de la columna como clave en los archivos de idioma.
        /// </summary>
        private void TranslateGridHeaders()
        {
            foreach (DataGridViewColumn col in dataGridViewPatents.Columns)
            {
                
                if (col.Name == "UserId")
                    continue;

                try
                {
                    col.HeaderText = LanguageBLL.Current.Traductor(col.Name);
                }
                catch
                {
                    // Si no se encuentra la traducción, se deja el HeaderText tal cual.
                }
            }
        }
        /// <summary>
        /// Evento que ejecuta la búsqueda de patentes y carga los resultados en el DataGridView.
        /// Aplica ocultamiento de columnas técnicas y traducción de encabezados.
        /// </summary>
        private void btnFindPatent_Click(object sender, EventArgs e)
        {
            try
            {
                var patents = _permissionSLService.GetAllPermissionComponents()
                                                  .OfType<Patente>()
                                                  .ToList();

                dataGridViewPatents.DataSource = patents;

                HideTechnicalColumns();
                TranslateGridHeaders();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar las patentes: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Oculta columnas técnicas que no deben mostrarse al usuario final
        /// dentro del DataGridView de patentes.
        /// </summary>
        private void HideTechnicalColumns()
        {
            try
            {
                if (dataGridViewPatents.Columns.Contains("IdComponent"))
                    dataGridViewPatents.Columns["IdComponent"].Visible = false;

                if (dataGridViewPatents.Columns.Contains("ComponentType"))
                    dataGridViewPatents.Columns["ComponentType"].Visible = false;

                if (dataGridViewPatents.Columns.Contains("DVH"))
                    dataGridViewPatents.Columns["DVH"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ocultar columnas: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
