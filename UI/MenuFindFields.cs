using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Factory;
using Domain;
using SL;
using SL.Service.Extension;
using UI.Helpers;

namespace UI
{
    public partial class MenuFindFields : Form
    {
        private Panel _panelContenedor;

        private readonly FieldSLService _fieldSLService;
        public MenuFindFields(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); // Traductor

            var fieldRepo = Factory.Current.GetFieldRepository();
            var fieldService = new BLL.Service.FieldService(fieldRepo);
            _fieldSLService = new FieldSLService(fieldService);

            CargarTiposDeCancha();
        }

        private void CargarTiposDeCancha()
        {
            cmbFieldType.DataSource = Enum.GetValues(typeof(FieldType));
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

        private void btnMenuAdmin_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuAdmin(_panelContenedor));
        }

        private void btnRegField_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegField(_panelContenedor));
        }

        private void btnModField_Click(object sender, EventArgs e)
        {
            if (dataGridViewFields.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una cancha para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedField = (Field)dataGridViewFields.SelectedRows[0].DataBoundItem;

            OpenFormChild(new MenuModField(_panelContenedor, selectedField));
        }

        private void btnFindField_Click(object sender, EventArgs e)
        {
            int? fieldType = null;
            if (!string.IsNullOrWhiteSpace(cmbFieldType.Text) && int.TryParse(cmbFieldType.Text, out int tipo))
            {
                fieldType = tipo;
            }

            int? fieldState = null;
            if (cmbState.SelectedItem != null)
            {
                fieldState = int.Parse(cmbState.SelectedValue.ToString());
            }

            try
            {
                var fields = _fieldSLService.GetAll(null, null, fieldType, fieldState);
                dataGridViewFields.DataSource = fields.ToList();

                if (dataGridViewFields.Columns.Contains("DVH"))
                {
                    dataGridViewFields.Columns["DVH"].Visible = false;
                }

                lblStatus.Text = fields.Any()
                    ? $"Se encontraron {fields.Count()} canchas.": "No se encontraron canchas con esos criterios.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar canchas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
