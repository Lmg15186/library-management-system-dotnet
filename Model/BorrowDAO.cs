using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataSetLibrarySystemTableAdapters;

namespace Model
{
    // DATA ACCESS LAYER for loans (TabBorrow).
    // It only reads and writes rows. Deciding whether a book can be borrowed, or how
    // much the late fee is, belongs to BorrowLogic.
    public class BorrowDAO
    {
        private TabBorrowTableAdapter tabBorrowTableAdapter = new TabBorrowTableAdapter();

        // Returns every loan. The DataSet has no filtered query for TabBorrow, so the
        // filtering by user or by book is done in the logic layer over this table.
        public DataSetLibrarySystem.TabBorrowDataTable GetAllBorrows()
        {
            return tabBorrowTableAdapter.GetData();
        }

        // No BID here because it is an identity column.
        //     INSERT INTO [TabBorrow] (UID, ISBN, BorrowDate, ReturnDate,
        //         ActualReturnDate, LateFee) VALUES (...)
        public int Insert(int uid, string isbn, DateTime borrowDate, DateTime returnDate,
                          DateTime actualReturnDate, decimal lateFee)
        {
            return tabBorrowTableAdapter.Insert(uid, isbn, borrowDate, returnDate,
                                                actualReturnDate, lateFee);
        }

        // Used by the return: the same row is written back with the real return date
        // and the fee. The BID at the end is what finds the row.
        public int Update(int uid, string isbn, DateTime borrowDate, DateTime returnDate,
                          DateTime actualReturnDate, decimal lateFee, int originalBid)
        {
            return tabBorrowTableAdapter.Update(uid, isbn, borrowDate, returnDate,
                                                actualReturnDate, lateFee, originalBid);
        }
    }
}