namespace GATE_GUARD
{
    partial class Form1
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
            this.gvShow = new System.Windows.Forms.DataGridView();
            this.IDS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Faculty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateTimeIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateTimeOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvShow)).BeginInit();
            this.SuspendLayout();
            // 
            // gvShow
            // 
            this.gvShow.AllowUserToAddRows = false;
            this.gvShow.AllowUserToDeleteRows = false;
            this.gvShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvShow.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDS,
            this.Name,
            this.Role,
            this.Faculty,
            this.DateTimeIn,
            this.DateTimeOut,
            this.Status,
            this.Column1});
            this.gvShow.Location = new System.Drawing.Point(233, 32);
            this.gvShow.Name = "gvShow";
            this.gvShow.ReadOnly = true;
            this.gvShow.Size = new System.Drawing.Size(647, 375);
            this.gvShow.TabIndex = 0;
            // 
            // IDS
            // 
            this.IDS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IDS.DataPropertyName = "IDS";
            this.IDS.HeaderText = "ID";
            this.IDS.Name = "IDS";
            this.IDS.ReadOnly = true;
            // 
            // Name
            // 
            this.Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Name.DataPropertyName = "Name";
            this.Name.HeaderText = "Name";
            this.Name.Name = "Name";
            this.Name.ReadOnly = true;
            // 
            // Role
            // 
            this.Role.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Role.DataPropertyName = "Role";
            this.Role.HeaderText = "Role";
            this.Role.Name = "Role";
            this.Role.ReadOnly = true;
            // 
            // Faculty
            // 
            this.Faculty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Faculty.DataPropertyName = "Faculty";
            this.Faculty.HeaderText = "Faculty";
            this.Faculty.Name = "Faculty";
            this.Faculty.ReadOnly = true;
            // 
            // DateTimeIn
            // 
            this.DateTimeIn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DateTimeIn.DataPropertyName = "DateTimeIn";
            this.DateTimeIn.HeaderText = "Time In";
            this.DateTimeIn.Name = "DateTimeIn";
            this.DateTimeIn.ReadOnly = true;
            // 
            // DateTimeOut
            // 
            this.DateTimeOut.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DateTimeOut.DataPropertyName = "DateTimeOut";
            this.DateTimeOut.HeaderText = "Time Out";
            this.DateTimeOut.Name = "DateTimeOut";
            this.DateTimeOut.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 50F;
            this.Column1.HeaderText = "Confirm";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Text = "OK";
            this.Column1.Width = 50;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 419);
            this.Controls.Add(this.gvShow);
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.gvShow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gvShow;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Role;
        private System.Windows.Forms.DataGridViewTextBoxColumn Faculty;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateTimeIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateTimeOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
    }
}

