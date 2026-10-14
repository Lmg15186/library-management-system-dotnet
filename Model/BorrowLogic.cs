using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    // BUSINESS LOGIC LAYER for loans.
    // This is where the borrow, reserve and return rules from the brief live:
    // a book can only be borrowed if nobody has it and nobody else has reserved it,
    // and a late return is charged per day.
    public class BorrowLogic
    {
        private BorrowDAO borrowDAO = new BorrowDAO();
        private ReservedLogic reservedLogic = new ReservedLogic();
        private BookLogic bookLogic = new BookLogic();

        // The loan lasts two weeks and a late return costs one dollar per day. These
        // are constants so the rule is written once and the same value is used by the
        // borrow and by the return.
        private const int LoanPeriodDays = 14;
        private const decimal LateFeePerDay = 1.00m;

        // ActualReturnDate does not accept nulls, so a book that is still out carries
        // this date. Anything later than it means the book came back.
        private static readonly DateTime NotReturnedDate = new DateTime(2000, 1, 1);

        // ================= READ =================

        // True when the book is out on loan right now.
        public bool IsBookBorrowed(string isbn)
        {
            DataSetLibrarySystem.TabBorrowDataTable borrows = borrowDAO.GetAllBorrows();

            foreach (DataRow row in borrows.Rows)
            {
                if (row["ISBN"].ToString() == isbn
                    && (DateTime)row["ActualReturnDate"] <= NotReturnedDate)
                {
                    return true;
                }
            }

            return false;
        }

        // The books this user still has, used to fill the return drop-down.
        // The book titles are read once from BookLogic and matched by ISBN, so the
        // drop-down can show a name instead of a number.
        public List<Borrow> GetActiveBorrows(int uid)
        {
            DataSetLibrarySystem.TabBorrowDataTable borrows = borrowDAO.GetAllBorrows();
            List<Book> books = bookLogic.BrowseBooks();
            List<Borrow> activeBorrows = new List<Borrow>();

            foreach (DataRow row in borrows.Rows)
            {
                if ((int)row["UID"] != uid
                    || (DateTime)row["ActualReturnDate"] > NotReturnedDate)
                {
                    continue;
                }

                activeBorrows.Add(BuildBorrow(row, books));
            }

            return activeBorrows;
        }

        // Every loan in the system, open or already returned. Used by the reports.
        public List<Borrow> GetAllBorrows()
        {
            DataSetLibrarySystem.TabBorrowDataTable borrows = borrowDAO.GetAllBorrows();
            List<Book> books = bookLogic.BrowseBooks();
            List<Borrow> result = new List<Borrow>();

            foreach (DataRow row in borrows.Rows)
            {
                result.Add(BuildBorrow(row, books));
            }

            return result;
        }

        // Turns one TabBorrow row into a Borrow object. GetActiveBorrows and
        // GetAllBorrows both need it.
        private Borrow BuildBorrow(DataRow row, List<Book> books)
        {
            return new Borrow
            {
                BID = (int)row["BID"],
                UID = (int)row["UID"],
                ISBN = row["ISBN"].ToString(),
                BorrowDate = (DateTime)row["BorrowDate"],
                ReturnDate = (DateTime)row["ReturnDate"],
                ActualReturnDate = (DateTime)row["ActualReturnDate"],
                LateFee = (decimal)row["LateFee"],
                BookName = FindBookName(books, row["ISBN"].ToString())
            };
        }

        // Matches an ISBN to its title. Returns the ISBN itself if the book is not in
        // the list, so the drop-down never shows a blank line.
        private string FindBookName(List<Book> books, string isbn)
        {
            if (books != null)
            {
                foreach (Book book in books)
                {
                    if (book.ISBN == isbn)
                    {
                        return book.BookName;
                    }
                }
            }

            return isbn;
        }

        // ================= WRITE =================

        // Borrows a book for a user, applying the two rules from the brief:
        // the book must not already be out, and it must not be reserved by someone
        // else. If the user reserved it themselves, that reservation is removed,
        // because it has now been used.
        public int BorrowBook(int uid, string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                throw new ArgumentException("Please select a book first.");
            }

            if (IsBookBorrowed(isbn))
            {
                throw new ArgumentException("This book is already borrowed by somebody else.");
            }

            int holder = reservedLogic.GetReservationHolder(isbn);

            if (holder != ReservedLogic.NoReservation && holder != uid)
            {
                throw new ArgumentException("This book is reserved by another student.");
            }

            if (holder == uid)
            {
                int rid = reservedLogic.GetReservationId(uid, isbn);
                reservedLogic.DeleteReservation(rid);
            }

            DateTime borrowDate = DateTime.Today;
            DateTime dueDate = borrowDate.AddDays(LoanPeriodDays);

            // The loan starts with the "not returned" date and no fee. Both are filled
            // in properly when the book comes back.
            return borrowDAO.Insert(uid, isbn, borrowDate, dueDate, NotReturnedDate, 0m);
        }

        // Returns a book: writes today's date into ActualReturnDate and works out the
        // late fee. The fee is returned so the screen can tell the student what they
        // owe.
        public decimal ReturnBook(Borrow borrow)
        {
            if (borrow == null)
            {
                throw new ArgumentException("Please select a book to return.");
            }

            DateTime today = DateTime.Today;

            // Days past the due date. A book returned early or on time gives a
            // negative or zero result, which must not become a negative fee.
            int daysLate = (today - borrow.ReturnDate).Days;

            if (daysLate < 0)
            {
                daysLate = 0;
            }

            decimal lateFee = daysLate * LateFeePerDay;

            int rows = borrowDAO.Update(borrow.UID, borrow.ISBN, borrow.BorrowDate,
                                        borrow.ReturnDate, today, lateFee, borrow.BID);

            if (rows == 0)
            {
                throw new Exception("This loan record no longer exists. Please refresh and try again.");
            }

            return lateFee;
        }
    }
}