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
            this.btnRepBooking = new System.Windows.Forms.Button();
            this.btnAdmin = new System.Windows.Forms.Button();
            this.btnAdminEmployees = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(438, 201);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(341, 32);
            this.label1.TabIndex = 83;
            this.label1.Text = "Menu de Administración";
            // 
            // btnRepBooking
            // 
            this.btnRepBooking.BackColor = System.Drawing.Color.DarkOrange;
            this.btnRepBooking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepBooking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepBooking.Location = new System.Drawing.Point(672, 304);
            this.btnRepBooking.Name = "btnRepBooking";
            this.btnRepBooking.Size = new System.Drawing.Size(176, 85);
            this.btnRepBooking.TabIndex = 82;
            this.btnRepBooking.Text = "Promociones";
            this.btnRepBooking.UseVisualStyleBackColor = false;
            // 
            // btnAdmin
            // 
            this.btnAdmin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnAdmin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdmin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAdmin.Location = new System.Drawing.Point(495, 304);
            this.btnAdmin.Name = "btnAdmin";
            this.btnAdmin.Size = new System.Drawing.Size(171, 86);
            this.btnAdmin.TabIndex = 81;
            this.btnAdmin.Text = "Canchas";
            this.btnAdmin.UseVisualStyleBackColor = false;
            // 
            // btnAdminEmployees
            // 
            this.btnAdminEmployees.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAdminEmployees.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdminEmployees.Location = new System.Drawing.Point(315, 304);
            this.btnAdminEmployees.Name = "btnAdminEmployees";
            this.btnAdminEmployees.Size = new System.Drawing.Size(174, 86);
            this.btnAdminEmployees.TabIndex = 80;
            this.btnAdminEmployees.Text = "Empleados";
            this.btnAdminEmployees.UseVisualStyleBackColor = false;
            this.btnAdminEmployees.Click += new System.EventHandler(this.btnAdminEmployees_Click);
            // 
            // MenuAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(62)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1162, 694);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRepBooking);
            this.Controls.Add(this.btnAdmin);
            this.Controls.Add(this.btnAdminEmployees);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuAdmin";
            this.Text = "MenuAdmin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRepBooking;
        private System.Windows.Forms.Button btnAdmin;
        private System.Windows.Forms.Button btnAdminEmployees;
    }
}