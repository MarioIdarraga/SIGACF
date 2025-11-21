using BLL.Service;
using DAL.Factory;
using Domain;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UI.Helpers;

namespace UI
{
    /// <summary>
    /// Formulario de reporte de cancelaciones por rango de fechas.
    /// Permite visualizar reservas canceladas y generar un archivo PDF.
    /// </summary>
    public partial class MenuRepCan : Form
    {
        private readonly BookingSLService _bookingSLService;
        private readonly BookingStateSLService _bookingStateSLService;
        private readonly Panel _panelContenedor;

        /// <summary>
        /// Constructor que inicializa los componentes y servicios de la capa SL.
        /// </summary>
        /// <param name="panelContenedor">Panel donde se incrusta el formulario.</param>
        public MenuRepCan(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate();

            var repo = Factory.Current.GetBookingRepository();
            var bllService = new BookingService(repo);
            _bookingSLService = new BookingSLService(bllService);

            var stateRepo = Factory.Current.GetBookingStateRepository();
            var stateBll = new BookingStateService(stateRepo);
            _bookingStateSLService = new BookingStateSLService(stateBll);
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

        private void btnRepSales_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRepSales(_panelContenedor));
        }

        private void btnRepBooking_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRepBooking(_panelContenedor));
        }

        /// <summary>
        /// Busca cancelaciones por rango de fechas y las muestra en el DataGridView.
        /// </summary>
        private void btnGenRepCan_Click(object sender, EventArgs e)
        {
            DateTime? dateFrom = dtpDateSinceCan.Checked ? dtpDateSinceCan.Value.Date : (DateTime?)null;
            DateTime? dateTo = dtpDateUntilCan.Checked ? dtpDateUntilCan.Value.Date : (DateTime?)null;

            try
            {
                var bookings = _bookingSLService.GetCanceledBookingsByDateRange(dateFrom, dateTo).ToList();
                var statesLookup = _bookingStateSLService.GetStatesLookup();

                var viewData = bookings.Select(b => new
                {
                    b.NroDocument,
                    Fecha = b.RegistrationBooking.ToShortDateString(),
                    HoraInicio = new DateTime(b.StartTime.Ticks).ToString("hh\\:mm"),
                    HoraFin = new DateTime(b.EndTime.Ticks).ToString("hh\\:mm"),
                    Estado = statesLookup.TryGetValue(b.State, out var desc)
                        ? $"{b.State} - {desc}"
                        : b.State.ToString(),
                    Importe = $"${b.ImporteBooking:N2}"
                }).ToList();

                dataGridView1.DataSource = viewData;

                MessageBox.Show(
                    bookings.Any()
                        ? $"Se encontraron {bookings.Count} reservas canceladas."
                        : "No se encontraron cancelaciones en ese rango.",
                    "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al generar reporte de cancelaciones: {ex}", EventLevel.Critical);
                MessageBox.Show("Ocurrió un error al buscar cancelaciones.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Genera un PDF con las cancelaciones listadas actualmente.
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
                    FileName = $"Reporte_Cancelaciones_{DateTime.Now:yyyyMMdd}.pdf"
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

                    doc.Add(new Paragraph("Reporte de Cancelaciones – " + DateTime.Now.ToShortDateString(), titleFont));
                    doc.Add(new Paragraph(" "));

                    var table = new PdfPTable(dataGridView1.Columns.Cast<DataGridViewColumn>().Count(c => c.Visible))
                    {
                        WidthPercentage = 100
                    };

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

                    decimal total = 0;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (!cell.Visible || cell.OwningColumn.Visible == false)
                                continue;

                            var value = cell.Value?.ToString() ?? "";
                            table.AddCell(new Phrase(value, normalFont));

                            if (cell.OwningColumn.HeaderText.ToLower().Contains("importe") &&
                                decimal.TryParse(value.Replace("$", "").Replace(",", "."), out decimal val))
                                total += val;
                        }
                    }

                    doc.Add(table);
                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph($"Total de registros: {dataGridView1.Rows.Count}", normalFont));
                    doc.Add(new Paragraph($"Total general: ${total:N2}", normalFont));
                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph("Generado automáticamente por el sistema SIGACF.", footerFont));

                    doc.Close();
                }

                MessageBox.Show("PDF generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(saveFileDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
