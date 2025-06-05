namespace UI
{
    partial class MenuRepSales
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
            this.dtpDateUntilSales = new System.Windows.Forms.DateTimePicker();
            this.dtpDateSinceSales = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGenRepSales = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnRepCan = new System.Windows.Forms.Button();
            this.btnRepBooking = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpDateUntilSales
            // 
            this.dtpDateUntilSales.Location = new System.Drawing.Point(528, 220);
            this.dtpDateUntilSales.Name = "dtpDateUntilSales";
            this.dtpDateUntilSales.Size = new System.Drawing.Size(200, 22);
            this.dtpDateUntilSales.TabIndex = 52;
            // 
            // dtpDateSinceSales
            // 
            this.dtpDateSinceSales.Location = new System.Drawing.Point(147, 220);
            this.dtpDateSinceSales.Name = "dtpDateSinceSales";
            this.dtpDateSinceSales.Size = new System.Drawing.Size(200, 22);
            this.dtpDateSinceSales.TabIndex = 51;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(410, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 18);
            this.label3.TabIndex = 50;
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
            this.label2.TabIndex = 49;
            this.label2.Text = "Fecha Desde";
            // 
            // btnGenRepSales
            // 
            this.btnGenRepSales.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenRepSales.Location = new System.Drawing.Point(461, 285);
            this.btnGenRepSales.Name = "btnGenRepSales";
            this.btnGenRepSales.Size = new System.Drawing.Size(164, 44);
            this.btnGenRepSales.TabIndex = 48;
            this.btnGenRepSales.Text = "Generar Reporte";
            this.btnGenRepSales.UseVisualStyleBackColor = true;
            this.btnGenRepSales.Click += new System.EventHandler(this.btnGenRepSales_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(37, 352);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1110, 379);
            this.dataGridView1.TabIndex = 47;
            // 
            // btnRepCan
            // 
            this.btnRepCan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnRepCan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepCan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepCan.Location = new System.Drawing.Point(218, 9);
            this.btnRepCan.Name = "btnRepCan";
            this.btnRepCan.Size = new System.Drawing.Size(176, 85);
            this.btnRepCan.TabIndex = 46;
            this.btnRepCan.Text = "Reporte de Cancelaciones";
            this.btnRepCan.UseVisualStyleBackColor = false;
            this.btnRepCan.Click += new System.EventHandler(this.btnRepCan_Click);
            // 
            // btnRepBooking
            // 
            this.btnRepBooking.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnRepBooking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepBooking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepBooking.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRepBooking.Location = new System.Drawing.Point(37, 9);
            this.btnRepBooking.Name = "btnRepBooking";
            this.btnRepBooking.Size = new System.Drawing.Size(175, 85);
            this.btnRepBooking.TabIndex = 45;
            this.btnRepBooking.Text = "Reporte de Reservas";
            this.btnRepBooking.UseVisualStyleBackColor = false;
            this.btnRepBooking.Click += new System.EventHandler(this.btnRepBooking_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(405, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(267, 32);
            this.label1.TabIndex = 44;
            this.label1.Text = "Reporte de Ventas";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(444, 332);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(194, 20);
            this.lblStatus.TabIndex = 87;
            this.lblStatus.Text = "Listo para buscar ventas";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // MenuRepSales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(62)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1180, 741);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.dtpDateUntilSales);
            this.Controls.Add(this.dtpDateSinceSales);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGenRepSales);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnRepCan);
            this.Controls.Add(this.btnRepBooking);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuRepSales";
            this.Text = "MenuRepSales";
            this.Load += new System.EventHandler(this.MenuRepSales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDateUntilSales;
        private System.Windows.Forms.DateTimePicker dtpDateSinceSales;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGenRepSales;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnRepCan;
        private System.Windows.Forms.Button btnRepBooking;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatus;
    }
}