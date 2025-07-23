namespace UI
{
    partial class MenuSales
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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNroDocument = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFindBooking = new System.Windows.Forms.Button();
            this.dataGridViewBookings = new System.Windows.Forms.DataGridView();
            this.btnModBooking = new System.Windows.Forms.Button();
            this.btnRegBooking = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpRegistrationBooking = new System.Windows.Forms.DateTimePicker();
            this.dtpRegistrationDate = new System.Windows.Forms.DateTimePicker();
            this.btnRegPay = new System.Windows.Forms.Button();
            this.btnCanBooking = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBookings)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Location = new System.Drawing.Point(78, 318);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 18);
            this.label4.TabIndex = 27;
            this.label4.Text = "Fecha de Registro";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(504, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 18);
            this.label3.TabIndex = 26;
            this.label3.Text = "Fecha de Reserva";
            // 
            // txtNroDocument
            // 
            this.txtNroDocument.Location = new System.Drawing.Point(249, 239);
            this.txtNroDocument.Name = "txtNroDocument";
            this.txtNroDocument.Size = new System.Drawing.Size(170, 22);
            this.txtNroDocument.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(76, 245);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 18);
            this.label2.TabIndex = 20;
            this.label2.Text = "Nro de Documento";
            // 
            // btnFindBooking
            // 
            this.btnFindBooking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindBooking.Location = new System.Drawing.Point(685, 292);
            this.btnFindBooking.Name = "btnFindBooking";
            this.btnFindBooking.Size = new System.Drawing.Size(167, 44);
            this.btnFindBooking.TabIndex = 19;
            this.btnFindBooking.Text = "Consultar Reserva";
            this.btnFindBooking.UseVisualStyleBackColor = true;
            this.btnFindBooking.Click += new System.EventHandler(this.btnFindBooking_Click);
            // 
            // dataGridViewBookings
            // 
            this.dataGridViewBookings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBookings.Location = new System.Drawing.Point(71, 369);
            this.dataGridViewBookings.Name = "dataGridViewBookings";
            this.dataGridViewBookings.RowHeadersWidth = 51;
            this.dataGridViewBookings.RowTemplate.Height = 24;
            this.dataGridViewBookings.Size = new System.Drawing.Size(1110, 379);
            this.dataGridViewBookings.TabIndex = 18;
            // 
            // btnModBooking
            // 
            this.btnModBooking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnModBooking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModBooking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModBooking.Location = new System.Drawing.Point(248, 27);
            this.btnModBooking.Name = "btnModBooking";
            this.btnModBooking.Size = new System.Drawing.Size(176, 85);
            this.btnModBooking.TabIndex = 17;
            this.btnModBooking.Text = "Modificar Reserva";
            this.btnModBooking.UseVisualStyleBackColor = false;
            this.btnModBooking.Click += new System.EventHandler(this.btnModBooking_Click);
            // 
            // btnRegBooking
            // 
            this.btnRegBooking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRegBooking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegBooking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegBooking.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRegBooking.Location = new System.Drawing.Point(67, 27);
            this.btnRegBooking.Name = "btnRegBooking";
            this.btnRegBooking.Size = new System.Drawing.Size(175, 85);
            this.btnRegBooking.TabIndex = 16;
            this.btnRegBooking.Text = "Registrar Reserva";
            this.btnRegBooking.UseVisualStyleBackColor = false;
            this.btnRegBooking.Click += new System.EventHandler(this.btnRegBooking_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(439, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(328, 32);
            this.label1.TabIndex = 15;
            this.label1.Text = "Búsqueda de Reservas";
            // 
            // dtpRegistrationBooking
            // 
            this.dtpRegistrationBooking.Location = new System.Drawing.Point(674, 237);
            this.dtpRegistrationBooking.Name = "dtpRegistrationBooking";
            this.dtpRegistrationBooking.ShowCheckBox = true;
            this.dtpRegistrationBooking.Size = new System.Drawing.Size(188, 22);
            this.dtpRegistrationBooking.TabIndex = 31;
            // 
            // dtpRegistrationDate
            // 
            this.dtpRegistrationDate.Location = new System.Drawing.Point(241, 312);
            this.dtpRegistrationDate.Name = "dtpRegistrationDate";
            this.dtpRegistrationDate.ShowCheckBox = true;
            this.dtpRegistrationDate.Size = new System.Drawing.Size(200, 22);
            this.dtpRegistrationDate.TabIndex = 32;
            // 
            // btnRegPay
            // 
            this.btnRegPay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnRegPay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegPay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegPay.Location = new System.Drawing.Point(430, 27);
            this.btnRegPay.Name = "btnRegPay";
            this.btnRegPay.Size = new System.Drawing.Size(176, 85);
            this.btnRegPay.TabIndex = 33;
            this.btnRegPay.Text = "Registrar Pago";
            this.btnRegPay.UseVisualStyleBackColor = false;
            this.btnRegPay.Click += new System.EventHandler(this.btnRegPay_Click);
            // 
            // btnCanBooking
            // 
            this.btnCanBooking.BackColor = System.Drawing.Color.Red;
            this.btnCanBooking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCanBooking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCanBooking.Location = new System.Drawing.Point(612, 27);
            this.btnCanBooking.Name = "btnCanBooking";
            this.btnCanBooking.Size = new System.Drawing.Size(176, 85);
            this.btnCanBooking.TabIndex = 34;
            this.btnCanBooking.Text = "Cancelar Reserva";
            this.btnCanBooking.UseVisualStyleBackColor = false;
            this.btnCanBooking.Click += new System.EventHandler(this.btnCanBooking_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(652, 346);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(210, 20);
            this.lblStatus.TabIndex = 86;
            this.lblStatus.Text = "Listo para buscar reservas";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // MenuSales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(62)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1201, 767);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCanBooking);
            this.Controls.Add(this.btnRegPay);
            this.Controls.Add(this.dtpRegistrationDate);
            this.Controls.Add(this.dtpRegistrationBooking);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNroDocument);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnFindBooking);
            this.Controls.Add(this.dataGridViewBookings);
            this.Controls.Add(this.btnModBooking);
            this.Controls.Add(this.btnRegBooking);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuSales";
            this.Text = "MenuRent";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBookings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNroDocument;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFindBooking;
        private System.Windows.Forms.DataGridView dataGridViewBookings;
        private System.Windows.Forms.Button btnModBooking;
        private System.Windows.Forms.Button btnRegBooking;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpRegistrationBooking;
        private System.Windows.Forms.DateTimePicker dtpRegistrationDate;
        private System.Windows.Forms.Button btnRegPay;
        private System.Windows.Forms.Button btnCanBooking;
        private System.Windows.Forms.Label lblStatus;
    }
}