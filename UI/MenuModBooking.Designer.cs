namespace UI
{
    partial class MenuModBooking
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
            this.btnRegPay = new System.Windows.Forms.Button();
            this.btnCanBooking = new System.Windows.Forms.Button();
            this.txtNroDocument = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.dtpRegistrationBooking = new System.Windows.Forms.DateTimePicker();
            this.cmbPromotion = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbField = new System.Windows.Forms.ComboBox();
            this.txtIdCustomer = new System.Windows.Forms.TextBox();
            this.btnModBooking = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIdBooking = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtState = new System.Windows.Forms.TextBox();
            this.txtImporteBooking = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btnVerHorarios = new System.Windows.Forms.Button();
            this.cmbHorariosDisponibles = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(430, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 32);
            this.label1.TabIndex = 74;
            this.label1.Text = "Modificar Reserva";
            // 
            // btnFindBooking
            // 
            this.btnFindBooking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnFindBooking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindBooking.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnFindBooking.Location = new System.Drawing.Point(57, 55);
            this.btnFindBooking.Name = "btnFindBooking";
            this.btnFindBooking.Size = new System.Drawing.Size(174, 86);
            this.btnFindBooking.TabIndex = 72;
            this.btnFindBooking.Text = "Búsqueda de Reservas";
            this.btnFindBooking.UseVisualStyleBackColor = false;
            this.btnFindBooking.Click += new System.EventHandler(this.btnFindBooking_Click);
            // 
            // btnRegPay
            // 
            this.btnRegPay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRegPay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegPay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegPay.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRegPay.Location = new System.Drawing.Point(237, 55);
            this.btnRegPay.Name = "btnRegPay";
            this.btnRegPay.Size = new System.Drawing.Size(171, 86);
            this.btnRegPay.TabIndex = 73;
            this.btnRegPay.Text = "Registrar Pago";
            this.btnRegPay.UseVisualStyleBackColor = false;
            // 
            // btnCanBooking
            // 
            this.btnCanBooking.BackColor = System.Drawing.Color.Red;
            this.btnCanBooking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCanBooking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCanBooking.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnCanBooking.Location = new System.Drawing.Point(414, 55);
            this.btnCanBooking.Name = "btnCanBooking";
            this.btnCanBooking.Size = new System.Drawing.Size(176, 85);
            this.btnCanBooking.TabIndex = 75;
            this.btnCanBooking.Text = "Cancelar Reserva";
            this.btnCanBooking.UseVisualStyleBackColor = false;
            this.btnCanBooking.Click += new System.EventHandler(this.btnCanBooking_Click);
            // 
            // txtNroDocument
            // 
            this.txtNroDocument.Enabled = false;
            this.txtNroDocument.Location = new System.Drawing.Point(254, 285);
            this.txtNroDocument.Name = "txtNroDocument";
            this.txtNroDocument.ReadOnly = true;
            this.txtNroDocument.Size = new System.Drawing.Size(186, 22);
            this.txtNroDocument.TabIndex = 121;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label8.Location = new System.Drawing.Point(88, 289);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 18);
            this.label8.TabIndex = 120;
            this.label8.Text = "Nro de Documento";
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpEndTime.Location = new System.Drawing.Point(252, 492);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(188, 22);
            this.dtpEndTime.TabIndex = 119;
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpStartTime.Location = new System.Drawing.Point(254, 428);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(188, 22);
            this.dtpStartTime.TabIndex = 118;
            // 
            // dtpRegistrationBooking
            // 
            this.dtpRegistrationBooking.Location = new System.Drawing.Point(252, 358);
            this.dtpRegistrationBooking.Name = "dtpRegistrationBooking";
            this.dtpRegistrationBooking.Size = new System.Drawing.Size(188, 22);
            this.dtpRegistrationBooking.TabIndex = 117;
            // 
            // cmbPromotion
            // 
            this.cmbPromotion.FormattingEnabled = true;
            this.cmbPromotion.Location = new System.Drawing.Point(686, 356);
            this.cmbPromotion.Name = "cmbPromotion";
            this.cmbPromotion.Size = new System.Drawing.Size(186, 24);
            this.cmbPromotion.TabIndex = 116;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label7.Location = new System.Drawing.Point(508, 356);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 18);
            this.label7.TabIndex = 115;
            this.label7.Text = "Promoción";
            // 
            // cmbField
            // 
            this.cmbField.FormattingEnabled = true;
            this.cmbField.Location = new System.Drawing.Point(686, 285);
            this.cmbField.Name = "cmbField";
            this.cmbField.Size = new System.Drawing.Size(186, 24);
            this.cmbField.TabIndex = 114;
            // 
            // txtIdCustomer
            // 
            this.txtIdCustomer.Location = new System.Drawing.Point(256, 618);
            this.txtIdCustomer.Name = "txtIdCustomer";
            this.txtIdCustomer.Size = new System.Drawing.Size(186, 22);
            this.txtIdCustomer.TabIndex = 113;
            // 
            // btnModBooking
            // 
            this.btnModBooking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModBooking.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnModBooking.Location = new System.Drawing.Point(530, 610);
            this.btnModBooking.Name = "btnModBooking";
            this.btnModBooking.Size = new System.Drawing.Size(162, 42);
            this.btnModBooking.TabIndex = 112;
            this.btnModBooking.Text = "Modificar Reserva";
            this.btnModBooking.UseVisualStyleBackColor = true;
            this.btnModBooking.Click += new System.EventHandler(this.btnModBooking_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(508, 291);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 18);
            this.label2.TabIndex = 111;
            this.label2.Text = "Cancha";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label6.Location = new System.Drawing.Point(88, 496);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 18);
            this.label6.TabIndex = 110;
            this.label6.Text = "Hora Fin";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label5.Location = new System.Drawing.Point(88, 432);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 18);
            this.label5.TabIndex = 109;
            this.label5.Text = "Hora Inicio";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Location = new System.Drawing.Point(88, 366);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 18);
            this.label4.TabIndex = 108;
            this.label4.Text = "Fecha Reserva";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Location = new System.Drawing.Point(94, 618);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 18);
            this.label3.TabIndex = 107;
            this.label3.Text = "Id Cliente";
            // 
            // txtIdBooking
            // 
            this.txtIdBooking.Enabled = false;
            this.txtIdBooking.Location = new System.Drawing.Point(256, 556);
            this.txtIdBooking.Name = "txtIdBooking";
            this.txtIdBooking.Size = new System.Drawing.Size(186, 22);
            this.txtIdBooking.TabIndex = 123;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label9.Location = new System.Drawing.Point(94, 556);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 18);
            this.label9.TabIndex = 122;
            this.label9.Text = "Id Reserva";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label10.Location = new System.Drawing.Point(514, 498);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 18);
            this.label10.TabIndex = 124;
            this.label10.Text = "Estado";
            // 
            // txtState
            // 
            this.txtState.Enabled = false;
            this.txtState.Location = new System.Drawing.Point(686, 501);
            this.txtState.Name = "txtState";
            this.txtState.ReadOnly = true;
            this.txtState.Size = new System.Drawing.Size(186, 22);
            this.txtState.TabIndex = 125;
            // 
            // txtImporteBooking
            // 
            this.txtImporteBooking.Location = new System.Drawing.Point(686, 555);
            this.txtImporteBooking.Name = "txtImporteBooking";
            this.txtImporteBooking.ReadOnly = true;
            this.txtImporteBooking.Size = new System.Drawing.Size(186, 22);
            this.txtImporteBooking.TabIndex = 127;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label11.Location = new System.Drawing.Point(514, 556);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 18);
            this.label11.TabIndex = 126;
            this.label11.Text = "Importe";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label12.Location = new System.Drawing.Point(514, 453);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 18);
            this.label12.TabIndex = 132;
            this.label12.Text = "Duración";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(686, 453);
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
            this.numericUpDown1.TabIndex = 131;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnVerHorarios
            // 
            this.btnVerHorarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerHorarios.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnVerHorarios.Location = new System.Drawing.Point(878, 399);
            this.btnVerHorarios.Name = "btnVerHorarios";
            this.btnVerHorarios.Size = new System.Drawing.Size(134, 51);
            this.btnVerHorarios.TabIndex = 130;
            this.btnVerHorarios.Text = "Ver Horarios Disponibles";
            this.btnVerHorarios.UseVisualStyleBackColor = true;
            this.btnVerHorarios.Click += new System.EventHandler(this.btnVerHorarios_Click);
            // 
            // cmbHorariosDisponibles
            // 
            this.cmbHorariosDisponibles.FormattingEnabled = true;
            this.cmbHorariosDisponibles.Location = new System.Drawing.Point(686, 399);
            this.cmbHorariosDisponibles.Name = "cmbHorariosDisponibles";
            this.cmbHorariosDisponibles.Size = new System.Drawing.Size(121, 24);
            this.cmbHorariosDisponibles.TabIndex = 129;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label13.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label13.Location = new System.Drawing.Point(508, 405);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(164, 18);
            this.label13.TabIndex = 128;
            this.label13.Text = "Horarios disponibles";
            // 
            // MenuModBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(62)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1142, 705);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.btnVerHorarios);
            this.Controls.Add(this.cmbHorariosDisponibles);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtImporteBooking);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtState);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtIdBooking);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtNroDocument);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtpEndTime);
            this.Controls.Add(this.dtpStartTime);
            this.Controls.Add(this.dtpRegistrationBooking);
            this.Controls.Add(this.cmbPromotion);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbField);
            this.Controls.Add(this.txtIdCustomer);
            this.Controls.Add(this.btnModBooking);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCanBooking);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRegPay);
            this.Controls.Add(this.btnFindBooking);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuModBooking";
            this.Text = "MenuModBooking";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFindBooking;
        private System.Windows.Forms.Button btnRegPay;
        private System.Windows.Forms.Button btnCanBooking;
        private System.Windows.Forms.TextBox txtNroDocument;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.DateTimePicker dtpRegistrationBooking;
        private System.Windows.Forms.ComboBox cmbPromotion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbField;
        private System.Windows.Forms.TextBox txtIdCustomer;
        private System.Windows.Forms.Button btnModBooking;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIdBooking;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.TextBox txtImporteBooking;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button btnVerHorarios;
        private System.Windows.Forms.ComboBox cmbHorariosDisponibles;
        private System.Windows.Forms.Label label13;
    }
}