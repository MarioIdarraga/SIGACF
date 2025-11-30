namespace UI
{
    partial class MenuRegBooking
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnFindBooking = new System.Windows.Forms.Button();
            this.txtIdCustomer = new System.Windows.Forms.TextBox();
            this.btnRegBooking = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRegPay = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbField = new System.Windows.Forms.ComboBox();
            this.btnFindCustomer = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbPromotion = new System.Windows.Forms.ComboBox();
            this.dtpRegistrationBooking = new System.Windows.Forms.DateTimePicker();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.txtNroDocument = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtImporteBooking = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbHorariosDisponibles = new System.Windows.Forms.ComboBox();
            this.btnVerHorarios = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(432, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 32);
            this.label1.TabIndex = 94;
            this.label1.Text = "Registrar Reserva";
            // 
            // btnFindBooking
            // 
            this.btnFindBooking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnFindBooking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindBooking.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnFindBooking.Location = new System.Drawing.Point(59, 55);
            this.btnFindBooking.Name = "btnFindBooking";
            this.btnFindBooking.Size = new System.Drawing.Size(174, 86);
            this.btnFindBooking.TabIndex = 92;
            this.btnFindBooking.Text = "Búsqueda de Reservas";
            this.btnFindBooking.UseVisualStyleBackColor = false;
            this.btnFindBooking.Click += new System.EventHandler(this.btnFindBooking_Click);
            // 
            // txtIdCustomer
            // 
            this.txtIdCustomer.Location = new System.Drawing.Point(221, 260);
            this.txtIdCustomer.Name = "txtIdCustomer";
            this.txtIdCustomer.Size = new System.Drawing.Size(186, 22);
            this.txtIdCustomer.TabIndex = 85;
            // 
            // btnRegBooking
            // 
            this.btnRegBooking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegBooking.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRegBooking.Location = new System.Drawing.Point(509, 565);
            this.btnRegBooking.Name = "btnRegBooking";
            this.btnRegBooking.Size = new System.Drawing.Size(162, 42);
            this.btnRegBooking.TabIndex = 83;
            this.btnRegBooking.Text = "Registrar Reserva";
            this.btnRegBooking.UseVisualStyleBackColor = true;
            this.btnRegBooking.Click += new System.EventHandler(this.btnRegBooking_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label5.Location = new System.Drawing.Point(57, 480);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 18);
            this.label5.TabIndex = 78;
            this.label5.Text = "Hora Inicio";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Location = new System.Drawing.Point(57, 414);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 18);
            this.label4.TabIndex = 77;
            this.label4.Text = "Fecha Reserva";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Location = new System.Drawing.Point(55, 264);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 18);
            this.label3.TabIndex = 76;
            this.label3.Text = "Id Cliente";
            // 
            // btnRegPay
            // 
            this.btnRegPay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRegPay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegPay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegPay.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRegPay.Location = new System.Drawing.Point(239, 55);
            this.btnRegPay.Name = "btnRegPay";
            this.btnRegPay.Size = new System.Drawing.Size(171, 86);
            this.btnRegPay.TabIndex = 93;
            this.btnRegPay.Text = "Registrar Pago";
            this.btnRegPay.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label6.Location = new System.Drawing.Point(57, 544);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 18);
            this.label6.TabIndex = 79;
            this.label6.Text = "Hora Fin";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(506, 259);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 18);
            this.label2.TabIndex = 80;
            this.label2.Text = "Cancha";
            // 
            // cmbField
            // 
            this.cmbField.FormattingEnabled = true;
            this.cmbField.Location = new System.Drawing.Point(684, 258);
            this.cmbField.Name = "cmbField";
            this.cmbField.Size = new System.Drawing.Size(186, 24);
            this.cmbField.TabIndex = 97;
            // 
            // btnFindCustomer
            // 
            this.btnFindCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnFindCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindCustomer.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnFindCustomer.Location = new System.Drawing.Point(417, 54);
            this.btnFindCustomer.Name = "btnFindCustomer";
            this.btnFindCustomer.Size = new System.Drawing.Size(178, 87);
            this.btnFindCustomer.TabIndex = 99;
            this.btnFindCustomer.Text = "Búsqueda de Clientes";
            this.btnFindCustomer.UseVisualStyleBackColor = false;
            this.btnFindCustomer.Click += new System.EventHandler(this.btnFindCustomer_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label7.Location = new System.Drawing.Point(506, 321);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 18);
            this.label7.TabIndex = 100;
            this.label7.Text = "Promoción";
            // 
            // cmbPromotion
            // 
            this.cmbPromotion.FormattingEnabled = true;
            this.cmbPromotion.Location = new System.Drawing.Point(684, 320);
            this.cmbPromotion.Name = "cmbPromotion";
            this.cmbPromotion.Size = new System.Drawing.Size(186, 24);
            this.cmbPromotion.TabIndex = 101;
            // 
            // dtpRegistrationBooking
            // 
            this.dtpRegistrationBooking.Location = new System.Drawing.Point(221, 408);
            this.dtpRegistrationBooking.Name = "dtpRegistrationBooking";
            this.dtpRegistrationBooking.Size = new System.Drawing.Size(188, 22);
            this.dtpRegistrationBooking.TabIndex = 102;
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CustomFormat = "HH:mm";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(223, 476);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.ShowUpDown = true;
            this.dtpStartTime.Size = new System.Drawing.Size(188, 22);
            this.dtpStartTime.TabIndex = 103;
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CustomFormat = "HH:mm";
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(221, 540);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.ShowUpDown = true;
            this.dtpEndTime.Size = new System.Drawing.Size(188, 22);
            this.dtpEndTime.TabIndex = 104;
            // 
            // txtNroDocument
            // 
            this.txtNroDocument.Location = new System.Drawing.Point(223, 333);
            this.txtNroDocument.Name = "txtNroDocument";
            this.txtNroDocument.Size = new System.Drawing.Size(186, 22);
            this.txtNroDocument.TabIndex = 106;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label8.Location = new System.Drawing.Point(57, 337);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 18);
            this.label8.TabIndex = 105;
            this.label8.Text = "Nro de Documento";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label9.Location = new System.Drawing.Point(511, 479);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 18);
            this.label9.TabIndex = 107;
            this.label9.Text = "Importe";
            // 
            // txtImporteBooking
            // 
            this.txtImporteBooking.Enabled = false;
            this.txtImporteBooking.Location = new System.Drawing.Point(684, 478);
            this.txtImporteBooking.Name = "txtImporteBooking";
            this.txtImporteBooking.Size = new System.Drawing.Size(186, 22);
            this.txtImporteBooking.TabIndex = 108;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label10.Location = new System.Drawing.Point(506, 377);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(164, 18);
            this.label10.TabIndex = 109;
            this.label10.Text = "Horarios disponibles";
            // 
            // cmbHorariosDisponibles
            // 
            this.cmbHorariosDisponibles.FormattingEnabled = true;
            this.cmbHorariosDisponibles.Location = new System.Drawing.Point(684, 371);
            this.cmbHorariosDisponibles.Name = "cmbHorariosDisponibles";
            this.cmbHorariosDisponibles.Size = new System.Drawing.Size(121, 24);
            this.cmbHorariosDisponibles.TabIndex = 110;
            this.cmbHorariosDisponibles.SelectedIndexChanged += new System.EventHandler(this.cmbHorariosDisponibles_SelectedIndexChanged);
            // 
            // btnVerHorarios
            // 
            this.btnVerHorarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerHorarios.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnVerHorarios.Location = new System.Drawing.Point(703, 565);
            this.btnVerHorarios.Name = "btnVerHorarios";
            this.btnVerHorarios.Size = new System.Drawing.Size(225, 42);
            this.btnVerHorarios.TabIndex = 111;
            this.btnVerHorarios.Text = "Ver Horarios Disponibles";
            this.btnVerHorarios.UseVisualStyleBackColor = true;
            this.btnVerHorarios.Click += new System.EventHandler(this.btnVerHorarios_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(684, 425);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(59, 22);
            this.numericUpDown1.TabIndex = 112;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label11.Location = new System.Drawing.Point(510, 425);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 18);
            this.label11.TabIndex = 113;
            this.label11.Text = "Duración";
            // 
            // MenuRegBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(62)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1012, 666);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.btnVerHorarios);
            this.Controls.Add(this.cmbHorariosDisponibles);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtImporteBooking);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtNroDocument);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtpEndTime);
            this.Controls.Add(this.dtpStartTime);
            this.Controls.Add(this.dtpRegistrationBooking);
            this.Controls.Add(this.cmbPromotion);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnFindCustomer);
            this.Controls.Add(this.cmbField);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRegPay);
            this.Controls.Add(this.btnFindBooking);
            this.Controls.Add(this.txtIdCustomer);
            this.Controls.Add(this.btnRegBooking);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuRegBooking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "MenuRegBooking";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFindBooking;
        private System.Windows.Forms.TextBox txtIdCustomer;
        private System.Windows.Forms.Button btnRegBooking;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRegPay;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbField;
        private System.Windows.Forms.Button btnFindCustomer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbPromotion;
        private System.Windows.Forms.DateTimePicker dtpRegistrationBooking;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.TextBox txtNroDocument;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtImporteBooking;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbHorariosDisponibles;
        private System.Windows.Forms.Button btnVerHorarios;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label11;
    }
}