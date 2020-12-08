using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Configuration;



namespace ConsoleAppTestAfLandlystDBContionH2
{
    public static class DalManager
    {
        //private static string cs = @"Data Source=DATABASESEVER20;Initial Catalog=Test3;Persist Security Info=True;User ID=sa;Password=Kode123!";

        public static List<Room> GetRooms(string ServiceYesOrNo, string service)
        {
            List<Room> rooms = new List<Room>();

            using (SqlConnection connection = new SqlConnection(DBconnection.connect("LandLystDB")))
            {
                SqlCommand cmd;
                connection.Open();
                if (ServiceYesOrNo == "ja")
                {
                    cmd = new SqlCommand($@"
                    select dbo.RoomServices.RoomNo, dbo.Room.Price from dbo.Room
                    join dbo.RoomServices
                    on dbo.Room.RoomNo = dbo.RoomServices.RoomNo
                    where dbo.RoomServices.SerName = '{service}' and dbo.Room.Cleaned = 'true';", connection);
                }
                else
                {
                    cmd = new SqlCommand(@"
                    select RoomNo, Price from dbo.Room
                    where dbo.Room.Cleaned = 'true';", connection);
                }

               SqlDataReader dataReader = cmd.ExecuteReader();
                

                while (dataReader.Read())
                {
                    // RoomNo, Cleaned, Price
                    int roomNo = (int)dataReader["RoomNo"];
                    int price = (int)dataReader["Price"];

                    Room room = new Room()
                    { RoomNo = roomNo, Price = price };

                    rooms.Add(room);
                }
            }

            return rooms;
        }

        public static List<Booking> GetBookings()
        {
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection connection = new SqlConnection(DBconnection.connect("LandLystDB")))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("select * from Booking", connection);
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    //BookingNo, CheckIn, CheckOut, GuestsID
                    int bookingNo = (int)dataReader["BookingNo"];
                    DateTime checkIn = (DateTime)dataReader["CheckIn"];
                    DateTime checkOut = (DateTime)dataReader["CheckOut"];
                    int GuestsId = (int)dataReader["GuestsID"];

                    Booking booking = new Booking()
                    { BookingNo = bookingNo, CheckIn = checkIn, CheckOut = checkOut, GuestsID = GuestsId };

                    bookings.Add(booking);
                }
            }

            return bookings;
        }

        /// <summary>
        /// i denne metode bruger jeg dapper
        /// BookingNo, CheckIn, CheckOut, GuestsID
        /// </summary>
        /// <param name="bookingNo"></param>
        /// <param name="checkIn"></param>
        /// <param name="checkOut"></param>
        /// <param name="guestsId"></param>
        public static void SetBookings(DateTime checkIn, DateTime checkOut, int guestsId)
        {
            using (IDbConnection connection = new SqlConnection(DBconnection.connect("LandLystDB")))
            {              
                List<Booking> bookings = new List<Booking>();

                bookings.Add(new Booking {CheckIn = checkIn, CheckOut = checkOut, GuestsID = guestsId });

                connection.Execute("dbo.AddBooking @CheckIn, @CheckOut, @GuestsID", bookings);

            }

        }

        public static List<TotalAmount> GetRoomPrice(int roomNumber)
        {
            List<TotalAmount> amounts = new List<TotalAmount>();

            using (SqlConnection connection = new SqlConnection(DBconnection.connect("LandLystDB")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand($@"select Room.Price from Room
                where RoomNo = {roomNumber}", connection);


                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int roomPrice = (int)dataReader["Price"];


                    TotalAmount totalAmount = new TotalAmount()
                    {RoomPrice = roomPrice };

                    amounts.Add(totalAmount);
                }
            }

            return amounts;
        }

        public static List<TotalAmount> GetServicesPrice(int roomNumber)
        {
            List<TotalAmount> amounts = new List<TotalAmount>();

            using (SqlConnection connection = new SqlConnection(DBconnection.connect("LandLystDB")))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand($@"
                select HotelServices.Price from HotelServices
                join RoomServices
                on dbo.RoomServices.SerName = dbo.HotelServices.ServiceName
                where dbo.RoomServices.RoomNo = {roomNumber};", connection);
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int servicePrice = (int)dataReader["Price"];

                    TotalAmount totalAmount = new TotalAmount()
                    {ServicePrice = servicePrice };

                    amounts.Add(totalAmount);
                }
            }

            return amounts;
        }

        /// <summary>
        /// i denne metode bruger jeg dapper
        /// </summary>
        /// <param name="bookingNo"></param>
        /// <param name="roomNo"></param>
        public static void SetRoomQuantity(int bookingNo, int roomNo)
        {
            using (IDbConnection connection = new SqlConnection(DBconnection.connect("LandLystDB")))
            {
                List<RoomQuantity> roomQuantities = new List<RoomQuantity>();

                roomQuantities.Add(new RoomQuantity { BookingNo = bookingNo, RoomNo = roomNo});

                connection.Execute("dbo.AddRoomQuantity  @BookingNo,@RoomNo", roomQuantities);

            }

        }


        /// <summary>
        /// i denne metode bruger jeg dapper
        /// GuestsID, ForeName, LastName, Address, Email, TelephoneNo, ZipCode
        /// </summary>
        /// <param name="bookingNo"></param>
        /// <param name="checkIn"></param>
        /// <param name="checkOut"></param>
        /// <param name="guestsId"></param>
        public static void SetGuests(string foreName, string lastName, string address, string email, int telephoneNo, int zipCode)
        {
            using (IDbConnection connection = new SqlConnection(DBconnection.connect("LandLystDB")))
            {
                List<Guests> guests = new List<Guests>();

                guests.Add(new Guests {ForeName = foreName, LastName = lastName, Address = address, Email = email, TelefonNo = telephoneNo, ZipCode = zipCode });

                connection.Execute("dbo.AddNewGuests @Forename,@Lastname,@Address,@Email,@TelefonNo,@ZipCode", guests);

            }

        }

        public static List<Booking> GetDate(int bookingNo)
        {
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection connection = new SqlConnection(DBconnection.connect("LandLystDB")))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand($@"select dbo.Booking.CheckIn, dbo.Booking.CheckOut from Booking 
                where BookingNo = {bookingNo}", connection);
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    //BookingNo, CheckIn, CheckOut, GuestsID
                    DateTime checkIn = (DateTime)dataReader["CheckIn"];
                    DateTime checkOut = (DateTime)dataReader["CheckOut"];

                    Booking booking = new Booking()
                    { CheckIn = checkIn, CheckOut = checkOut };

                    bookings.Add(booking);
                }
            }

            return bookings;
        }
    }
}

