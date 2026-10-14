using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    // CONTROLLER LAYER for reservations.
    public class ReservedController
    {
        private ReservedLogic reservedLogic = new ReservedLogic();

        public int ReserveBook(int uid, string isbn)
        {
            return reservedLogic.ReserveBook(uid, isbn);
        }

        // Used by the reservations report.
        public List<Reserved> GetAllReservations()
        {
            return reservedLogic.GetAllReservations();
        }
    }
}