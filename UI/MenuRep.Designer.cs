namespace UI
{
    partial class MenuRep
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
            this.btnRepBooking = new System.Windows.Forms.Button();
            this.btnRepCan = new System.Windows.Forms.Button();
            this.btnRepSales = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRepBooking
            // 
            this.btnRepBooking.BackColor = System.Drawing.Color.DarkOrange;
            this.btnRepBooking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepBooking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepBooking.Location = new System.Drawing.Point(672, 304);
            this.btnRepBooking.Name = "btnRepBooking";
            this.btnRepBooking.Size = new System.Drawing.Size(176, 85);
            this.btnRepBooking.TabIndex = 78;
            this.btnRepBooking.Text = "Reservas";
            this.btnRepBooking.UseVisualStyleBackColor = false;
            this.btnRepBooking.Click += new System.EventHandler(this.btnRepBooking_Click);
            // 
            // btnRepCan
            // 
            this.btnRepCan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRepCan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepCan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepCan.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRepCan.Location = new System.Drawing.Point(495, 304);
            this.btnRepCan.Name = "btnRepCan";
            this.btnRepCan.Size = new System.Drawing.Size(171, 86);
            this.btnRepCan.TabIndex = 77;
            this.btnRepCan.Text = "Cancelaciones";
            this.btnRepCan.UseVisualStyleBackColor = false;
            this.btnRepCan.Click += new System.EventHandler(this.btnRepCan_Click);
            // 
            // btnRepSales
            // 
            this.btnRepSales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnRepSales.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepSales.Location = new System.Drawing.Point(315, 304);
            this.btnRepSales.Name = "btnRepSales";
            this.btnRepSales.Size = new System.Drawing.Size(174, 86);
            this.btnRepSales.TabIndex = 76;
            this.btnRepSales.Text = "Ventas Mensuales";
            this.btnRepSales.UseVisualStyleBackColor = false;
            this.btnRepSales.Click += new System.EventHandler(this.btnRepSales_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(438, 201);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(328, 40);
            this.label1.TabIndex = 79;
            this.label1.Text = "Menu de Reportes";
            // 
            // MenuRep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(62)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1162, 694);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRepBooking);
            this.Controls.Add(this.btnRepCan);
            this.Controls.Add(this.btnRepSales);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuRep";
            this.Text = "MenuRep";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRepBooking;
        private System.Windows.Forms.Button btnRepCan;
        private System.Windows.Forms.Button btnRepSales;
        private System.Windows.Forms.Label label1;
    }
}