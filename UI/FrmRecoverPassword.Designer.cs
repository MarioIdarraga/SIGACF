namespace UI
{
    partial class FrmRecoverPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRecoverPassword));
            this.label1 = new System.Windows.Forms.Label();
            this.txtDato = new System.Windows.Forms.TextBox();
            this.btnEnviarRecoveryPassword = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRestaurarRecoveryPassword = new System.Windows.Forms.PictureBox();
            this.btnMinimizarRecoveryPassword = new System.Windows.Forms.PictureBox();
            this.btnMaximizarLogin = new System.Windows.Forms.PictureBox();
            this.btnCerrarRecoveryPassword = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnRestaurarRecoveryPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizarRecoveryPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximizarLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrarRecoveryPassword)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SeaGreen;
            this.label1.Location = new System.Drawing.Point(111, 97);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(301, 35);
            this.label1.TabIndex = 6;
            this.label1.Text = "Recuperar Contraseña";
            // 
            // txtDato
            // 
            this.txtDato.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtDato.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDato.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDato.ForeColor = System.Drawing.Color.SeaGreen;
            this.txtDato.Location = new System.Drawing.Point(101, 187);
            this.txtDato.Margin = new System.Windows.Forms.Padding(4);
            this.txtDato.Name = "txtDato";
            this.txtDato.Size = new System.Drawing.Size(322, 34);
            this.txtDato.TabIndex = 7;
            this.txtDato.Text = "Ingrese su usuario o correo (*)";
            this.txtDato.Enter += new System.EventHandler(this.txtDato_Enter);
            this.txtDato.Leave += new System.EventHandler(this.txtDato_Leave);
            // 
            // btnEnviarRecoveryPassword
            // 
            this.btnEnviarRecoveryPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnEnviarRecoveryPassword.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnEnviarRecoveryPassword.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnEnviarRecoveryPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnviarRecoveryPassword.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviarRecoveryPassword.ForeColor = System.Drawing.Color.SeaGreen;
            this.btnEnviarRecoveryPassword.Location = new System.Drawing.Point(101, 310);
            this.btnEnviarRecoveryPassword.Margin = new System.Windows.Forms.Padding(4);
            this.btnEnviarRecoveryPassword.Name = "btnEnviarRecoveryPassword";
            this.btnEnviarRecoveryPassword.Size = new System.Drawing.Size(322, 49);
            this.btnEnviarRecoveryPassword.TabIndex = 8;
            this.btnEnviarRecoveryPassword.Text = "Enviar Solicitud";
            this.btnEnviarRecoveryPassword.UseVisualStyleBackColor = false;
            this.btnEnviarRecoveryPassword.Click += new System.EventHandler(this.btnEnviarRecoveryPassword_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.SeaGreen;
            this.label2.Location = new System.Drawing.Point(70, 258);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(495, 29);
            this.label2.TabIndex = 9;
            this.label2.Text = "*Se enviara un correo si los datos son correctos";
            // 
            // btnRestaurarRecoveryPassword
            // 
            this.btnRestaurarRecoveryPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestaurarRecoveryPassword.Image = ((System.Drawing.Image)(resources.GetObject("btnRestaurarRecoveryPassword.Image")));
            this.btnRestaurarRecoveryPassword.Location = new System.Drawing.Point(452, 13);
            this.btnRestaurarRecoveryPassword.Margin = new System.Windows.Forms.Padding(4);
            this.btnRestaurarRecoveryPassword.Name = "btnRestaurarRecoveryPassword";
            this.btnRestaurarRecoveryPassword.Size = new System.Drawing.Size(33, 31);
            this.btnRestaurarRecoveryPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnRestaurarRecoveryPassword.TabIndex = 13;
            this.btnRestaurarRecoveryPassword.TabStop = false;
            this.btnRestaurarRecoveryPassword.Click += new System.EventHandler(this.btnRestaurarLogin_Click);
            // 
            // btnMinimizarRecoveryPassword
            // 
            this.btnMinimizarRecoveryPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimizarRecoveryPassword.Image = ((System.Drawing.Image)(resources.GetObject("btnMinimizarRecoveryPassword.Image")));
            this.btnMinimizarRecoveryPassword.Location = new System.Drawing.Point(411, 13);
            this.btnMinimizarRecoveryPassword.Margin = new System.Windows.Forms.Padding(4);
            this.btnMinimizarRecoveryPassword.Name = "btnMinimizarRecoveryPassword";
            this.btnMinimizarRecoveryPassword.Size = new System.Drawing.Size(33, 31);
            this.btnMinimizarRecoveryPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMinimizarRecoveryPassword.TabIndex = 12;
            this.btnMinimizarRecoveryPassword.TabStop = false;
            this.btnMinimizarRecoveryPassword.Click += new System.EventHandler(this.btnMinimizarRecoveryPassword_Click);
            // 
            // btnMaximizarLogin
            // 
            this.btnMaximizarLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximizarLogin.Image = ((System.Drawing.Image)(resources.GetObject("btnMaximizarLogin.Image")));
            this.btnMaximizarLogin.Location = new System.Drawing.Point(452, 13);
            this.btnMaximizarLogin.Margin = new System.Windows.Forms.Padding(4);
            this.btnMaximizarLogin.Name = "btnMaximizarLogin";
            this.btnMaximizarLogin.Size = new System.Drawing.Size(33, 31);
            this.btnMaximizarLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMaximizarLogin.TabIndex = 11;
            this.btnMaximizarLogin.TabStop = false;
            // 
            // btnCerrarRecoveryPassword
            // 
            this.btnCerrarRecoveryPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrarRecoveryPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarRecoveryPassword.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrarRecoveryPassword.Image")));
            this.btnCerrarRecoveryPassword.Location = new System.Drawing.Point(493, 13);
            this.btnCerrarRecoveryPassword.Margin = new System.Windows.Forms.Padding(4);
            this.btnCerrarRecoveryPassword.Name = "btnCerrarRecoveryPassword";
            this.btnCerrarRecoveryPassword.Size = new System.Drawing.Size(33, 31);
            this.btnCerrarRecoveryPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnCerrarRecoveryPassword.TabIndex = 10;
            this.btnCerrarRecoveryPassword.TabStop = false;
            this.btnCerrarRecoveryPassword.Click += new System.EventHandler(this.btnCerrarRecoveryPassword_Click);
            // 
            // FrmRecoverPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ClientSize = new System.Drawing.Size(545, 450);
            this.Controls.Add(this.btnRestaurarRecoveryPassword);
            this.Controls.Add(this.btnMinimizarRecoveryPassword);
            this.Controls.Add(this.btnMaximizarLogin);
            this.Controls.Add(this.btnCerrarRecoveryPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnEnviarRecoveryPassword);
            this.Controls.Add(this.txtDato);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmRecoverPassword";
            this.Text = "FrmRecoverPassword";
            ((System.ComponentModel.ISupportInitialize)(this.btnRestaurarRecoveryPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizarRecoveryPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximizarLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrarRecoveryPassword)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDato;
        private System.Windows.Forms.Button btnEnviarRecoveryPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox btnRestaurarRecoveryPassword;
        private System.Windows.Forms.PictureBox btnMinimizarRecoveryPassword;
        private System.Windows.Forms.PictureBox btnMaximizarLogin;
        private System.Windows.Forms.PictureBox btnCerrarRecoveryPassword;
    }
}