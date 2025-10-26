namespace UI
{
    partial class MenuPay
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnModCustomer = new System.Windows.Forms.Button();
            this.btnRegCustomer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.dtpRegistrationBooking = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNroDocument = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFindBooking = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(35, 352);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1110, 379);
            this.dataGridView1.TabIndex = 18;
            // 
            // btnModCustomer
            // 
            this.btnModCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnModCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModCustomer.Location = new System.Drawing.Point(216, 9);
            this.btnModCustomer.Name = "btnModCustomer";
            this.btnModCustomer.Size = new System.Drawing.Size(176, 85);
            this.btnModCustomer.TabIndex = 17;
            this.btnModCustomer.Text = "Modificar Cliente";
            this.btnModCustomer.UseVisualStyleBackColor = false;
            // 
            // btnRegCustomer
            // 
            this.btnRegCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRegCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegCustomer.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRegCustomer.Location = new System.Drawing.Point(35, 9);
            this.btnRegCustomer.Name = "btnRegCustomer";
            this.btnRegCustomer.Size = new System.Drawing.Size(175, 85);
            this.btnRegCustomer.TabIndex = 16;
            this.btnRegCustomer.Text = "Registrar Cliente";
            this.btnRegCustomer.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(403, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 32);
            this.label1.TabIndex = 15;
            this.label1.Text = "Búsqueda de Pagos";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(694, 315);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(238, 25);
            this.lblStatus.TabIndex = 94;
            this.lblStatus.Text = "Listo para buscar pagos";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // dtpRegistrationBooking
            // 
            this.dtpRegistrationBooking.Location = new System.Drawing.Point(716, 206);
            this.dtpRegistrationBooking.Name = "dtpRegistrationBooking";
            this.dtpRegistrationBooking.ShowCheckBox = true;
            this.dtpRegistrationBooking.Size = new System.Drawing.Size(188, 22);
            this.dtpRegistrationBooking.TabIndex = 92;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Location = new System.Drawing.Point(120, 287);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 18);
            this.label4.TabIndex = 91;
            this.label4.Text = "Metodo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(546, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 18);
            this.label3.TabIndex = 90;
            this.label3.Text = "Fecha de Pago";
            // 
            // txtNroDocument
            // 
            this.txtNroDocument.Location = new System.Drawing.Point(291, 208);
            this.txtNroDocument.Name = "txtNroDocument";
            this.txtNroDocument.Size = new System.Drawing.Size(170, 22);
            this.txtNroDocument.TabIndex = 89;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(118, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 18);
            this.label2.TabIndex = 88;
            this.label2.Text = "Nro de Documento";
            // 
            // btnFindBooking
            // 
            this.btnFindBooking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindBooking.Location = new System.Drawing.Point(727, 261);
            this.btnFindBooking.Name = "btnFindBooking";
            this.btnFindBooking.Size = new System.Drawing.Size(167, 44);
            this.btnFindBooking.TabIndex = 87;
            this.btnFindBooking.Text = "Consultar Reserva";
            this.btnFindBooking.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(291, 281);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(170, 24);
            this.comboBox1.TabIndex = 95;
            // 
            // MenuPay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(62)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1177, 741);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.dtpRegistrationBooking);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNroDocument);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnFindBooking);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnModCustomer);
            this.Controls.Add(this.btnRegCustomer);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuPay";
            this.Text = "MenuPay";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnModCustomer;
        private System.Windows.Forms.Button btnRegCustomer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DateTimePicker dtpRegistrationBooking;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNroDocument;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFindBooking;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}