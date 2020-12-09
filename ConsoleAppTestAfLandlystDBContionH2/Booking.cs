using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTestAfLandlystDBContionH2
{
    /// <summary>
    /// BookingNo, CheckIn, CheckOut, GuestsID
    /// </summary>
    public class Booking
    {
        public int BookingNo { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int GuestsID { get; set; }
        public int RoomNo { get; set; }

    }
}
