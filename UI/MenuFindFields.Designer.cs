namespace UI
{
    partial class MenuFindFields
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
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnModField = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFindField = new System.Windows.Forms.Button();
            this.dataGridViewFields = new System.Windows.Forms.DataGridView();
            this.btnRegField = new System.Windows.Forms.Button();
            this.btnMenuAdmin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.cmbFieldType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFields)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(910, 295);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(208, 20);
            this.lblStatus.TabIndex = 103;
            this.lblStatus.Text = "Listo para buscar canchas";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btnModField
            // 
            this.btnModField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnModField.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModField.Location = new System.Drawing.Point(423, 12);
            this.btnModField.Name = "btnModField";
            this.btnModField.Size = new System.Drawing.Size(176, 85);
            this.btnModField.TabIndex = 102;
            this.btnModField.Text = "Modificar Cancha";
            this.btnModField.UseVisualStyleBackColor = false;
            this.btnModField.Click += new System.EventHandler(this.btnModField_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(433, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 18);
            this.label3.TabIndex = 98;
            this.label3.Text = "Estado";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(57, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 18);
            this.label2.TabIndex = 92;
            this.label2.Text = "Tipo de Cancha";
            // 
            // btnFindField
            // 
            this.btnFindField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindField.Location = new System.Drawing.Point(940, 248);
            this.btnFindField.Name = "btnFindField";
            this.btnFindField.Size = new System.Drawing.Size(167, 44);
            this.btnFindField.TabIndex = 91;
            this.btnFindField.Text = "Buscar Canchas";
            this.btnFindField.UseVisualStyleBackColor = true;
            this.btnFindField.Click += new System.EventHandler(this.btnFindField_Click);
            // 
            // dataGridViewFields
            // 
            this.dataGridViewFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFields.Location = new System.Drawing.Point(60, 323);
            this.dataGridViewFields.Name = "dataGridViewFields";
            this.dataGridViewFields.RowHeadersWidth = 51;
            this.dataGridViewFields.RowTemplate.Height = 24;
            this.dataGridViewFields.Size = new System.Drawing.Size(1110, 379);
            this.dataGridViewFields.TabIndex = 90;
            // 
            // btnRegField
            // 
            this.btnRegField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRegField.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegField.Location = new System.Drawing.Point(241, 12);
            this.btnRegField.Name = "btnRegField";
            this.btnRegField.Size = new System.Drawing.Size(176, 85);
            this.btnRegField.TabIndex = 89;
            this.btnRegField.Text = "Cargar Cancha";
            this.btnRegField.UseVisualStyleBackColor = false;
            this.btnRegField.Click += new System.EventHandler(this.btnRegField_Click);
            // 
            // btnMenuAdmin
            // 
            this.btnMenuAdmin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnMenuAdmin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuAdmin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnMenuAdmin.Location = new System.Drawing.Point(60, 12);
            this.btnMenuAdmin.Name = "btnMenuAdmin";
            this.btnMenuAdmin.Size = new System.Drawing.Size(175, 85);
            this.btnMenuAdmin.TabIndex = 88;
            this.btnMenuAdmin.Text = "Menu Administración";
            this.btnMenuAdmin.UseVisualStyleBackColor = false;
            this.btnMenuAdmin.Click += new System.EventHandler(this.btnMenuAdmin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(428, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 32);
            this.label1.TabIndex = 87;
            this.label1.Text = "Búsqueda de Canchas";
            // 
            // cmbState
            // 
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(545, 186);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(188, 24);
            this.cmbState.TabIndex = 104;
            // 
            // cmbFieldType
            // 
            this.cmbFieldType.FormattingEnabled = true;
            this.cmbFieldType.Location = new System.Drawing.Point(198, 186);
            this.cmbFieldType.Name = "cmbFieldType";
            this.cmbFieldType.Size = new System.Drawing.Size(188, 24);
            this.cmbFieldType.TabIndex = 105;
            // 
            // MenuFindFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(62)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1227, 715);
            this.Controls.Add(this.cmbFieldType);
            this.Controls.Add(this.cmbState);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnModField);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnFindField);
            this.Controls.Add(this.dataGridViewFields);
            this.Controls.Add(this.btnRegField);
            this.Controls.Add(this.btnMenuAdmin);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuFindFields";
            this.Text = "MenuFindFields";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFields)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnModField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFindField;
        private System.Windows.Forms.DataGridView dataGridViewFields;
        private System.Windows.Forms.Button btnRegField;
        private System.Windows.Forms.Button btnMenuAdmin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbState;
        private System.Windows.Forms.ComboBox cmbFieldType;
    }
}