namespace UI
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnToAccess = new System.Windows.Forms.Button();
            this.lnkPass = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCerrarLogin = new System.Windows.Forms.PictureBox();
            this.btnMaximizarLogin = new System.Windows.Forms.PictureBox();
            this.btnMinimizarLogin = new System.Windows.Forms.PictureBox();
            this.btnRestaurarLogin = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrarLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximizarLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizarLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRestaurarLogin)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SeaGreen;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(333, 556);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(81, 97);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(166, 187);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUser.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.ForeColor = System.Drawing.Color.SeaGreen;
            this.txtUser.Location = new System.Drawing.Point(548, 207);
            this.txtUser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(477, 34);
            this.txtUser.TabIndex = 1;
            this.txtUser.Text = "Usuario";
            this.txtUser.Enter += new System.EventHandler(this.txtUser_Enter);
            this.txtUser.Leave += new System.EventHandler(this.txtUser_Leave);
            // 
            // txtPass
            // 
            this.txtPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPass.CausesValidation = false;
            this.txtPass.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass.ForeColor = System.Drawing.Color.SeaGreen;
            this.txtPass.Location = new System.Drawing.Point(548, 310);
            this.txtPass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(477, 34);
            this.txtPass.TabIndex = 2;
            this.txtPass.Text = "Contraseña";
            this.txtPass.Enter += new System.EventHandler(this.txtPass_Enter);
            this.txtPass.Leave += new System.EventHandler(this.txtPass_Leave);
            // 
            // btnToAccess
            // 
            this.btnToAccess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnToAccess.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnToAccess.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnToAccess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToAccess.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToAccess.ForeColor = System.Drawing.Color.SeaGreen;
            this.btnToAccess.Location = new System.Drawing.Point(548, 382);
            this.btnToAccess.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnToAccess.Name = "btnToAccess";
            this.btnToAccess.Size = new System.Drawing.Size(476, 49);
            this.btnToAccess.TabIndex = 3;
            this.btnToAccess.Text = "Ingresar";
            this.btnToAccess.UseVisualStyleBackColor = false;
            this.btnToAccess.Click += new System.EventHandler(this.btnToAccess_Click);
            // 
            // lnkPass
            // 
            this.lnkPass.ActiveLinkColor = System.Drawing.Color.DarkGreen;
            this.lnkPass.AutoSize = true;
            this.lnkPass.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkPass.LinkColor = System.Drawing.Color.SeaGreen;
            this.lnkPass.Location = new System.Drawing.Point(668, 453);
            this.lnkPass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkPass.Name = "lnkPass";
            this.lnkPass.Size = new System.Drawing.Size(241, 23);
            this.lnkPass.TabIndex = 4;
            this.lnkPass.TabStop = true;
            this.lnkPass.Text = "¿Ha olvidado su contraseña?";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SeaGreen;
            this.label1.Location = new System.Drawing.Point(737, 123);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 35);
            this.label1.TabIndex = 5;
            this.label1.Text = "LOGIN";
            // 
            // btnCerrarLogin
            // 
            this.btnCerrarLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrarLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarLogin.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrarLogin.Image")));
            this.btnCerrarLogin.Location = new System.Drawing.Point(1217, 15);
            this.btnCerrarLogin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCerrarLogin.Name = "btnCerrarLogin";
            this.btnCerrarLogin.Size = new System.Drawing.Size(33, 31);
            this.btnCerrarLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnCerrarLogin.TabIndex = 6;
            this.btnCerrarLogin.TabStop = false;
            this.btnCerrarLogin.Click += new System.EventHandler(this.btnCerrarLogin_Click);
            // 
            // btnMaximizarLogin
            // 
            this.btnMaximizarLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximizarLogin.Image = ((System.Drawing.Image)(resources.GetObject("btnMaximizarLogin.Image")));
            this.btnMaximizarLogin.Location = new System.Drawing.Point(1176, 15);
            this.btnMaximizarLogin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMaximizarLogin.Name = "btnMaximizarLogin";
            this.btnMaximizarLogin.Size = new System.Drawing.Size(33, 31);
            this.btnMaximizarLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMaximizarLogin.TabIndex = 7;
            this.btnMaximizarLogin.TabStop = false;
            this.btnMaximizarLogin.Click += new System.EventHandler(this.btnMaximizarLogin_Click);
            // 
            // btnMinimizarLogin
            // 
            this.btnMinimizarLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimizarLogin.Image = ((System.Drawing.Image)(resources.GetObject("btnMinimizarLogin.Image")));
            this.btnMinimizarLogin.Location = new System.Drawing.Point(1135, 15);
            this.btnMinimizarLogin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMinimizarLogin.Name = "btnMinimizarLogin";
            this.btnMinimizarLogin.Size = new System.Drawing.Size(33, 31);
            this.btnMinimizarLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMinimizarLogin.TabIndex = 8;
            this.btnMinimizarLogin.TabStop = false;
            this.btnMinimizarLogin.Click += new System.EventHandler(this.btnMinimizarLogin_Click);
            // 
            // btnRestaurarLogin
            // 
            this.btnRestaurarLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestaurarLogin.Image = ((System.Drawing.Image)(resources.GetObject("btnRestaurarLogin.Image")));
            this.btnRestaurarLogin.Location = new System.Drawing.Point(1176, 15);
            this.btnRestaurarLogin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRestaurarLogin.Name = "btnRestaurarLogin";
            this.btnRestaurarLogin.Size = new System.Drawing.Size(33, 31);
            this.btnRestaurarLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnRestaurarLogin.TabIndex = 9;
            this.btnRestaurarLogin.TabStop = false;
            this.btnRestaurarLogin.Click += new System.EventHandler(this.btnRestaurarLogin_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(1267, 556);
            this.Controls.Add(this.btnRestaurarLogin);
            this.Controls.Add(this.btnMinimizarLogin);
            this.Controls.Add(this.btnMaximizarLogin);
            this.Controls.Add(this.btnCerrarLogin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lnkPass);
            this.Controls.Add(this.btnToAccess);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Login";
            this.Opacity = 0.8D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login - Cancha 5/13";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Login_MouseDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrarLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximizarLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizarLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRestaurarLogin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnToAccess;
        private System.Windows.Forms.LinkLabel lnkPass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox btnCerrarLogin;
        private System.Windows.Forms.PictureBox btnMaximizarLogin;
        private System.Windows.Forms.PictureBox btnMinimizarLogin;
        private System.Windows.Forms.PictureBox btnRestaurarLogin;
    }
}

