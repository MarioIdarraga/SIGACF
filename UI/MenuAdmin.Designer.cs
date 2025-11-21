namespace UI
{
    partial class MenuAdmin
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
            this.btnAdminProm = new System.Windows.Forms.Button();
            this.btnAdminFields = new System.Windows.Forms.Button();
            this.btnAdminEmployees = new System.Windows.Forms.Button();
            this.btnAdminManuals = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(305, 238);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(341, 32);
            this.label1.TabIndex = 83;
            this.label1.Text = "Menu de Administración";
            // 
            // btnAdminProm
            // 
            this.btnAdminProm.BackColor = System.Drawing.Color.DarkOrange;
            this.btnAdminProm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdminProm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdminProm.Location = new System.Drawing.Point(454, 331);
            this.btnAdminProm.Name = "btnAdminProm";
            this.btnAdminProm.Size = new System.Drawing.Size(176, 85);
            this.btnAdminProm.TabIndex = 82;
            this.btnAdminProm.Text = "Promociones";
            this.btnAdminProm.UseVisualStyleBackColor = false;
            this.btnAdminProm.Click += new System.EventHandler(this.btnAdminProm_Click);
            // 
            // btnAdminFields
            // 
            this.btnAdminFields.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnAdminFields.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdminFields.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdminFields.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAdminFields.Location = new System.Drawing.Point(277, 331);
            this.btnAdminFields.Name = "btnAdminFields";
            this.btnAdminFields.Size = new System.Drawing.Size(171, 86);
            this.btnAdminFields.TabIndex = 81;
            this.btnAdminFields.Text = "Canchas";
            this.btnAdminFields.UseVisualStyleBackColor = false;
            this.btnAdminFields.Click += new System.EventHandler(this.btnAdminFields_Click);
            // 
            // btnAdminEmployees
            // 
            this.btnAdminEmployees.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAdminEmployees.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdminEmployees.Location = new System.Drawing.Point(97, 331);
            this.btnAdminEmployees.Name = "btnAdminEmployees";
            this.btnAdminEmployees.Size = new System.Drawing.Size(174, 86);
            this.btnAdminEmployees.TabIndex = 80;
            this.btnAdminEmployees.Text = "Usuarios / Permisos";
            this.btnAdminEmployees.UseVisualStyleBackColor = false;
            this.btnAdminEmployees.Click += new System.EventHandler(this.btnAdminEmployees_Click);
            // 
            // btnAdminManuals
            // 
            this.btnAdminManuals.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnAdminManuals.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdminManuals.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdminManuals.Location = new System.Drawing.Point(636, 331);
            this.btnAdminManuals.Name = "btnAdminManuals";
            this.btnAdminManuals.Size = new System.Drawing.Size(176, 85);
            this.btnAdminManuals.TabIndex = 84;
            this.btnAdminManuals.Text = "BackUp";
            this.btnAdminManuals.UseVisualStyleBackColor = false;
            this.btnAdminManuals.Click += new System.EventHandler(this.btnAdminManuals_Click);
            // 
            // MenuAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(62)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(929, 555);
            this.Controls.Add(this.btnAdminManuals);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAdminProm);
            this.Controls.Add(this.btnAdminFields);
            this.Controls.Add(this.btnAdminEmployees);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuAdmin";
            this.Text = "MenuAdmin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdminProm;
        private System.Windows.Forms.Button btnAdminFields;
        private System.Windows.Forms.Button btnAdminEmployees;
        private System.Windows.Forms.Button btnAdminManuals;
    }
}