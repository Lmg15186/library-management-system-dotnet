using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    // BUSINESS LOGIC LAYER for reservations.
    // Holds the rules about who may reserve a book and finds an existing reservation.
    public class ReservedLogic
    {
        private ReservedDAO reservedDAO = new ReservedDAO();
        private BookLogic bookLogic = new BookLogic();

        // No user has UID 0, so 0 is used to mean "nobody has reserved this book".
        public const int NoReservation = 0;

        // Returns the UID of whoever reserved this book, or 0 when it is free.
        // BorrowLogic uses this to apply the rule from the brief: a book can only be
        // borrowed if it is not reserved by someone else.
        public int GetReservationHolder(string isbn)
        {
            DataSetLibrarySystem.TabReservedDataTable reservations = reservedDAO.GetAllReservations();

            foreach (DataRow row in reservations.Rows)
            {
                if (row["ISBN"].ToString() == isbn)
                {
                    return (int)row["UID"];
                }
            }

            return NoReservation;
        }

        // Returns the RID of this user's reservation for this book, or 0 if there is
        // none. The RID is needed to delete the reservation when the book is borrowed.
        public int GetReservationId(int uid, string isbn)
        {
            DataSetLibrarySystem.TabReservedDataTable reservations = reservedDAO.GetAllReservations();

            foreach (DataRow row in reservations.Rows)
            {
                if (row["ISBN"].ToString() == isbn && (int)row["UID"] == uid)
                {
                    return (int)row["RID"];
                }
            }

            return NoReservation;
        }

        // Reserves a book for a user. The reservation date is today's date.
        public int ReserveBook(int uid, string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                throw new ArgumentException("Please select a book first.");
            }

            int holder = GetReservationHolder(isbn);

            if (holder == uid)
            {
                throw new ArgumentException("You have already reserved this book.");
            }

            if (holder != NoReservation)
            {
                throw new ArgumentException("This book is already reserved by another student.");
            }

            return reservedDAO.Insert(uid, isbn, DateTime.Today);
        }

        public int DeleteReservation(int rid)
        {
            return reservedDAO.Delete(rid);
        }

        // Every reservation, used by the reservations report. The book titles are read
        // once and matched by ISBN, so the report shows a name and not just a number.
        public List<Reserved> GetAllReservations()
        {
            DataSetLibrarySystem.TabReservedDataTable reservations = reservedDAO.GetAllReservations();
            List<Book> books = bookLogic.BrowseBooks();
            List<Reserved> result = new List<Reserved>();

            foreach (DataRow row in reservations.Rows)
            {
                Reserved reserved = new Reserved
                {
                    RID = (int)row["RID"],
                    UID = (int)row["UID"],
                    ISBN = row["ISBN"].ToString(),
                    ReservedDate = (DateTime)row["ReservedDate"],
                    BookName = FindBookName(books, row["ISBN"].ToString())
                };

                result.Add(reserved);
            }

            return result;
        }

        // Matches an ISBN to its title, returning the ISBN when the book is not found
        // so the report never shows a blank line.
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
    }
}