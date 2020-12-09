using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTestAfLandlystDBContionH2
{
    public static class HotelManager
    {
        #region Gets From DalManageer
        public static List<Room> GetRooms(string ServiceYesOrNo,string service, DateTime usrADate, DateTime usrLDate)
        {
            return DalManager.GetRooms(ServiceYesOrNo, service, usrADate, usrLDate);
        }

        public static List<TotalAmount> GetRoomPrice(int roomNo)
        {
            return DalManager.GetRoomPrice(roomNo);
        }

        public static List<TotalAmount> GetServicePrice(int roomNo)
        {
            return DalManager.GetServicesPrice(roomNo);

        }
        #endregion

        #region Gets stored procedures from DalManageer
        public static void SetGuests(string foreName, string lastName, string address, string email, int telephoneNo, int zipCode)
        {
             DalManager.SetGuests( foreName, lastName, address, email, telephoneNo, zipCode);
        }

        public static void SetBookings(DateTime checkIn, DateTime checkOut, int guestsId, int roomNo)
        {
            DalManager.SetBookings(checkIn, checkOut, guestsId, roomNo);
        }
        #endregion
    }
}
