namespace View
{
    partial class FormReports
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

        // Three report grids stacked with a label above each: borrowed books,
        // overdue books with their late fee, and active reservations. Same AIT teal
        // header as the rest of the application.
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblBorrowed = new System.Windows.Forms.Label();
            this.rvBorrowed = new Microsoft.Reporting.WinForms.ReportViewer();
            this.lblOverdue = new System.Windows.Forms.Label();
            this.rvOverdue = new Microsoft.Reporting.WinForms.ReportViewer();
            this.lblReserved = new System.Windows.Forms.Label();
            this.rvReserved = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(179)))));
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(700, 55);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(20, 14);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(150, 30);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Library Reports";
            // 
            // lblBorrowed
            // 
            this.lblBorrowed.AutoSize = true;
            this.lblBorrowed.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBorrowed.Location = new System.Drawing.Point(20, 70);
            this.lblBorrowed.Name = "lblBorrowed";
            this.lblBorrowed.Size = new System.Drawing.Size(180, 23);
            this.lblBorrowed.TabIndex = 1;
            this.lblBorrowed.Text = "Report 1: Borrowed Books";
            // 
            // rvBorrowed
            // 
            this.rvBorrowed.Location = new System.Drawing.Point(20, 98);
            this.rvBorrowed.Name = "rvBorrowed";
            this.rvBorrowed.Size = new System.Drawing.Size(660, 165);
            this.rvBorrowed.TabIndex = 2;
            // 
            // lblOverdue
            // 
            this.lblOverdue.AutoSize = true;
            this.lblOverdue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblOverdue.Location = new System.Drawing.Point(20, 280);
            this.lblOverdue.Name = "lblOverdue";
            this.lblOverdue.Size = new System.Drawing.Size(260, 23);
            this.lblOverdue.TabIndex = 3;
            this.lblOverdue.Text = "Report 2: Overdue Books + Late Fee";
            // 
            // rvOverdue
            // 
            this.rvOverdue.Location = new System.Drawing.Point(20, 308);
            this.rvOverdue.Name = "rvOverdue";
            this.rvOverdue.Size = new System.Drawing.Size(660, 165);
            this.rvOverdue.TabIndex = 4;
            // 
            // lblReserved
            // 
            this.lblReserved.AutoSize = true;
            this.lblReserved.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblReserved.Location = new System.Drawing.Point(20, 490);
            this.lblReserved.Name = "lblReserved";
            this.lblReserved.Size = new System.Drawing.Size(210, 23);
            this.lblReserved.TabIndex = 5;
            this.lblReserved.Text = "Report 3: Active Reservations";
            // 
            // rvReserved
            // 
            this.rvReserved.Location = new System.Drawing.Point(20, 518);
            this.rvReserved.Name = "rvReserved";
            this.rvReserved.Size = new System.Drawing.Size(660, 165);
            this.rvReserved.TabIndex = 6;
            // 
            // FormReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.ClientSize = new System.Drawing.Size(700, 705);
            this.Controls.Add(this.rvReserved);
            this.Controls.Add(this.lblReserved);
            this.Controls.Add(this.rvOverdue);
            this.Controls.Add(this.lblOverdue);
            this.Controls.Add(this.rvBorrowed);
            this.Controls.Add(this.lblBorrowed);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Library Reports";
            this.Load += new System.EventHandler(this.FormReports_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblBorrowed;
        private Microsoft.Reporting.WinForms.ReportViewer rvBorrowed;
        private System.Windows.Forms.Label lblOverdue;
        private Microsoft.Reporting.WinForms.ReportViewer rvOverdue;
        private System.Windows.Forms.Label lblReserved;
        private Microsoft.Reporting.WinForms.ReportViewer rvReserved;
    }
}