namespace View
{
    partial class FormBook
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

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblBookName = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.txtBookName = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.btnSearchName = new System.Windows.Forms.Button();
            this.btnSearchAuthor = new System.Windows.Forms.Button();
            this.btnSearchCategory = new System.Windows.Forms.Button();
            this.btnBrowseAll = new System.Windows.Forms.Button();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.btnBorrow = new System.Windows.Forms.Button();
            this.btnReserve = new System.Windows.Forms.Button();
            this.lblBorrowed = new System.Windows.Forms.Label();
            this.cmbBorrowed = new System.Windows.Forms.ComboBox();
            this.btnReturn = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(0, 170, 179);
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(820, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(25, 16);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(180, 25);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Book Search and Browse";
            // 
            // lblBookName
            // 
            this.lblBookName.AutoSize = true;
            this.lblBookName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBookName.ForeColor = System.Drawing.Color.FromArgb(44, 42, 60);
            this.lblBookName.Location = new System.Drawing.Point(30, 85);
            this.lblBookName.Name = "lblBookName";
            this.lblBookName.Size = new System.Drawing.Size(110, 19);
            this.lblBookName.TabIndex = 1;
            this.lblBookName.Text = "Search by Name:";
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAuthor.ForeColor = System.Drawing.Color.FromArgb(44, 42, 60);
            this.lblAuthor.Location = new System.Drawing.Point(30, 125);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(118, 19);
            this.lblAuthor.TabIndex = 2;
            this.lblAuthor.Text = "Search by Author:";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(44, 42, 60);
            this.lblCategory.Location = new System.Drawing.Point(30, 165);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(128, 19);
            this.lblCategory.TabIndex = 3;
            this.lblCategory.Text = "Search by Category:";
            // 
            // txtBookName
            // 
            this.txtBookName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBookName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBookName.Location = new System.Drawing.Point(200, 82);
            this.txtBookName.Name = "txtBookName";
            this.txtBookName.Size = new System.Drawing.Size(200, 25);
            this.txtBookName.TabIndex = 4;
            // 
            // txtAuthor
            // 
            this.txtAuthor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAuthor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAuthor.Location = new System.Drawing.Point(200, 122);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(200, 25);
            this.txtAuthor.TabIndex = 5;
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(200, 162);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(200, 25);
            this.cmbCategory.TabIndex = 6;
            // 
            // btnSearchName
            // 
            this.btnSearchName.BackColor = System.Drawing.Color.FromArgb(0, 170, 179);
            this.btnSearchName.FlatAppearance.BorderSize = 0;
            this.btnSearchName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchName.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSearchName.ForeColor = System.Drawing.Color.White;
            this.btnSearchName.Location = new System.Drawing.Point(420, 80);
            this.btnSearchName.Name = "btnSearchName";
            this.btnSearchName.Size = new System.Drawing.Size(180, 30);
            this.btnSearchName.TabIndex = 7;
            this.btnSearchName.Text = "Search by Name";
            this.btnSearchName.UseVisualStyleBackColor = false;
            this.btnSearchName.Click += new System.EventHandler(this.btnSearchName_Click);
            // 
            // btnSearchAuthor
            // 
            this.btnSearchAuthor.BackColor = System.Drawing.Color.FromArgb(0, 170, 179);
            this.btnSearchAuthor.FlatAppearance.BorderSize = 0;
            this.btnSearchAuthor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchAuthor.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSearchAuthor.ForeColor = System.Drawing.Color.White;
            this.btnSearchAuthor.Location = new System.Drawing.Point(420, 120);
            this.btnSearchAuthor.Name = "btnSearchAuthor";
            this.btnSearchAuthor.Size = new System.Drawing.Size(180, 30);
            this.btnSearchAuthor.TabIndex = 8;
            this.btnSearchAuthor.Text = "Search by Author";
            this.btnSearchAuthor.UseVisualStyleBackColor = false;
            this.btnSearchAuthor.Click += new System.EventHandler(this.btnSearchAuthor_Click);
            // 
            // btnSearchCategory
            // 
            this.btnSearchCategory.BackColor = System.Drawing.Color.FromArgb(0, 170, 179);
            this.btnSearchCategory.FlatAppearance.BorderSize = 0;
            this.btnSearchCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchCategory.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSearchCategory.ForeColor = System.Drawing.Color.White;
            this.btnSearchCategory.Location = new System.Drawing.Point(420, 160);
            this.btnSearchCategory.Name = "btnSearchCategory";
            this.btnSearchCategory.Size = new System.Drawing.Size(180, 30);
            this.btnSearchCategory.TabIndex = 9;
            this.btnSearchCategory.Text = "Search by Category";
            this.btnSearchCategory.UseVisualStyleBackColor = false;
            this.btnSearchCategory.Click += new System.EventHandler(this.btnSearchCategory_Click);
            // 
            // btnBrowseAll
            // 
            this.btnBrowseAll.BackColor = System.Drawing.Color.FromArgb(0, 184, 184);
            this.btnBrowseAll.FlatAppearance.BorderSize = 0;
            this.btnBrowseAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseAll.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnBrowseAll.ForeColor = System.Drawing.Color.White;
            this.btnBrowseAll.Location = new System.Drawing.Point(200, 200);
            this.btnBrowseAll.Name = "btnBrowseAll";
            this.btnBrowseAll.Size = new System.Drawing.Size(200, 32);
            this.btnBrowseAll.TabIndex = 10;
            this.btnBrowseAll.Text = "Browse All Books";
            this.btnBrowseAll.UseVisualStyleBackColor = false;
            this.btnBrowseAll.Click += new System.EventHandler(this.btnBrowseAll_Click);
            // 
            // dgvBooks
            // 
            this.dgvBooks.AllowUserToAddRows = false;
            this.dgvBooks.AllowUserToDeleteRows = false;
            this.dgvBooks.BackgroundColor = System.Drawing.Color.White;
            this.dgvBooks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBooks.Location = new System.Drawing.Point(30, 245);
            this.dgvBooks.MultiSelect = false;
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.ReadOnly = true;
            this.dgvBooks.RowHeadersWidth = 51;
            this.dgvBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBooks.Size = new System.Drawing.Size(760, 250);
            this.dgvBooks.TabIndex = 11;
            // 
            // btnBorrow
            // 
            this.btnBorrow.BackColor = System.Drawing.Color.FromArgb(0, 170, 179);
            this.btnBorrow.FlatAppearance.BorderSize = 0;
            this.btnBorrow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrow.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnBorrow.ForeColor = System.Drawing.Color.White;
            this.btnBorrow.Location = new System.Drawing.Point(30, 510);
            this.btnBorrow.Name = "btnBorrow";
            this.btnBorrow.Size = new System.Drawing.Size(170, 35);
            this.btnBorrow.TabIndex = 12;
            this.btnBorrow.Text = "Borrow Selected Book";
            this.btnBorrow.UseVisualStyleBackColor = false;
            this.btnBorrow.Click += new System.EventHandler(this.btnBorrow_Click);
            // 
            // btnReserve
            // 
            this.btnReserve.BackColor = System.Drawing.Color.FromArgb(0, 184, 184);
            this.btnReserve.FlatAppearance.BorderSize = 0;
            this.btnReserve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReserve.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnReserve.ForeColor = System.Drawing.Color.White;
            this.btnReserve.Location = new System.Drawing.Point(215, 510);
            this.btnReserve.Name = "btnReserve";
            this.btnReserve.Size = new System.Drawing.Size(170, 35);
            this.btnReserve.TabIndex = 13;
            this.btnReserve.Text = "Reserve Selected Book";
            this.btnReserve.UseVisualStyleBackColor = false;
            this.btnReserve.Click += new System.EventHandler(this.btnReserve_Click);
            // 
            // lblBorrowed
            // 
            this.lblBorrowed.AutoSize = true;
            this.lblBorrowed.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBorrowed.ForeColor = System.Drawing.Color.FromArgb(44, 42, 60);
            this.lblBorrowed.Location = new System.Drawing.Point(30, 565);
            this.lblBorrowed.Name = "lblBorrowed";
            this.lblBorrowed.Size = new System.Drawing.Size(155, 19);
            this.lblBorrowed.TabIndex = 14;
            this.lblBorrowed.Text = "My borrowed books:";
            // 
            // cmbBorrowed
            // 
            this.cmbBorrowed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBorrowed.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbBorrowed.FormattingEnabled = true;
            this.cmbBorrowed.Location = new System.Drawing.Point(30, 590);
            this.cmbBorrowed.Name = "cmbBorrowed";
            this.cmbBorrowed.Size = new System.Drawing.Size(400, 25);
            this.cmbBorrowed.TabIndex = 15;
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.FromArgb(0, 170, 179);
            this.btnReturn.FlatAppearance.BorderSize = 0;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(450, 587);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(170, 32);
            this.btnReturn.TabIndex = 16;
            this.btnReturn.Text = "Return Book";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // FormBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(239, 239, 239);
            this.ClientSize = new System.Drawing.Size(820, 650);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.cmbBorrowed);
            this.Controls.Add(this.lblBorrowed);
            this.Controls.Add(this.btnReserve);
            this.Controls.Add(this.btnBorrow);
            this.Controls.Add(this.dgvBooks);
            this.Controls.Add(this.btnBrowseAll);
            this.Controls.Add(this.btnSearchCategory);
            this.Controls.Add(this.btnSearchAuthor);
            this.Controls.Add(this.btnSearchName);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.txtBookName);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.lblBookName);
            this.Controls.Add(this.pnlHeader);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "FormBook";
            this.Text = "Library System - Books";
            this.Load += new System.EventHandler(this.FormBook_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblBookName;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.TextBox txtBookName;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Button btnSearchName;
        private System.Windows.Forms.Button btnSearchAuthor;
        private System.Windows.Forms.Button btnSearchCategory;
        private System.Windows.Forms.Button btnBrowseAll;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.Button btnBorrow;
        private System.Windows.Forms.Button btnReserve;
        private System.Windows.Forms.Label lblBorrowed;
        private System.Windows.Forms.ComboBox cmbBorrowed;
        private System.Windows.Forms.Button btnReturn;
    }
}