using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    // CONTROLLER LAYER for loans. The book screen calls only this class, and it
    // forwards everything to BorrowLogic.
    public class BorrowController
    {
        private BorrowLogic borrowLogic = new BorrowLogic();

        // Used to enable or explain the Borrow button.
        public bool IsBookBorrowed(string isbn)
        {
            return borrowLogic.IsBookBorrowed(isbn);
        }

        // The books this user still has out. Fills the return drop-down.
        public List<Borrow> GetActiveBorrows(int uid)
        {
            return borrowLogic.GetActiveBorrows(uid);
        }

        // Every loan, open or returned. Used by the reports.
        public List<Borrow> GetAllBorrows()
        {
            return borrowLogic.GetAllBorrows();
        }

        public int BorrowBook(int uid, string isbn)
        {
            return borrowLogic.BorrowBook(uid, isbn);
        }

        // Returns the late fee charged, so the form can show it.
        public decimal ReturnBook(Borrow borrow)
        {
            return borrowLogic.ReturnBook(borrow);
        }
    }
}