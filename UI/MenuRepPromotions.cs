using BLL.Service;
using DAL.Contracts;
using DAL.Factory;
using Domain;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SL;
using SL.BLL;
using System;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UI.Helpers;

namespace UI
{
    /// <summary>
    /// Reporte de promociones más usadas en un rango de fechas.
    /// Muestra las promociones con cantidad de reservas y monto total,
    /// y permite exportar el resultado a PDF.
    /// </summary>
    public partial class MenuRepPromotions : Form
    {
        private readonly Panel _panelContenedor;
        private readonly BookingSLService _bookingSlService;
        private readonly IGenericRepository<Promotion> _promotionRepo;

        /// <summary>
        /// Constructor usado por el runtime (recibe el panel contenedor).
        /// </summary>
        /// <param name="panelContenedor">Panel principal donde se incrusta el formulario.</param>
        public MenuRepPromotions(Panel panelContenedor) : this()
        {
            _panelContenedor = panelContenedor;
            this.Translate();

            var bookingRepo = Factory.Current.GetBookingRepository();
            var bookingBll = new BookingService(bookingRepo);
            _bookingSlService = new BookingSLService(bookingBll);

            _promotionRepo = Factory.Current.GetPromotionRepository();
        }

        /// <summary>
        /// Constructor por defecto requerido por el diseñador.
        /// </summary>
        public MenuRepPromotions()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor.
        /// </summary>
        private void OpenFormChild(Form child)
        {
            if (_panelContenedor == null)
                return; // por si se abre desde el diseñador

            if (_panelContenedor.Controls.Count > 0)
                _panelContenedor.Controls.RemoveAt(0);

            child.TopLevel = false;
            child.Dock = DockStyle.Fill;
            _panelContenedor.Controls.Add(child);
            _panelContenedor.Tag = child;
            child.Show();
        }

        #region Navegación entre reportes

        private void btnRepSales_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRepSales(_panelContenedor));
        }

        private void btnRepCan_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRepCan(_panelContenedor));
        }

        private void btnRepBooking_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRepBooking(_panelContenedor));
        }

        #endregion

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

        /// <summary>
        /// Genera el reporte de promociones más usadas en el rango de fechas.
        /// Llena el DataGridView con: Promoción, VecesUsada, TotalReservado.
        /// </summary>
        private void btnGenRepPromotion_Click(object sender, EventArgs e)
        {
            DateTime? desde = dateTimePicker1.Value.Date;
            DateTime? hasta = dateTimePicker2.Value.Date;

            try
            {
                // Traemos reservas por rango
                var bookings = _bookingSlService
                    .GetBookingsByDateRange(desde, hasta)
                    .Where(b => b.Promotion != Guid.Empty)
                    .ToList();

                // Traemos promociones para obtener los nombres
                var promociones = _promotionRepo.GetAll().ToList();
                var promoLookup = promociones.ToDictionary(p => p.IdPromotion, p => p.Name);

                // Agrupamos por promoción
                var viewData = bookings
                    .GroupBy(b => b.Promotion)
                    .Select(g => new
                    {
                        Promocion = promoLookup.TryGetValue(g.Key, out var name)
                            ? name
                            : "(Promoción no encontrada)",
                        VecesUsada = g.Count(),
                        TotalReservado = g.Sum(b => b.ImporteBooking)
                    })
                    .OrderByDescending(x => x.VecesUsada)
                    .ToList();

                dataGridView1.DataSource = viewData;
                TranslateGridHeaders();

                MessageBox.Show(
                    viewData.Any()
                        ? $"Se encontraron {viewData.Count} promociones utilizadas."
                        : "No se encontraron promociones en ese rango.",
                    "Resultado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error al generar reporte de promociones: {ex}",
                    EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error al generar el reporte de promociones.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Exporta a PDF el contenido actual del DataGridView de promociones.
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
                    FileName = $"Reporte_Promociones_{DateTime.Now:yyyyMMdd}.pdf"
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

                    doc.Add(new Paragraph("Reporte de Promociones más usadas", titleFont));
                    doc.Add(new Paragraph(DateTime.Now.ToShortDateString(), normalFont));
                    doc.Add(new Paragraph(" "));

                    var visibleColumns = dataGridView1.Columns
                        .Cast<DataGridViewColumn>()
                        .Where(c => c.Visible)
                        .ToList();

                    var table = new PdfPTable(visibleColumns.Count)
                    {
                        WidthPercentage = 100
                    };

                    // Encabezados
                    foreach (var column in visibleColumns)
                    {
                        table.AddCell(new PdfPCell(new Phrase(column.HeaderText, headerFont))
                        {
                            BackgroundColor = BaseColor.LIGHT_GRAY
                        });
                    }

                    decimal totalGeneral = 0;

                    // Filas
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.IsNewRow) continue;

                        foreach (var column in visibleColumns)
                        {
                            var cell = row.Cells[column.Index];
                            var value = cell.Value?.ToString() ?? string.Empty;
                            table.AddCell(new Phrase(value, normalFont));

                            // Sumamos el total de la columna TotalReservado
                            if (column.HeaderText.ToLower().Contains("total") &&
                                decimal.TryParse(
                                    value.Replace("$", "").Replace(".", "").Replace(",", "."),
                                    out decimal val))
                            {
                                totalGeneral += val;
                            }
                        }
                    }

                    doc.Add(table);
                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph($"Total de promociones: {dataGridView1.Rows.Count}", normalFont));
                    doc.Add(new Paragraph($"Total general reservado: ${totalGeneral:N2}", normalFont));
                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph("Generado automáticamente por el sistema SIGACF.", footerFont));

                    doc.Close();
                }

                MessageBox.Show("PDF generado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                Process.Start(saveFileDialog.FileName);
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error al generar PDF de promociones: {ex}",
                    EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error al generar el PDF de promociones.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        
    }
}
