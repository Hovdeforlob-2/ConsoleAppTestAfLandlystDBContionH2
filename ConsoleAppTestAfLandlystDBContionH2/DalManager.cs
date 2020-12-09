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
        #region Gets From Databasen(DB)
        public static List<Room> GetRooms(string ServiceYesOrNo, string service, DateTime usrADate, DateTime usrLDate)
        {
            List<Room> rooms = new List<Room>();
            List<Booking> bookings = DalManager.GetRoomInUse(ServiceYesOrNo, usrADate, usrLDate);

            string contentsFromRoomUse = "";
            foreach (Booking item in bookings)
            {
                contentsFromRoomUse = contentsFromRoomUse + "Room.RoomNo <> " + item.RoomNo;
            }

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
                    where dbo.RoomServices.SerName = '{service}' and dbo.Room.Cleaned = 'true' and {contentsFromRoomUse};", connection);
                }
                else
                {
                    cmd = new SqlCommand($@"
                    select RoomNo, Price from dbo.Room
                    where dbo.Room.Cleaned = 'true' and {contentsFromRoomUse};", connection);
                }

                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    // RoomNo, Price
                    int roomNo = (int)dataReader["RoomNo"];
                    int price = (int)dataReader["Price"];

                    Room room = new Room()
                    { RoomNo = roomNo, Price = price };

                    rooms.Add(room);
                }
            }
            return rooms;
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
                    { RoomPrice = roomPrice };

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
                    { ServicePrice = servicePrice };

                    amounts.Add(totalAmount);
                }
            }
            return amounts;
        }

        private static List<Booking> GetRoomInUse(string ServiceYesOrNo, DateTime usrADate, DateTime usrLDate)
        {
            List<Booking> bookedRoomNo = new List<Booking>();

            using (SqlConnection connection = new SqlConnection(DBconnection.connect("LandLystDB")))
            {
                SqlCommand cmd;
                connection.Open();

                cmd = new SqlCommand($@"
                select dbo.Booking.RoomNo from dbo.Booking
                where Booking.CheckIn between '2020-05-01' and '2020-10-03';", connection);


                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int roomNo = (int)dataReader["RoomNo"];

                    Booking room = new Booking()
                    { RoomNo = roomNo };

                    bookedRoomNo.Add(room);
                }
            }
            return bookedRoomNo;
        }
        #endregion

        #region Sets to Databasen(DB)
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

                guests.Add(new Guests { ForeName = foreName, LastName = lastName, Address = address, Email = email, TelefonNo = telephoneNo, ZipCode = zipCode });

                connection.Execute("dbo.AddNewGuests @Forename,@Lastname,@Address,@Email,@TelefonNo,@ZipCode", guests);

            }
        }

        /// <summary>
        /// i denne metode bruger jeg dapper
        /// BookingNo, CheckIn, CheckOut, GuestsID
        /// </summary>
        /// <param name="bookingNo"></param>
        /// <param name="checkIn"></param>
        /// <param name="checkOut"></param>
        /// <param name="guestsId"></param>
        public static void SetBookings(DateTime checkIn, DateTime checkOut, int guestsId, int roomNo)
        {
            using (IDbConnection connection = new SqlConnection(DBconnection.connect("LandLystDB")))
            {
                List<Booking> bookings = new List<Booking>();

                bookings.Add(new Booking { CheckIn = checkIn, CheckOut = checkOut, GuestsID = guestsId, RoomNo = roomNo });

                connection.Execute("dbo.AddBooking @CheckIn, @CheckOut, @GuestsID, @RoomNo", bookings);

            }
        }
        #endregion
    }
}