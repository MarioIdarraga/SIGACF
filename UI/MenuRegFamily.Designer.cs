namespace UI
{
    partial class MenuRegFamily
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnRegFamily = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btnFindFamily = new System.Windows.Forms.Button();
            this.checkedListPatent = new System.Windows.Forms.CheckedListBox();
            this.listBoxAdd = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label10.Location = new System.Drawing.Point(413, 187);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(214, 32);
            this.label10.TabIndex = 85;
            this.label10.Text = "Cargar Familia";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(266, 272);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(186, 22);
            this.txtName.TabIndex = 82;
            // 
            // btnRegFamily
            // 
            this.btnRegFamily.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegFamily.Location = new System.Drawing.Point(324, 558);
            this.btnRegFamily.Name = "btnRegFamily";
            this.btnRegFamily.Size = new System.Drawing.Size(162, 42);
            this.btnRegFamily.TabIndex = 81;
            this.btnRegFamily.Text = "Cargar Familia";
            this.btnRegFamily.UseVisualStyleBackColor = true;
            this.btnRegFamily.Click += new System.EventHandler(this.btnRegFamily_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label14.Location = new System.Drawing.Point(100, 332);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(157, 18);
            this.label14.TabIndex = 80;
            this.label14.Text = "Listado de Patentes";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label18.Location = new System.Drawing.Point(100, 276);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(68, 18);
            this.label18.TabIndex = 77;
            this.label18.Text = "Nombre";
            // 
            // btnFindFamily
            // 
            this.btnFindFamily.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnFindFamily.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindFamily.Location = new System.Drawing.Point(38, 35);
            this.btnFindFamily.Name = "btnFindFamily";
            this.btnFindFamily.Size = new System.Drawing.Size(178, 87);
            this.btnFindFamily.TabIndex = 76;
            this.btnFindFamily.Text = "Búsqueda de Familias";
            this.btnFindFamily.UseVisualStyleBackColor = false;
            this.btnFindFamily.Click += new System.EventHandler(this.btnFindFamily_Click);
            // 
            // checkedListPatent
            // 
            this.checkedListPatent.FormattingEnabled = true;
            this.checkedListPatent.Location = new System.Drawing.Point(266, 332);
            this.checkedListPatent.Name = "checkedListPatent";
            this.checkedListPatent.Size = new System.Drawing.Size(282, 208);
            this.checkedListPatent.TabIndex = 87;
            // 
            // listBoxAdd
            // 
            this.listBoxAdd.FormattingEnabled = true;
            this.listBoxAdd.ItemHeight = 16;
            this.listBoxAdd.Location = new System.Drawing.Point(569, 332);
            this.listBoxAdd.Name = "listBoxAdd";
            this.listBoxAdd.Size = new System.Drawing.Size(398, 132);
            this.listBoxAdd.TabIndex = 88;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(569, 470);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(162, 42);
            this.btnAdd.TabIndex = 89;
            this.btnAdd.Text = "Agregar";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(805, 470);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(162, 42);
            this.btnRemove.TabIndex = 90;
            this.btnRemove.Text = "Quitar";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // MenuRegFamily
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(62)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(995, 668);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.listBoxAdd);
            this.Controls.Add(this.checkedListPatent);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnRegFamily);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.btnFindFamily);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuRegFamily";
            this.Text = "MenuRegFamily";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnRegFamily;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnFindFamily;
        private System.Windows.Forms.CheckedListBox checkedListPatent;
        private System.Windows.Forms.ListBox listBoxAdd;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
    }
}