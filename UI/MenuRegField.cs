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
    public partial class MenuRegField : Form
    {
        private Panel _panelContenedor;

        private readonly FieldSLService _fieldSLService;

        public MenuRegField(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate();

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

        private void btnFindField_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindPromotions(_panelContenedor));
        }


        private void btnRegField_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones de entrada
                if (string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtCapacity.Text) ||
                    string.IsNullOrWhiteSpace(txtHourlyCost.Text) ||
                    cmbFieldType.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtCapacity.Text.Trim(), out int capacity))
                {
                    MessageBox.Show("Capacidad inválida. Ingrese un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtHourlyCost.Text.Trim(), out decimal hourlyCost))
                {
                    MessageBox.Show("Costo por hora inválido. Ingrese un número decimal válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Field newField = new Field
                {
                    IdField = Guid.NewGuid(),
                    Name = txtName.Text.Trim(),
                    Capacity = capacity,
                    HourlyCost = hourlyCost,
                    FieldType = (int)Enum.Parse(typeof(FieldType), cmbFieldType.SelectedItem.ToString()),
                    IdFieldState = 1, //Estado habilitada
                    DVH = string.Empty // Si usás verificación, lo calculás después
                };

                _fieldSLService.Insert(newField);

                MessageBox.Show("“Carga de cancha realizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiamos los campos
                txtName.Clear();
                txtCapacity.Clear();
                txtHourlyCost.Clear();
                cmbFieldType.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
