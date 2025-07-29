namespace UI
{
    partial class MenuFindPatents
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
            this.btnModFamily = new System.Windows.Forms.Button();
            this.btnFindPatents = new System.Windows.Forms.Button();
            this.dataGridViewPatents = new System.Windows.Forms.DataGridView();
            this.btnRegFamily = new System.Windows.Forms.Button();
            this.btnMenuUsers = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPatents)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(452, 260);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(209, 20);
            this.lblStatus.TabIndex = 110;
            this.lblStatus.Text = "Listo para buscar patentes";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btnModFamily
            // 
            this.btnModFamily.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnModFamily.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModFamily.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModFamily.Location = new System.Drawing.Point(394, 9);
            this.btnModFamily.Name = "btnModFamily";
            this.btnModFamily.Size = new System.Drawing.Size(176, 85);
            this.btnModFamily.TabIndex = 109;
            this.btnModFamily.Text = "Modificar Patente";
            this.btnModFamily.UseVisualStyleBackColor = false;
            this.btnModFamily.Click += new System.EventHandler(this.btnModPatent_Click);
            // 
            // btnFindPatents
            // 
            this.btnFindPatents.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindPatents.Location = new System.Drawing.Point(478, 213);
            this.btnFindPatents.Name = "btnFindPatents";
            this.btnFindPatents.Size = new System.Drawing.Size(167, 44);
            this.btnFindPatents.TabIndex = 108;
            this.btnFindPatents.Text = "Buscar Patentes";
            this.btnFindPatents.UseVisualStyleBackColor = true;
            this.btnFindPatents.Click += new System.EventHandler(this.btnFindPatent_Click);
            // 
            // dataGridViewPatents
            // 
            this.dataGridViewPatents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPatents.Location = new System.Drawing.Point(28, 322);
            this.dataGridViewPatents.Name = "dataGridViewPatents";
            this.dataGridViewPatents.RowHeadersWidth = 51;
            this.dataGridViewPatents.RowTemplate.Height = 24;
            this.dataGridViewPatents.Size = new System.Drawing.Size(1110, 379);
            this.dataGridViewPatents.TabIndex = 107;
            // 
            // btnRegFamily
            // 
            this.btnRegFamily.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRegFamily.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegFamily.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegFamily.Location = new System.Drawing.Point(212, 9);
            this.btnRegFamily.Name = "btnRegFamily";
            this.btnRegFamily.Size = new System.Drawing.Size(176, 85);
            this.btnRegFamily.TabIndex = 106;
            this.btnRegFamily.Text = "Registrar Patente";
            this.btnRegFamily.UseVisualStyleBackColor = false;
            this.btnRegFamily.Click += new System.EventHandler(this.btnRegPatent_Click);
            // 
            // btnMenuUsers
            // 
            this.btnMenuUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnMenuUsers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuUsers.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnMenuUsers.Location = new System.Drawing.Point(31, 9);
            this.btnMenuUsers.Name = "btnMenuUsers";
            this.btnMenuUsers.Size = new System.Drawing.Size(175, 85);
            this.btnMenuUsers.TabIndex = 105;
            this.btnMenuUsers.Text = "Menu Usuarios";
            this.btnMenuUsers.UseVisualStyleBackColor = false;
            this.btnMenuUsers.Click += new System.EventHandler(this.btnMenuUsers_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(412, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(316, 32);
            this.label1.TabIndex = 104;
            this.label1.Text = "Busqueda de Familias";
            // 
            // MenuFindPatents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(62)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1166, 710);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnModFamily);
            this.Controls.Add(this.btnFindPatents);
            this.Controls.Add(this.dataGridViewPatents);
            this.Controls.Add(this.btnRegFamily);
            this.Controls.Add(this.btnMenuUsers);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuFindPatents";
            this.Text = "MenuFindPatents";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPatents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnModFamily;
        private System.Windows.Forms.Button btnFindPatents;
        private System.Windows.Forms.DataGridView dataGridViewPatents;
        private System.Windows.Forms.Button btnRegFamily;
        private System.Windows.Forms.Button btnMenuUsers;
        private System.Windows.Forms.Label label1;
    }
}