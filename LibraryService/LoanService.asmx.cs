using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;
using Controller;
using Model;

namespace LibraryService
{
    // WEB SERVICE for the loans: borrow, reserve, return, and the data the three
    // reports need. Basic types go in, a DataTable or a number comes back.
    [WebService(Namespace = "http://ait.edu.au/library/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class LoanService : System.Web.Services.WebService
    {
        private BorrowController borrowController = new BorrowController();
        private ReservedController reservedController = new ReservedController();

        // Column names match the Borrow properties, so the grids read the same way as
        // before the conversion.
        private DataTable CreateBorrowTable()
        {
            DataTable table = new DataTable("Borrows");
            table.Columns.Add("BID", typeof(int));
            table.Columns.Add("UID", typeof(int));
            table.Columns.Add("ISBN", typeof(string));
            table.Columns.Add("BookName", typeof(string));
            table.Columns.Add("BorrowDate", typeof(DateTime));
            table.Columns.Add("ReturnDate", typeof(DateTime));
            table.Columns.Add("ActualReturnDate", typeof(DateTime));
            table.Columns.Add("LateFee", typeof(decimal));
            return table;
        }

        private DataTable ToBorrowTable(List<Borrow> borrows)
        {
            DataTable table = CreateBorrowTable();

            if (borrows == null)
            {
                return table;
            }

            foreach (Borrow borrow in borrows)
            {
                table.Rows.Add(borrow.BID, borrow.UID, borrow.ISBN, borrow.BookName,
                               borrow.BorrowDate, borrow.ReturnDate,
                               borrow.ActualReturnDate, borrow.LateFee);
            }

            return table;
        }

        // ---------- SELECT ----------

        [WebMethod(Description = "Returns the books this user has not returned yet.")]
        public DataTable GetActiveBorrows(int uid)
        {
            return ToBorrowTable(borrowController.GetActiveBorrows(uid));
        }

        [WebMethod(Description = "Returns every loan, open or already returned.")]
        public DataTable GetAllBorrows()
        {
            return ToBorrowTable(borrowController.GetAllBorrows());
        }

        [WebMethod(Description = "Returns every reservation.")]
        public DataTable GetAllReservations()
        {
            DataTable table = new DataTable("Reservations");
            table.Columns.Add("RID", typeof(int));
            table.Columns.Add("UID", typeof(int));
            table.Columns.Add("ISBN", typeof(string));
            table.Columns.Add("BookName", typeof(string));
            table.Columns.Add("ReservedDate", typeof(DateTime));

            List<Reserved> reservations = reservedController.GetAllReservations();

            if (reservations != null)
            {
                foreach (Reserved reserved in reservations)
                {
                    table.Rows.Add(reserved.RID, reserved.UID, reserved.ISBN,
                                   reserved.BookName, reserved.ReservedDate);
                }
            }

            return table;
        }

        // ---------- ACTIONS ----------

        [WebMethod(Description = "Borrows a book for a user.")]
        public int BorrowBook(int uid, string isbn)
        {
            return borrowController.BorrowBook(uid, isbn);
        }

        [WebMethod(Description = "Reserves a book for a user.")]
        public int ReserveBook(int uid, string isbn)
        {
            return reservedController.ReserveBook(uid, isbn);
        }

        // Returns the late fee charged, so the screen can tell the student what is owed.
        //
        // Only the BID travels from the client, because a Borrow object cannot cross a
        // web service. The loan itself is found again here, among the books this user
        // still has out.
        [WebMethod(Description = "Returns a borrowed book and charges the late fee.")]
        public decimal ReturnBook(int uid, int bid)
        {
            List<Borrow> activeBorrows = borrowController.GetActiveBorrows(uid);

            if (activeBorrows != null)
            {
                foreach (Borrow borrow in activeBorrows)
                {
                    if (borrow.BID == bid)
                    {
                        return borrowController.ReturnBook(borrow);
                    }
                }
            }

            throw new ArgumentException("That loan was not found. Please refresh the list.");
        }
    }
}