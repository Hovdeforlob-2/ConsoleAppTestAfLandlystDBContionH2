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
            Console.WriteLine("Hvad er din forenavn");
            string fisteName = Console.ReadLine();
            Console.WriteLine("Hvad er din efternavn");
            string lastName = Console.ReadLine();
            Console.WriteLine("Hvad er din Addrese");
            string  adderss= Console.ReadLine();
            Console.WriteLine("Hvad er din e-mail");
            string mail = Console.ReadLine();
            Console.WriteLine("Hvad er din telefonNo");
            int telefonNo = int.Parse(Console.ReadLine());
            Console.WriteLine("Hvad er din postnr");
            int zipCode = int.Parse(Console.ReadLine());

            HotelManager.SetGuests(fisteName, lastName, adderss, mail, telefonNo, zipCode);

            Console.WriteLine("vil du have en særlid service : ja|nej");
            string usrServiceYesOrNo = Console.ReadLine();
            string usrService = "";
            if (usrServiceYesOrNo == "ja")
            {
                Console.WriteLine("hvilken service vil du have: ");
                usrService = Console.ReadLine();
            }

            Console.WriteLine("hvilken dato for ankomst :");
            DateTime usrADate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("hvilken hjem dato :");
            DateTime usrLDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("hvilken kunde numer :");
            int usrCustomerNo = Int32.Parse(Console.ReadLine());

            List<Room> rooms = HotelManager.GetRooms(usrServiceYesOrNo, usrService, usrADate, usrLDate);

            Console.WriteLine("Room No | Price");
            foreach (Room item in rooms)
            {
                Console.WriteLine("  " + item.RoomNo + "       " + item.Price);
            }

            Console.WriteLine("hvilken vælget værlse :");
            int usrRoomNo = Int32.Parse(Console.ReadLine());

            DalManager.SetBookings(usrADate, usrLDate, usrCustomerNo, usrRoomNo);


            Console.WriteLine("den samlet pris er : ");
            TotalAmount totalAmount = new TotalAmount();
            Console.WriteLine(totalAmount.Percentage(usrADate, usrLDate, usrRoomNo));

            Console.WriteLine("// SLUT");
            Console.ReadKey();
        }
    }
}
