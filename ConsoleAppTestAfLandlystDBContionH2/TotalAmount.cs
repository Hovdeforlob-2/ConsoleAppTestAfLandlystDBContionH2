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

        private int CalculateAmount(int roomNumber)
        {
            HotelManager.GetRoomPrice(roomNumber);

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

        public decimal Percentage(DateTime usrADate, DateTime usrLDate, int usrRoomNo)
        {
            Booking booking = new Booking();
            TimeSpan timeSpan = usrLDate - usrADate;
            Console.WriteLine(timeSpan.TotalDays + " dage");
            Console.WriteLine(CalculateAmount(usrRoomNo));
            if (timeSpan.TotalDays >= 7)
            {
                double PriceForRoom = CalculateAmount(usrRoomNo) * timeSpan.TotalDays;
                decimal PriceWithProcentage = Convert.ToDecimal(PriceForRoom) * (decimal.Divide(90, 100));
                return PriceWithProcentage;
            }
            else
            {
                return Convert.ToDecimal(CalculateAmount(usrRoomNo) * timeSpan.TotalDays);
            }
        }
    }
}
