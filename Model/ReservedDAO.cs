using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataSetLibrarySystemTableAdapters;

namespace Model
{
    // DATA ACCESS LAYER for reservations (TabReserved).
    public class ReservedDAO
    {
        private TabReservedTableAdapter tabReservedTableAdapter = new TabReservedTableAdapter();

        public DataSetLibrarySystem.TabReservedDataTable GetAllReservations()
        {
            return tabReservedTableAdapter.GetData();
        }

        // RID is an identity column, so it is not sent.
        public int Insert(int uid, string isbn, DateTime reservedDate)
        {
            return tabReservedTableAdapter.Insert(uid, isbn, reservedDate);
        }

        // A reservation is removed when the same student borrows the book.
        public int Delete(int originalRid)
        {
            return tabReservedTableAdapter.Delete(originalRid);
        }
    }
}