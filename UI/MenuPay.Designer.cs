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
            this.dataGridViewPay = new System.Windows.Forms.DataGridView();
            this.btnModCustomer = new System.Windows.Forms.Button();
            this.btnRegCustomer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.dtpPayFrom = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNroDocument = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFindPay = new System.Windows.Forms.Button();
            this.cmbMethodPayment = new System.Windows.Forms.ComboBox();
            this.dtpPayTo = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPay)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewPay
            // 
            this.dataGridViewPay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPay.Location = new System.Drawing.Point(35, 352);
            this.dataGridViewPay.Name = "dataGridViewPay";
            this.dataGridViewPay.RowHeadersWidth = 51;
            this.dataGridViewPay.RowTemplate.Height = 24;
            this.dataGridViewPay.Size = new System.Drawing.Size(1110, 379);
            this.dataGridViewPay.TabIndex = 18;
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
            this.lblStatus.Size = new System.Drawing.Size(190, 20);
            this.lblStatus.TabIndex = 94;
            this.lblStatus.Text = "Listo para buscar pagos";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // dtpPayFrom
            // 
            this.dtpPayFrom.Location = new System.Drawing.Point(291, 261);
            this.dtpPayFrom.Name = "dtpPayFrom";
            this.dtpPayFrom.ShowCheckBox = true;
            this.dtpPayFrom.Size = new System.Drawing.Size(188, 22);
            this.dtpPayFrom.TabIndex = 92;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Location = new System.Drawing.Point(556, 212);
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
            this.label3.Location = new System.Drawing.Point(121, 267);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(189, 23);
            this.label3.TabIndex = 90;
            this.label3.Text = "Fecha Pago Desde";
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
            // btnFindPay
            // 
            this.btnFindPay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindPay.Location = new System.Drawing.Point(727, 261);
            this.btnFindPay.Name = "btnFindPay";
            this.btnFindPay.Size = new System.Drawing.Size(167, 44);
            this.btnFindPay.TabIndex = 87;
            this.btnFindPay.Text = "Consultar Pagos";
            this.btnFindPay.UseVisualStyleBackColor = true;
            this.btnFindPay.Click += new System.EventHandler(this.btnFindPay_Click);
            // 
            // cmbMethodPayment
            // 
            this.cmbMethodPayment.FormattingEnabled = true;
            this.cmbMethodPayment.Location = new System.Drawing.Point(727, 206);
            this.cmbMethodPayment.Name = "cmbMethodPayment";
            this.cmbMethodPayment.Size = new System.Drawing.Size(167, 24);
            this.cmbMethodPayment.TabIndex = 95;
            // 
            // dtpPayTo
            // 
            this.dtpPayTo.Location = new System.Drawing.Point(291, 311);
            this.dtpPayTo.Name = "dtpPayTo";
            this.dtpPayTo.ShowCheckBox = true;
            this.dtpPayTo.Size = new System.Drawing.Size(188, 22);
            this.dtpPayTo.TabIndex = 97;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label5.Location = new System.Drawing.Point(121, 317);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(184, 23);
            this.label5.TabIndex = 96;
            this.label5.Text = "Fecha Pago Hasta";
            // 
            // MenuPay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(62)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1177, 741);
            this.Controls.Add(this.dtpPayTo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbMethodPayment);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.dtpPayFrom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNroDocument);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnFindPay);
            this.Controls.Add(this.dataGridViewPay);
            this.Controls.Add(this.btnModCustomer);
            this.Controls.Add(this.btnRegCustomer);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuPay";
            this.Text = "MenuPay";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewPay;
        private System.Windows.Forms.Button btnModCustomer;
        private System.Windows.Forms.Button btnRegCustomer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DateTimePicker dtpPayFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNroDocument;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFindPay;
        private System.Windows.Forms.ComboBox cmbMethodPayment;
        private System.Windows.Forms.DateTimePicker dtpPayTo;
        private System.Windows.Forms.Label label5;
    }
}