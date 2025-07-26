namespace UI
{
    partial class MenuRegField
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
            this.label10 = new System.Windows.Forms.Label();
            this.txtHourlyCost = new System.Windows.Forms.TextBox();
            this.txtCapacity = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnRegField = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btnFindCustomer = new System.Windows.Forms.Button();
            this.cmbFieldType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label10.Location = new System.Drawing.Point(426, 183);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(218, 32);
            this.label10.TabIndex = 74;
            this.label10.Text = "Cargar Cancha";
            // 
            // txtHourlyCost
            // 
            this.txtHourlyCost.Location = new System.Drawing.Point(215, 397);
            this.txtHourlyCost.Name = "txtHourlyCost";
            this.txtHourlyCost.Size = new System.Drawing.Size(186, 22);
            this.txtHourlyCost.TabIndex = 70;
            // 
            // txtCapacity
            // 
            this.txtCapacity.Location = new System.Drawing.Point(215, 331);
            this.txtCapacity.Name = "txtCapacity";
            this.txtCapacity.Size = new System.Drawing.Size(186, 22);
            this.txtCapacity.TabIndex = 69;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(215, 262);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(186, 22);
            this.txtName.TabIndex = 67;
            // 
            // btnRegField
            // 
            this.btnRegField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegField.Location = new System.Drawing.Point(508, 504);
            this.btnRegField.Name = "btnRegField";
            this.btnRegField.Size = new System.Drawing.Size(162, 42);
            this.btnRegField.TabIndex = 66;
            this.btnRegField.Text = "Cargar Cancha";
            this.btnRegField.UseVisualStyleBackColor = true;
            this.btnRegField.Click += new System.EventHandler(this.btnRegField_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label14.Location = new System.Drawing.Point(49, 468);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(129, 18);
            this.label14.TabIndex = 63;
            this.label14.Text = "Tipo De Cancha";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label15.Location = new System.Drawing.Point(49, 401);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(96, 18);
            this.label15.TabIndex = 62;
            this.label15.Text = "Costo Hora";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label16.Location = new System.Drawing.Point(49, 335);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(87, 18);
            this.label16.TabIndex = 61;
            this.label16.Text = "Capacidad";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label18.Location = new System.Drawing.Point(49, 266);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(68, 18);
            this.label18.TabIndex = 59;
            this.label18.Text = "Nombre";
            // 
            // btnFindCustomer
            // 
            this.btnFindCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnFindCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindCustomer.Location = new System.Drawing.Point(54, 59);
            this.btnFindCustomer.Name = "btnFindCustomer";
            this.btnFindCustomer.Size = new System.Drawing.Size(178, 87);
            this.btnFindCustomer.TabIndex = 58;
            this.btnFindCustomer.Text = "Búsqueda de Canchas";
            this.btnFindCustomer.UseVisualStyleBackColor = false;
            this.btnFindCustomer.Click += new System.EventHandler(this.btnFindField_Click);
            // 
            // cmbFieldType
            // 
            this.cmbFieldType.FormattingEnabled = true;
            this.cmbFieldType.Location = new System.Drawing.Point(215, 461);
            this.cmbFieldType.Name = "cmbFieldType";
            this.cmbFieldType.Size = new System.Drawing.Size(186, 24);
            this.cmbFieldType.TabIndex = 75;
            // 
            // MenuRegField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(62)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(992, 668);
            this.Controls.Add(this.cmbFieldType);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtHourlyCost);
            this.Controls.Add(this.txtCapacity);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnRegField);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.btnFindCustomer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuRegField";
            this.Text = "MenuRegField";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtHourlyCost;
        private System.Windows.Forms.TextBox txtCapacity;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnRegField;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnFindCustomer;
        private System.Windows.Forms.ComboBox cmbFieldType;
    }
}