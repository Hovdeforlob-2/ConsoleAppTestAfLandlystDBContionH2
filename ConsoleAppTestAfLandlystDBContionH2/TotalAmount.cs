using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTestAfLandlystDBContionH2
{
    public class TotalAmount
    {

        public int RoomPrice { get; set; }
        public int ServicePrice { get; set; }

        public int CalculateAmount(int roomNumber)
        {
            HotelManager.GetRoomPrice(100);

            int servicesPrice = 0;
            foreach (TotalAmount item in HotelManager.GetServicePrice(roomNumber))
            {
                servicesPrice = servicesPrice + item.ServicePrice;
            }

            int roomPrice = 0;
            foreach (TotalAmount item in HotelManager.GetRoomPrice(roomNumber))
            {
                roomPrice = item.RoomPrice;
            }

            return roomPrice + servicesPrice;
            
        }
    }
}
