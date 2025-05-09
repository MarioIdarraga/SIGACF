﻿namespace UI
{
    partial class MenuRepBooking
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGenRepBookings = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnRepCan = new System.Windows.Forms.Button();
            this.btnRepSales = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.btnPrintBookings = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(408, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 18);
            this.label3.TabIndex = 41;
            this.label3.Text = "Fechas Hasta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(32, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 18);
            this.label2.TabIndex = 35;
            this.label2.Text = "Fecha Desde";
            // 
            // btnGenRepBookings
            // 
            this.btnGenRepBookings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenRepBookings.Location = new System.Drawing.Point(311, 286);
            this.btnGenRepBookings.Name = "btnGenRepBookings";
            this.btnGenRepBookings.Size = new System.Drawing.Size(164, 44);
            this.btnGenRepBookings.TabIndex = 34;
            this.btnGenRepBookings.Text = "Generar Reporte";
            this.btnGenRepBookings.UseVisualStyleBackColor = true;
            this.btnGenRepBookings.Click += new System.EventHandler(this.btnGenRepBooking_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(35, 352);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1110, 379);
            this.dataGridView1.TabIndex = 33;
            // 
            // btnRepCan
            // 
            this.btnRepCan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnRepCan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepCan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepCan.Location = new System.Drawing.Point(216, 9);
            this.btnRepCan.Name = "btnRepCan";
            this.btnRepCan.Size = new System.Drawing.Size(176, 85);
            this.btnRepCan.TabIndex = 32;
            this.btnRepCan.Text = "Reporte de Cancelaciones";
            this.btnRepCan.UseVisualStyleBackColor = false;
            this.btnRepCan.Click += new System.EventHandler(this.btnRepCan_Click);
            // 
            // btnRepSales
            // 
            this.btnRepSales.BackColor = System.Drawing.Color.Lime;
            this.btnRepSales.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepSales.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepSales.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRepSales.Location = new System.Drawing.Point(35, 9);
            this.btnRepSales.Name = "btnRepSales";
            this.btnRepSales.Size = new System.Drawing.Size(175, 85);
            this.btnRepSales.TabIndex = 31;
            this.btnRepSales.Text = "Reporte de Ventas";
            this.btnRepSales.UseVisualStyleBackColor = false;
            this.btnRepSales.Click += new System.EventHandler(this.btnRepSales_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(403, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(299, 32);
            this.label1.TabIndex = 30;
            this.label1.Text = "Reporte de Reservas";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(145, 220);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker1.TabIndex = 42;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(526, 220);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker2.TabIndex = 43;
            // 
            // btnPrintBookings
            // 
            this.btnPrintBookings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintBookings.Location = new System.Drawing.Point(599, 286);
            this.btnPrintBookings.Name = "btnPrintBookings";
            this.btnPrintBookings.Size = new System.Drawing.Size(164, 44);
            this.btnPrintBookings.TabIndex = 44;
            this.btnPrintBookings.Text = "Imprimir Reporte";
            this.btnPrintBookings.UseVisualStyleBackColor = true;
            // 
            // MenuRepBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(62)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1177, 741);
            this.Controls.Add(this.btnPrintBookings);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGenRepBookings);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnRepCan);
            this.Controls.Add(this.btnRepSales);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuRepBooking";
            this.Text = "MenuRepBooking";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGenRepBookings;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnRepCan;
        private System.Windows.Forms.Button btnRepSales;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Button btnPrintBookings;
    }
}