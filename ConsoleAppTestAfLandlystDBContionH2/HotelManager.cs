using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTestAfLandlystDBContionH2
{
    public static class HotelManager
    {
        public static List<Room> GetRooms(string ServiceYesOrNo,string service)
        {
            return DalManager.GetRooms(ServiceYesOrNo, service);
        }

        public static List<Booking> GetBookings()
        {
            return DalManager.GetBookings();
        }

        public static List<TotalAmount> GetRoomPrice(int roomNo)
        {
            return DalManager.GetRoomPrice(roomNo);
        }

        public static List<TotalAmount> GetServicePrice(int roomNo)
        {
            return DalManager.GetServicesPrice(roomNo);

        }

        //public static List<RoomQuantity> SetRoomQuantities(int bookingNo, int roomNo)
        //{
        //    return DalManager.SetRoomQuantity(bookingNo, roomNo);

        //}
    }
}
