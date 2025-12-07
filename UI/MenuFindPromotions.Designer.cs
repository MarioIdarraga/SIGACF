namespace UI
{
    partial class MenuFindPromotions
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
            this.btnModPromotion = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtNroDocument = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFindPromotion = new System.Windows.Forms.Button();
            this.dataGridViewUsers = new System.Windows.Forms.DataGridView();
            this.btnRegPromotion = new System.Windows.Forms.Button();
            this.btnMenuAdmin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(841, 290);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(270, 20);
            this.lblStatus.TabIndex = 103;
            this.lblStatus.Text = "Listo para buscar promociones";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btnModPromotion
            // 
            this.btnModPromotion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnModPromotion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModPromotion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModPromotion.Location = new System.Drawing.Point(385, 7);
            this.btnModPromotion.Name = "btnModPromotion";
            this.btnModPromotion.Size = new System.Drawing.Size(176, 85);
            this.btnModPromotion.TabIndex = 102;
            this.btnModPromotion.Text = "Modificar Promoción";
            this.btnModPromotion.UseVisualStyleBackColor = false;
            this.btnModPromotion.Click += new System.EventHandler(this.btnModPromotion_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(389, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 18);
            this.label3.TabIndex = 98;
            this.label3.Text = "Fecha Hasta";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(506, 183);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(188, 22);
            this.txtFirstName.TabIndex = 94;
            // 
            // txtNroDocument
            // 
            this.txtNroDocument.Location = new System.Drawing.Point(202, 183);
            this.txtNroDocument.Name = "txtNroDocument";
            this.txtNroDocument.Size = new System.Drawing.Size(170, 22);
            this.txtNroDocument.TabIndex = 93;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(19, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 18);
            this.label2.TabIndex = 92;
            this.label2.Text = "Fecha Desde";
            // 
            // btnFindPromotion
            // 
            this.btnFindPromotion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindPromotion.Location = new System.Drawing.Point(902, 243);
            this.btnFindPromotion.Name = "btnFindPromotion";
            this.btnFindPromotion.Size = new System.Drawing.Size(167, 44);
            this.btnFindPromotion.TabIndex = 91;
            this.btnFindPromotion.Text = "Buscar Promociones";
            this.btnFindPromotion.UseVisualStyleBackColor = true;
            this.btnFindPromotion.Click += new System.EventHandler(this.btnFindPromotion_Click);
            // 
            // dataGridViewUsers
            // 
            this.dataGridViewUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUsers.Location = new System.Drawing.Point(22, 318);
            this.dataGridViewUsers.Name = "dataGridViewUsers";
            this.dataGridViewUsers.RowHeadersWidth = 51;
            this.dataGridViewUsers.RowTemplate.Height = 24;
            this.dataGridViewUsers.Size = new System.Drawing.Size(1110, 379);
            this.dataGridViewUsers.TabIndex = 90;
            // 
            // btnRegPromotion
            // 
            this.btnRegPromotion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRegPromotion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegPromotion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegPromotion.Location = new System.Drawing.Point(203, 7);
            this.btnRegPromotion.Name = "btnRegPromotion";
            this.btnRegPromotion.Size = new System.Drawing.Size(176, 85);
            this.btnRegPromotion.TabIndex = 89;
            this.btnRegPromotion.Text = "Cargar Promoción";
            this.btnRegPromotion.UseVisualStyleBackColor = false;
            this.btnRegPromotion.Click += new System.EventHandler(this.btnRegPromotion_Click);
            // 
            // btnMenuAdmin
            // 
            this.btnMenuAdmin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnMenuAdmin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuAdmin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnMenuAdmin.Location = new System.Drawing.Point(22, 7);
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
            this.label1.Location = new System.Drawing.Point(390, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(378, 32);
            this.label1.TabIndex = 87;
            this.label1.Text = "Búsqueda de Promociónes";
            // 
            // MenuFindPromotions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(62)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1154, 704);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnModPromotion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.txtNroDocument);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnFindPromotion);
            this.Controls.Add(this.dataGridViewUsers);
            this.Controls.Add(this.btnRegPromotion);
            this.Controls.Add(this.btnMenuAdmin);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuFindPromotions";
            this.Text = "MenuFindPromotions";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnModPromotion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtNroDocument;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFindPromotion;
        private System.Windows.Forms.DataGridView dataGridViewUsers;
        private System.Windows.Forms.Button btnRegPromotion;
        private System.Windows.Forms.Button btnMenuAdmin;
        private System.Windows.Forms.Label label1;
    }
}