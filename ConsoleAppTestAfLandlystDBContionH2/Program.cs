using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTestAfLandlystDBContionH2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("// START");

            Console.WriteLine("vil du have en særlid service : ja|nej");
            string usrServiceYesOrNo = Console.ReadLine();
            string usrService = "";
            if (usrServiceYesOrNo == "ja")
            {
                Console.WriteLine("hvilken service vil du have: ");
                usrService = Console.ReadLine();
            }

            List<Room> rooms = HotelManager.GetRooms(usrServiceYesOrNo, usrService);

            Console.WriteLine("Room No | Price");
            foreach (Room item in rooms)
            {
                Console.WriteLine("  " + item.RoomNo + "       " + item.Price);
            }

            //List<Booking> bookings = HotelManager.GetBookings();
            //foreach (Booking item in bookings)
            //{
            //    Console.WriteLine("" + item.BookingNo + "  |  " + item.CheckIn + "  |   " + item.CheckOut + "  |  " + item.GuestsID);
            //}

            Console.WriteLine("hvilken dato for ankomst :");
            DateTime usrADate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("hvilken hjem dato :");
            DateTime usrLDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("hvilken kunde numer :");
            int usrCustomerNo = Int32.Parse(Console.ReadLine());
            //DalManager.SetBookings(new DateTime(2021, 01, 21), new DateTime(2021, 02, 16), 1); // using the storde procedureder addbooking
            DalManager.SetBookings(usrADate, usrLDate, usrCustomerNo); // using the storde procedureder addbooking
          
            Console.WriteLine("hvilken vælget værlse :");
            int usrRoomNo = Int32.Parse(Console.ReadLine());
            Console.WriteLine("den samlet pris er : ");

            ////List<TotalAmount> totals = DalManager.GetRoomPrice(100);
            ////foreach (TotalAmount item in totals)
            ////{
            ////    //Console.WriteLine(item.RoomNo + "  " + item.RoomPrice + "  " + item.ServiceName + "       " + item.ServicePrice);
            ////    Console.WriteLine(item.RoomPrice);
            ////}

            TotalAmount totalAmount = new TotalAmount();
            TimeSpan timeSpan = usrLDate - usrADate;
            Console.WriteLine(timeSpan.TotalDays + " dage");
            Console.WriteLine(totalAmount.CalculateAmount(usrRoomNo));
            if (timeSpan.TotalDays >= 7)
            {
                double PriceForRoom = totalAmount.CalculateAmount(usrRoomNo) * timeSpan.TotalDays;
                Console.WriteLine(PriceForRoom * (90/100));
            }
            else
            {
                Console.WriteLine(totalAmount.CalculateAmount(usrRoomNo) * timeSpan.TotalDays);
            }

            Console.WriteLine("// SLUT");
            Console.ReadKey();
        }
    }
}
