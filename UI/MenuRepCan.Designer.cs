namespace UI
{
    partial class MenuRepCan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtpDateUntilCan = new System.Windows.Forms.DateTimePicker();
            this.dtpDateSinceCan = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGenRepCan = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnRepBooking = new System.Windows.Forms.Button();
            this.btnRepSales = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExportPdf = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpDateUntilCan
            // 
            this.dtpDateUntilCan.Location = new System.Drawing.Point(528, 220);
            this.dtpDateUntilCan.Name = "dtpDateUntilCan";
            this.dtpDateUntilCan.Size = new System.Drawing.Size(200, 22);
            this.dtpDateUntilCan.TabIndex = 61;
            // 
            // dtpDateSinceCan
            // 
            this.dtpDateSinceCan.Location = new System.Drawing.Point(147, 220);
            this.dtpDateSinceCan.Name = "dtpDateSinceCan";
            this.dtpDateSinceCan.Size = new System.Drawing.Size(200, 22);
            this.dtpDateSinceCan.TabIndex = 60;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(410, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 18);
            this.label3.TabIndex = 59;
            this.label3.Text = "Fechas Hasta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(34, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 18);
            this.label2.TabIndex = 58;
            this.label2.Text = "Fecha Desde";
            // 
            // btnGenRepCan
            // 
            this.btnGenRepCan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenRepCan.Location = new System.Drawing.Point(370, 286);
            this.btnGenRepCan.Name = "btnGenRepCan";
            this.btnGenRepCan.Size = new System.Drawing.Size(164, 44);
            this.btnGenRepCan.TabIndex = 57;
            this.btnGenRepCan.Text = "Generar Reporte";
            this.btnGenRepCan.UseVisualStyleBackColor = true;
            this.btnGenRepCan.Click += new System.EventHandler(this.btnGenRepCan_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(37, 352);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1110, 379);
            this.dataGridView1.TabIndex = 56;
            // 
            // btnRepBooking
            // 
            this.btnRepBooking.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnRepBooking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepBooking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepBooking.Location = new System.Drawing.Point(218, 9);
            this.btnRepBooking.Name = "btnRepBooking";
            this.btnRepBooking.Size = new System.Drawing.Size(176, 85);
            this.btnRepBooking.TabIndex = 55;
            this.btnRepBooking.Text = "Reporte de Reservas";
            this.btnRepBooking.UseVisualStyleBackColor = false;
            this.btnRepBooking.Click += new System.EventHandler(this.btnRepBooking_Click);
            // 
            // btnRepSales
            // 
            this.btnRepSales.BackColor = System.Drawing.Color.Lime;
            this.btnRepSales.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepSales.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepSales.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRepSales.Location = new System.Drawing.Point(37, 9);
            this.btnRepSales.Name = "btnRepSales";
            this.btnRepSales.Size = new System.Drawing.Size(175, 85);
            this.btnRepSales.TabIndex = 54;
            this.btnRepSales.Text = "Reporte de Ventas";
            this.btnRepSales.UseVisualStyleBackColor = false;
            this.btnRepSales.Click += new System.EventHandler(this.btnRepSales_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(405, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(373, 32);
            this.label1.TabIndex = 53;
            this.label1.Text = "Reporte de Cancelaciones";
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportPdf.Location = new System.Drawing.Point(564, 286);
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.Size = new System.Drawing.Size(164, 44);
            this.btnExportPdf.TabIndex = 62;
            this.btnExportPdf.Text = "Generar PDF";
            this.btnExportPdf.UseVisualStyleBackColor = true;
            this.btnExportPdf.Click += new System.EventHandler(this.btnExportPdf_Click);
            // 
            // MenuRepCan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(62)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1180, 741);
            this.Controls.Add(this.btnExportPdf);
            this.Controls.Add(this.dtpDateUntilCan);
            this.Controls.Add(this.dtpDateSinceCan);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGenRepCan);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnRepBooking);
            this.Controls.Add(this.btnRepSales);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuRepCan";
            this.Text = "MenuRepCan";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDateUntilCan;
        private System.Windows.Forms.DateTimePicker dtpDateSinceCan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGenRepCan;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnRepBooking;
        private System.Windows.Forms.Button btnRepSales;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExportPdf;
    }
}