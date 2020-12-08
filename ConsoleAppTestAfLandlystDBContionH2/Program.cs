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
            //DalManager.SetGuests("Per", "jenson", "test no.2", "Gtest@test.com", 11223344, 4800);
            //Console.WriteLine("vil du have en særlid service : ja|nej");
            //string usrServiceYesOrNo = Console.ReadLine();
            //string usrService = "";
            //if (usrServiceYesOrNo == "ja")
            //{
            //    Console.WriteLine("hvilken service vil du have: ");
            //    usrService = Console.ReadLine();
            //}

            //List<Room> rooms = HotelManager.GetRooms(usrServiceYesOrNo, usrService);

            //Console.WriteLine("Room No | Price");
            //foreach (Room item in rooms)
            //{
            //    Console.WriteLine("  " + item.RoomNo + "       " + item.Price);
            //}



            Console.WriteLine("hvilken dato for ankomst :");
            DateTime usrADate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("hvilken hjem dato :");
            DateTime usrLDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("hvilken kunde numer :");
            int usrCustomerNo = Int32.Parse(Console.ReadLine());
        /*    DalManager.SetBookings(usrADate, usrLDate, usrCustomerNo);*/ // using the storde procedureder addbooking
          
            Console.WriteLine("hvilken vælget værlse :");
            int usrRoomNo = Int32.Parse(Console.ReadLine());
            Console.WriteLine("den samlet pris er : ");

            #region totalprice
            ////TotalAmount totalAmount = new TotalAmount();
            ////TimeSpan timeSpan = usrLDate - usrADate;
            ////Console.WriteLine(timeSpan.TotalDays + " dage");
            ////Console.WriteLine(totalAmount.CalculateAmount(usrRoomNo));
            ////Console.WriteLine("total price;");
            ////if (timeSpan.TotalDays >= 7)
            ////{
            ////    double PriceForRoom = totalAmount.CalculateAmount(usrRoomNo) * timeSpan.TotalDays;
            ////    Console.WriteLine(Convert.ToDecimal(PriceForRoom) * (decimal.Divide(90, 100)));

            ////}
            ////else
            ////{
            ////    Console.WriteLine(totalAmount.CalculateAmount(usrRoomNo) * timeSpan.TotalDays);
            ////}
            #endregion 

            TotalAmount totalAmount = new TotalAmount();
            Console.WriteLine(totalAmount.Percentage(usrADate, usrLDate, usrRoomNo));

            //DalManager.SetRoomQuantity(36,201);

            Console.WriteLine("// SLUT");
            Console.ReadKey();
        }
    }
}
