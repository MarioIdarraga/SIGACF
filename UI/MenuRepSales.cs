using BLL.Service;
using DAL.Contracts;
using DAL.Factory;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SL;
using SL.BLL;
using SL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Helpers;

namespace UI
{
    public partial class MenuRepSales : Form
    {
        private Panel _panelContenedor;
        private readonly PaySLService _paySLService;
        private readonly PaymentMethodSLService _paymentMethodSLService;

        public MenuRepSales(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate();

            var payRepo = Factory.Current.GetPayRepository();
            var payBLL = new PayService(payRepo);

            _paymentMethodSLService = new PaymentMethodSLService();
            _paySLService = new PaySLService(payBLL, _paymentMethodSLService);
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

        private void btnRepBooking_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRepBooking(_panelContenedor));
        }

        private void btnRepCan_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRepCan(_panelContenedor));
        }

        /// <summary>
        /// Traduce los encabezados de las columnas del DataGridView
        /// utilizando el mismo sistema de idiomas del resto de la aplicación.
        /// Usa el Name de la columna como clave en los archivos de idioma.
        /// </summary>
        private void TranslateGridHeaders()
        {
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                // Saltar columnas técnicas
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

        private void btnGenRepSales_Click(object sender, EventArgs e)
        {
            DateTime? since = dtpDateSinceSales.Checked ? dtpDateSinceSales.Value : (DateTime?)null;
            DateTime? until = dtpDateUntilSales.Checked ? dtpDateUntilSales.Value : (DateTime?)null;

            try
            {
                var results = _paySLService
                .GetAll(null, since, until, null)   
                .ToList();
                var viewData = results.Select(p => new
                {
                    // Mostrar ID del pago
                    IdPago = p.IdPay,

                    // Número de documento del cliente
                    Documento = p.NroDocument,

                    // Descripción del método de pago
                    MetodoPago = p.PaymentMethodDescription,

                    // Monto pagado
                    Monto = p.Amount,

                    // Fecha del pago
                    Fecha = p.Date,

                    // Estado del pago (solo descripción)
                    Estado = p.StateDescription
                }).ToList();

                dataGridView1.DataSource = viewData;

                // Traducción de columnas:
                TranslateGridHeaders();
                OcultarColumnasTecnicas();


                lblStatus.Text = results.Any()
                    ? $"Se encontraron {results.Count()} pagos."
                    : "No se encontraron pagos con esos criterios.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al generar el reporte: " + ex.Message);
            }
        }
        private void OcultarColumnasTecnicas()
        {
            string[] cols = { "IdBooking", "MethodPay", "State" };

            foreach (var col in cols)
                if (dataGridView1.Columns.Contains(col))
                    dataGridView1.Columns[col].Visible = false;
        }

        /// <summary>
        /// Genera un PDF con las ventas listadas actualmente.
        /// </summary>
        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Archivos PDF (*.pdf)|*.pdf",
                    FileName = $"Reporte_Ventas_{DateTime.Now:yyyyMMdd}.pdf"
                };

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                using (var stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    var doc = new Document(PageSize.A4, 40, 40, 40, 40);
                    PdfWriter.GetInstance(doc, stream);
                    doc.Open();

                    var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                    var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                    var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                    var footerFont = FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 9);

                    // TÍTULO
                    doc.Add(new Paragraph("Reporte de Ventas – " + DateTime.Now.ToShortDateString(), titleFont));
                    doc.Add(new Paragraph(" "));

                    // TABLA PDF (solo columnas visibles)
                    var table = new PdfPTable(dataGridView1.Columns.Cast<DataGridViewColumn>().Count(c => c.Visible))
                    {
                        WidthPercentage = 100
                    };

                    // Encabezados
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        if (column.Visible)
                        {
                            table.AddCell(new PdfPCell(new Phrase(column.HeaderText, headerFont))
                            {
                                BackgroundColor = BaseColor.LIGHT_GRAY
                            });
                        }
                    }

                    // Filas
                    decimal totalVentas = 0;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (!cell.OwningColumn.Visible)
                                continue;

                            var value = cell.Value?.ToString() ?? "";
                            table.AddCell(new Phrase(value, normalFont));

                            // SUMA DEL CAMPO MONTO
                            if (cell.OwningColumn.HeaderText.ToLower().Contains("monto"))
                            {
                                var raw = value.Replace("$", "").Trim();

                                if (decimal.TryParse(raw, NumberStyles.Any, new CultureInfo("es-AR"), out decimal val))
                                    totalVentas += val;
                            }
                        }
                    }

                    doc.Add(table);
                    doc.Add(new Paragraph(" "));

                    // Totales
                    doc.Add(new Paragraph($"Total de registros: {dataGridView1.Rows.Count}", normalFont));
                    doc.Add(new Paragraph($"Total de ventas: ${totalVentas:N2}", normalFont));
                    doc.Add(new Paragraph(" "));

                    // Footer
                    doc.Add(new Paragraph("Generado automáticamente por el sistema SIGACF.", footerFont));

                    doc.Close();
                }

                MessageBox.Show("PDF generado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                Process.Start(saveFileDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el PDF: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
