namespace UI
{
    partial class MenuModPatent
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
            this.txtFormName = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnModPatent = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btnFindPatent = new System.Windows.Forms.Button();
            this.btnRegPatent = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label10.Location = new System.Drawing.Point(391, 186);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(253, 32);
            this.label10.TabIndex = 103;
            this.label10.Text = "Modificar Patente";
            // 
            // txtFormName
            // 
            this.txtFormName.Location = new System.Drawing.Point(286, 328);
            this.txtFormName.Name = "txtFormName";
            this.txtFormName.Size = new System.Drawing.Size(186, 22);
            this.txtFormName.TabIndex = 102;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(286, 259);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(186, 22);
            this.txtName.TabIndex = 101;
            // 
            // btnModPatent
            // 
            this.btnModPatent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModPatent.Location = new System.Drawing.Point(579, 501);
            this.btnModPatent.Name = "btnModPatent";
            this.btnModPatent.Size = new System.Drawing.Size(162, 42);
            this.btnModPatent.TabIndex = 100;
            this.btnModPatent.Text = "Modificar Patente";
            this.btnModPatent.UseVisualStyleBackColor = true;
            this.btnModPatent.Click += new System.EventHandler(this.btnModPatent_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label16.Location = new System.Drawing.Point(109, 332);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(155, 18);
            this.label16.TabIndex = 99;
            this.label16.Text = "Nombre Formulario";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label18.Location = new System.Drawing.Point(109, 263);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(68, 18);
            this.label18.TabIndex = 98;
            this.label18.Text = "Nombre";
            // 
            // btnFindPatent
            // 
            this.btnFindPatent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnFindPatent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindPatent.Location = new System.Drawing.Point(112, 55);
            this.btnFindPatent.Name = "btnFindPatent";
            this.btnFindPatent.Size = new System.Drawing.Size(178, 87);
            this.btnFindPatent.TabIndex = 97;
            this.btnFindPatent.Text = "Búsqueda de Patentes";
            this.btnFindPatent.UseVisualStyleBackColor = false;
            this.btnFindPatent.Click += new System.EventHandler(this.btnFindPatent_Click);
            // 
            // btnRegPatent
            // 
            this.btnRegPatent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRegPatent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegPatent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegPatent.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRegPatent.Location = new System.Drawing.Point(301, 56);
            this.btnRegPatent.Name = "btnRegPatent";
            this.btnRegPatent.Size = new System.Drawing.Size(171, 86);
            this.btnRegPatent.TabIndex = 104;
            this.btnRegPatent.Text = "Cargar Patente";
            this.btnRegPatent.UseVisualStyleBackColor = false;
            this.btnRegPatent.Click += new System.EventHandler(this.btnRegPatent_Click);
            // 
            // MenuModPatent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(62)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(988, 654);
            this.Controls.Add(this.btnRegPatent);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtFormName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnModPatent);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.btnFindPatent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuModPatent";
            this.Text = "MenuModPatent";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtFormName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnModPatent;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnFindPatent;
        private System.Windows.Forms.Button btnRegPatent;
    }
}