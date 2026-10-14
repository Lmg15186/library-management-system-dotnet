namespace View
{
    partial class FormLanguage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        // Fields and buttons at the top, data grid across the bottom. Same layout and
        // AIT teal as the Category screen, so every maintenance form looks the same.
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.dgvLanguages = new System.Windows.Forms.DataGridView();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.lblLID = new System.Windows.Forms.Label();
            this.txtLID = new System.Windows.Forms.TextBox();
            this.lblLanguageName = new System.Windows.Forms.Label();
            this.txtLanguageName = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLanguages)).BeginInit();
            this.grpDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(179)))));
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(640, 55);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(20, 14);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(249, 30);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Language Maintenance";
            // 
            // grpDetails
            // 
            this.grpDetails.Controls.Add(this.lblLID);
            this.grpDetails.Controls.Add(this.txtLID);
            this.grpDetails.Controls.Add(this.lblLanguageName);
            this.grpDetails.Controls.Add(this.txtLanguageName);
            this.grpDetails.Controls.Add(this.btnAdd);
            this.grpDetails.Controls.Add(this.btnUpdate);
            this.grpDetails.Controls.Add(this.btnDelete);
            this.grpDetails.Controls.Add(this.btnClear);
            this.grpDetails.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.grpDetails.Location = new System.Drawing.Point(20, 70);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(600, 130);
            this.grpDetails.TabIndex = 1;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Language Details";
            // 
            // lblLID
            // 
            this.lblLID.AutoSize = true;
            this.lblLID.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLID.Location = new System.Drawing.Point(20, 35);
            this.lblLID.Name = "lblLID";
            this.lblLID.Size = new System.Drawing.Size(27, 20);
            this.lblLID.TabIndex = 0;
            this.lblLID.Text = "ID:";
            // 
            // txtLID
            // 
            this.txtLID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.txtLID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLID.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtLID.Location = new System.Drawing.Point(140, 32);
            this.txtLID.Name = "txtLID";
            this.txtLID.ReadOnly = true;
            this.txtLID.Size = new System.Drawing.Size(120, 27);
            this.txtLID.TabIndex = 1;
            // 
            // lblLanguageName
            // 
            this.lblLanguageName.AutoSize = true;
            this.lblLanguageName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLanguageName.Location = new System.Drawing.Point(300, 35);
            this.lblLanguageName.Name = "lblLanguageName";
            this.lblLanguageName.Size = new System.Drawing.Size(116, 20);
            this.lblLanguageName.TabIndex = 2;
            this.lblLanguageName.Text = "Language Name:";
            // 
            // txtLanguageName
            // 
            this.txtLanguageName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLanguageName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtLanguageName.Location = new System.Drawing.Point(422, 32);
            this.txtLanguageName.Name = "txtLanguageName";
            this.txtLanguageName.Size = new System.Drawing.Size(150, 27);
            this.txtLanguageName.TabIndex = 3;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(179)))));
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(20, 85);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(125, 30);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(179)))));
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(155, 85);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(125, 30);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(58)))), ((int)(((byte)(46)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(290, 85);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(125, 30);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.White;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.btnClear.Location = new System.Drawing.Point(425, 85);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(125, 30);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dgvLanguages
            // 
            this.dgvLanguages.AllowUserToAddRows = false;
            this.dgvLanguages.AllowUserToDeleteRows = false;
            this.dgvLanguages.AllowUserToResizeRows = false;
            this.dgvLanguages.BackgroundColor = System.Drawing.Color.White;
            this.dgvLanguages.ColumnHeadersHeight = 29;
            this.dgvLanguages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLanguages.Location = new System.Drawing.Point(20, 215);
            this.dgvLanguages.MultiSelect = false;
            this.dgvLanguages.Name = "dgvLanguages";
            this.dgvLanguages.ReadOnly = true;
            this.dgvLanguages.RowHeadersVisible = false;
            this.dgvLanguages.RowHeadersWidth = 51;
            this.dgvLanguages.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLanguages.Size = new System.Drawing.Size(600, 220);
            this.dgvLanguages.TabIndex = 2;
            this.dgvLanguages.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLanguages_CellClick);
            // 
            // FormLanguage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.ClientSize = new System.Drawing.Size(640, 455);
            this.Controls.Add(this.dgvLanguages);
            this.Controls.Add(this.grpDetails);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormLanguage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Language Maintenance";
            this.Load += new System.EventHandler(this.FormLanguage_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLanguages)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.DataGridView dgvLanguages;
        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.Label lblLID;
        private System.Windows.Forms.TextBox txtLID;
        private System.Windows.Forms.Label lblLanguageName;
        private System.Windows.Forms.TextBox txtLanguageName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
    }
}