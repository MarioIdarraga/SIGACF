namespace UI
{
    partial class MenuModField
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
            this.btnRegField = new System.Windows.Forms.Button();
            this.btnFindField = new System.Windows.Forms.Button();
            this.btnModField = new System.Windows.Forms.Button();
            this.cmbFieldType = new System.Windows.Forms.ComboBox();
            this.txtHourlyCost = new System.Windows.Forms.TextBox();
            this.txtCapacity = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtState = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(425, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 32);
            this.label1.TabIndex = 59;
            this.label1.Text = "Modificar Cancha";
            // 
            // btnRegField
            // 
            this.btnRegField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRegField.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegField.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRegField.Location = new System.Drawing.Point(232, 55);
            this.btnRegField.Name = "btnRegField";
            this.btnRegField.Size = new System.Drawing.Size(171, 86);
            this.btnRegField.TabIndex = 58;
            this.btnRegField.Text = "Cargar Cancha";
            this.btnRegField.UseVisualStyleBackColor = false;
            this.btnRegField.Click += new System.EventHandler(this.btnRegField_Click);
            // 
            // btnFindField
            // 
            this.btnFindField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnFindField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindField.Location = new System.Drawing.Point(52, 55);
            this.btnFindField.Name = "btnFindField";
            this.btnFindField.Size = new System.Drawing.Size(174, 86);
            this.btnFindField.TabIndex = 57;
            this.btnFindField.Text = "Búsqueda de Canchas";
            this.btnFindField.UseVisualStyleBackColor = false;
            this.btnFindField.Click += new System.EventHandler(this.btnFindField_Click);
            // 
            // btnModField
            // 
            this.btnModField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModField.Location = new System.Drawing.Point(507, 506);
            this.btnModField.Name = "btnModField";
            this.btnModField.Size = new System.Drawing.Size(162, 42);
            this.btnModField.TabIndex = 48;
            this.btnModField.Text = "Modificar Cancha";
            this.btnModField.UseVisualStyleBackColor = true;
            this.btnModField.Click += new System.EventHandler(this.btnModField_Click);
            // 
            // cmbFieldType
            // 
            this.cmbFieldType.FormattingEnabled = true;
            this.cmbFieldType.Location = new System.Drawing.Point(218, 437);
            this.cmbFieldType.Name = "cmbFieldType";
            this.cmbFieldType.Size = new System.Drawing.Size(185, 24);
            this.cmbFieldType.TabIndex = 83;
            // 
            // txtHourlyCost
            // 
            this.txtHourlyCost.Location = new System.Drawing.Point(218, 373);
            this.txtHourlyCost.Name = "txtHourlyCost";
            this.txtHourlyCost.Size = new System.Drawing.Size(185, 22);
            this.txtHourlyCost.TabIndex = 82;
            // 
            // txtCapacity
            // 
            this.txtCapacity.Location = new System.Drawing.Point(218, 307);
            this.txtCapacity.Name = "txtCapacity";
            this.txtCapacity.Size = new System.Drawing.Size(185, 22);
            this.txtCapacity.TabIndex = 81;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(218, 238);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(185, 22);
            this.txtName.TabIndex = 80;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label14.Location = new System.Drawing.Point(52, 444);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(129, 18);
            this.label14.TabIndex = 79;
            this.label14.Text = "Tipo De Cancha";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label15.Location = new System.Drawing.Point(52, 377);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(96, 18);
            this.label15.TabIndex = 78;
            this.label15.Text = "Costo Hora";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label16.Location = new System.Drawing.Point(52, 311);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(87, 18);
            this.label16.TabIndex = 77;
            this.label16.Text = "Capacidad";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label18.Location = new System.Drawing.Point(52, 242);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(68, 18);
            this.label18.TabIndex = 76;
            this.label18.Text = "Nombre";
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(218, 506);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(185, 22);
            this.txtState.TabIndex = 85;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(52, 510);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 18);
            this.label2.TabIndex = 84;
            this.label2.Text = "Estado";
            // 
            // MenuModField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(62)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(990, 666);
            this.Controls.Add(this.txtState);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbFieldType);
            this.Controls.Add(this.txtHourlyCost);
            this.Controls.Add(this.txtCapacity);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRegField);
            this.Controls.Add(this.btnFindField);
            this.Controls.Add(this.btnModField);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuModField";
            this.Text = "MenuModField";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRegField;
        private System.Windows.Forms.Button btnFindField;
        private System.Windows.Forms.Button btnModField;
        private System.Windows.Forms.ComboBox cmbFieldType;
        private System.Windows.Forms.TextBox txtHourlyCost;
        private System.Windows.Forms.TextBox txtCapacity;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.Label label2;
    }
}