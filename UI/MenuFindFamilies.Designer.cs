namespace UI
{
    partial class MenuFindFamilies
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
            this.btnFindFamily = new System.Windows.Forms.Button();
            this.dataGridViewUsers = new System.Windows.Forms.DataGridView();
            this.btnRegFamily = new System.Windows.Forms.Button();
            this.btnMenuAdmin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(454, 257);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(203, 20);
            this.lblStatus.TabIndex = 103;
            this.lblStatus.Text = "Listo para buscar familias";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btnModFamily
            // 
            this.btnModFamily.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnModFamily.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModFamily.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModFamily.Location = new System.Drawing.Point(392, 6);
            this.btnModFamily.Name = "btnModFamily";
            this.btnModFamily.Size = new System.Drawing.Size(176, 85);
            this.btnModFamily.TabIndex = 102;
            this.btnModFamily.Text = "Modificar Familia";
            this.btnModFamily.UseVisualStyleBackColor = false;
            this.btnModFamily.Click += new System.EventHandler(this.btnModFamily_Click);
            // 
            // btnFindFamily
            // 
            this.btnFindFamily.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindFamily.Location = new System.Drawing.Point(476, 210);
            this.btnFindFamily.Name = "btnFindFamily";
            this.btnFindFamily.Size = new System.Drawing.Size(167, 44);
            this.btnFindFamily.TabIndex = 91;
            this.btnFindFamily.Text = "Buscar Familias";
            this.btnFindFamily.UseVisualStyleBackColor = true;
            this.btnFindFamily.Click += new System.EventHandler(this.btnFindFamily_Click);
            // 
            // dataGridViewUsers
            // 
            this.dataGridViewUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUsers.Location = new System.Drawing.Point(26, 319);
            this.dataGridViewUsers.Name = "dataGridViewUsers";
            this.dataGridViewUsers.RowHeadersWidth = 51;
            this.dataGridViewUsers.RowTemplate.Height = 24;
            this.dataGridViewUsers.Size = new System.Drawing.Size(1110, 379);
            this.dataGridViewUsers.TabIndex = 90;
            // 
            // btnRegFamily
            // 
            this.btnRegFamily.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRegFamily.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegFamily.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegFamily.Location = new System.Drawing.Point(210, 6);
            this.btnRegFamily.Name = "btnRegFamily";
            this.btnRegFamily.Size = new System.Drawing.Size(176, 85);
            this.btnRegFamily.TabIndex = 89;
            this.btnRegFamily.Text = "Registrar Familia";
            this.btnRegFamily.UseVisualStyleBackColor = false;
            this.btnRegFamily.Click += new System.EventHandler(this.btnRegFamily_Click);
            // 
            // btnMenuAdmin
            // 
            this.btnMenuAdmin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnMenuAdmin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuAdmin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnMenuAdmin.Location = new System.Drawing.Point(29, 6);
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
            this.label1.Location = new System.Drawing.Point(410, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(316, 32);
            this.label1.TabIndex = 87;
            this.label1.Text = "Busqueda de Familias";
            // 
            // MenuFindFamilies
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(62)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1165, 710);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnModFamily);
            this.Controls.Add(this.btnFindFamily);
            this.Controls.Add(this.dataGridViewUsers);
            this.Controls.Add(this.btnRegFamily);
            this.Controls.Add(this.btnMenuAdmin);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuFindFamilies";
            this.Text = "MenuRolePremission";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnModFamily;
        private System.Windows.Forms.Button btnFindFamily;
        private System.Windows.Forms.DataGridView dataGridViewUsers;
        private System.Windows.Forms.Button btnRegFamily;
        private System.Windows.Forms.Button btnMenuAdmin;
        private System.Windows.Forms.Label label1;
    }
}