using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace View
{
    // LIBRARY REPORTS SCREEN (staff and admin).
    // Three reports, each rendered by a ReportViewer from its own .rdlc definition:
    //   1. every borrowed book,
    //   2. the books that are overdue, with the late fee owed,
    //   3. the active reservations.
    // The data comes from LoanService. A report only lists records, so nothing here
    // changes the database.
    public partial class FormReports : Form
    {
        private LoanServiceRef.LoanService loanService = new LoanServiceRef.LoanService();

        // A book still out carries the sentinel date 1/1/2000 in ActualReturnDate,
        // because the column does not accept nulls. Comparing against the start of
        // 2001 keeps the check away from that exact value.
        private static readonly DateTime NotReturnedThreshold = new DateTime(2001, 1, 1);

        public FormReports()
        {
            InitializeComponent();
        }

        private void FormReports_Load(object sender, EventArgs e)
        {
            LoadBorrowed();
            LoadOverdue();
            LoadReserved();
        }

        // Fills one ReportViewer: points it at the .rdlc file, hands it the table, and
        // renders. The dataset name has to match the one declared inside the .rdlc,
        // which is how the report knows where its fields come from.
        private void ShowReport(ReportViewer viewer, string reportFile, string datasetName, DataTable data)
        {
            viewer.Reset();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = "Reports\\" + reportFile;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(new ReportDataSource(datasetName, data));
            viewer.RefreshReport();
        }

        // ---------- Report 1: all borrowed books ----------
        private void LoadBorrowed()
        {
            try
            {
                ShowReport(rvBorrowed, "BorrowedReport.rdlc", "BorrowedDataSet",
                           loanService.GetAllBorrows());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading borrowed report: " + ex.Message);
            }
        }

        // ---------- Report 2: overdue books + late fee ----------
        // Of the books still out, the overdue ones are those whose expected ReturnDate
        // has already passed. The fee shown is $1 per day late, the same rate the
        // return itself charges.
        private void LoadOverdue()
        {
            try
            {
                DataTable borrows = loanService.GetAllBorrows();

                // A separate table just for the report, so DaysLate can sit next to
                // the fee.
                DataTable overdue = new DataTable();
                overdue.Columns.Add("BID", typeof(int));
                overdue.Columns.Add("UID", typeof(int));
                overdue.Columns.Add("ISBN", typeof(string));
                overdue.Columns.Add("BookName", typeof(string));
                overdue.Columns.Add("BorrowDate", typeof(DateTime));
                overdue.Columns.Add("ReturnDate", typeof(DateTime));
                overdue.Columns.Add("DaysLate", typeof(int));
                overdue.Columns.Add("LateFee", typeof(decimal));

                foreach (DataRow row in borrows.Rows)
                {
                    DateTime actualReturn = (DateTime)row["ActualReturnDate"];
                    DateTime dueDate = (DateTime)row["ReturnDate"];

                    if (actualReturn < NotReturnedThreshold && dueDate < DateTime.Today)
                    {
                        int daysLate = (DateTime.Today - dueDate).Days;
                        decimal fee = daysLate;  // $1 per day overdue

                        overdue.Rows.Add(row["BID"], row["UID"], row["ISBN"], row["BookName"],
                                         row["BorrowDate"], dueDate, daysLate, fee);
                    }
                }

                ShowReport(rvOverdue, "OverdueReport.rdlc", "OverdueDataSet", overdue);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading overdue report: " + ex.Message);
            }
        }

        // ---------- Report 3: active reservations ----------
        private void LoadReserved()
        {
            try
            {
                ShowReport(rvReserved, "ReservedReport.rdlc", "ReservedDataSet",
                           loanService.GetAllReservations());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading reservations report: " + ex.Message);
            }
        }
    }
}