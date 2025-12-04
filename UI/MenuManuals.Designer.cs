namespace UI
{
    partial class MenuManuals
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
            this.btnInstallManual = new System.Windows.Forms.Button();
            this.btnDeveloperManusl = new System.Windows.Forms.Button();
            this.btnUserManual = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(368, 243);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(484, 32);
            this.label1.TabIndex = 88;
            this.label1.Text = "Menu de Descargada de Manuales";
            // 
            // btnInstallManual
            // 
            this.btnInstallManual.BackColor = System.Drawing.Color.DarkOrange;
            this.btnInstallManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInstallManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInstallManual.Location = new System.Drawing.Point(697, 340);
            this.btnInstallManual.Name = "btnInstallManual";
            this.btnInstallManual.Size = new System.Drawing.Size(176, 85);
            this.btnInstallManual.TabIndex = 87;
            this.btnInstallManual.Text = "Manual de Instalación";
            this.btnInstallManual.UseVisualStyleBackColor = false;
            this.btnInstallManual.Click += new System.EventHandler(this.btnInstallManual_Click);
            // 
            // btnDeveloperManual
            // 
            this.btnDeveloperManusl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnDeveloperManusl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeveloperManusl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeveloperManusl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDeveloperManusl.Location = new System.Drawing.Point(520, 340);
            this.btnDeveloperManusl.Name = "btnDeveloperManusl";
            this.btnDeveloperManusl.Size = new System.Drawing.Size(171, 86);
            this.btnDeveloperManusl.TabIndex = 86;
            this.btnDeveloperManusl.Text = "Manual de Desarrollador";
            this.btnDeveloperManusl.UseVisualStyleBackColor = false;
            this.btnDeveloperManusl.Click += new System.EventHandler(this.btnDeveloperManual_Click);
            // 
            // btnUserManual
            // 
            this.btnUserManual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnUserManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUserManual.Location = new System.Drawing.Point(340, 340);
            this.btnUserManual.Name = "btnUserManual";
            this.btnUserManual.Size = new System.Drawing.Size(174, 86);
            this.btnUserManual.TabIndex = 85;
            this.btnUserManual.Text = "Manual de Usuario";
            this.btnUserManual.UseVisualStyleBackColor = false;
            this.btnUserManual.Click += new System.EventHandler(this.btnUserManual_Click);
            // 
            // MenuManuals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(62)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1249, 762);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnInstallManual);
            this.Controls.Add(this.btnDeveloperManusl);
            this.Controls.Add(this.btnUserManual);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuManuals";
            this.Text = "MenuManuals";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnInstallManual;
        private System.Windows.Forms.Button btnDeveloperManusl;
        private System.Windows.Forms.Button btnUserManual;
    }
}