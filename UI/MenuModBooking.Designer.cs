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
            this.btnFindBooking.Location = new System.Drawing.Point(57, 55);
            this.btnFindBooking.Name = "btnFindBooking";
            this.btnFindBooking.Size = new System.Drawing.Size(174, 86);
            this.btnFindBooking.TabIndex = 72;
            this.btnFindBooking.Text = "Busqueda de Reservas";
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
            this.btnRegPay.Click += new System.EventHandler(this.btnRegPay_Click);
            // 
            // btnCanBooking
            // 
            this.btnCanBooking.BackColor = System.Drawing.Color.Red;
            this.btnCanBooking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCanBooking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.txtNroDocument.Location = new System.Drawing.Point(239, 426);
            this.txtNroDocument.Name = "txtNroDocument";
            this.txtNroDocument.Size = new System.Drawing.Size(186, 22);
            this.txtNroDocument.TabIndex = 121;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label8.Location = new System.Drawing.Point(73, 430);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 18);
            this.label8.TabIndex = 120;
            this.label8.Text = "Nro de Documento";
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpEndTime.Location = new System.Drawing.Point(237, 633);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(188, 22);
            this.dtpEndTime.TabIndex = 119;
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpStartTime.Location = new System.Drawing.Point(239, 569);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(188, 22);
            this.dtpStartTime.TabIndex = 118;
            // 
            // dtpRegistrationBooking
            // 
            this.dtpRegistrationBooking.Location = new System.Drawing.Point(237, 501);
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
            this.label7.Enabled = false;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label7.Location = new System.Drawing.Point(527, 357);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 18);
            this.label7.TabIndex = 115;
            this.label7.Text = "Promoción";
            // 
            // cmbField
            // 
            this.cmbField.FormattingEnabled = true;
            this.cmbField.Location = new System.Drawing.Point(686, 294);
            this.cmbField.Name = "cmbField";
            this.cmbField.Size = new System.Drawing.Size(186, 24);
            this.cmbField.TabIndex = 114;
            // 
            // txtIdCustomer
            // 
            this.txtIdCustomer.Location = new System.Drawing.Point(237, 353);
            this.txtIdCustomer.Name = "txtIdCustomer";
            this.txtIdCustomer.Size = new System.Drawing.Size(186, 22);
            this.txtIdCustomer.TabIndex = 113;
            // 
            // btnModBooking
            // 
            this.btnModBooking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModBooking.Location = new System.Drawing.Point(530, 542);
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
            this.label2.Location = new System.Drawing.Point(522, 295);
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
            this.label6.Location = new System.Drawing.Point(73, 637);
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
            this.label5.Location = new System.Drawing.Point(73, 573);
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
            this.label4.Location = new System.Drawing.Point(73, 507);
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
            this.label3.Location = new System.Drawing.Point(71, 357);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 18);
            this.label3.TabIndex = 107;
            this.label3.Text = "Id Cliente";
            // 
            // txtIdBooking
            // 
            this.txtIdBooking.Location = new System.Drawing.Point(237, 291);
            this.txtIdBooking.Name = "txtIdBooking";
            this.txtIdBooking.Size = new System.Drawing.Size(186, 22);
            this.txtIdBooking.TabIndex = 123;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label9.Location = new System.Drawing.Point(71, 295);
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
            this.label10.Location = new System.Drawing.Point(522, 425);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 18);
            this.label10.TabIndex = 124;
            this.label10.Text = "Estado";
            // 
            // txtState
            // 
            this.txtState.Enabled = false;
            this.txtState.Location = new System.Drawing.Point(686, 426);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(186, 22);
            this.txtState.TabIndex = 125;
            // 
            // MenuModBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(62)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1142, 705);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuModBooking";
            this.Text = "MenuModBooking";
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
    }
}