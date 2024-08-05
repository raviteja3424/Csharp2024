using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Miniproject
{
    public class Program
    {
        private static string connectionString = "data source=ICS-LT-38Q0LQ3\\SQLEXPRESS; initial catalog=RaiwayReservation2; User ID=sa; Password=june@2024";

        public static void Main()
        {
            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Admin Login");
                Console.WriteLine("4. Exit");

                Console.Write("Choose an option: ");
                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        RegisterUser();
                        break;
                    case 2:
                        LoginUser();
                        break;
                    case 3:
                        AdminLogin();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        private static void RegisterUser()
        {
            try
            {
                Console.Write("Enter username: ");
                string username = Console.ReadLine();

                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                Console.Write("Enter full name: ");
                string fullName = Console.ReadLine();

                Console.Write("Enter email: ");
                string email = Console.ReadLine();

                Console.Write("Enter phone number: ");
                string phoneNumber = Console.ReadLine();



                string query = "INSERT INTO Users (UserName, Password, FullName, Email, PhoneNumber) VALUES (@UserName, @Password, @FullName, @Email, @PhoneNumber)";
                SqlParameter[] parameters = {
                new SqlParameter("@UserName", username),
                new SqlParameter("@Password", password),
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@Email", email),
                new SqlParameter("@PhoneNumber", phoneNumber)
            };

                ExecuteNonQuery(query, parameters);
                Console.WriteLine("User registered successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        private static void LoginUser()
        {
            try
            {
                Console.Write("Enter username: ");
                string username = Console.ReadLine();

                Console.Write("Enter password: ");
                string password = Console.ReadLine();



                string query = "SELECT UserID FROM Users WHERE UserName = @UserName AND Password = @Password";
                SqlParameter[] parameters = {
                new SqlParameter("@UserName", username),
                new SqlParameter("@Password", password)
            };

                int userID = ExecuteScalarInt(query, parameters);

                if (userID == 0)
                {
                    Console.WriteLine("Invalid credentials.");
                    return;
                }

                Console.WriteLine("Login successful!");
                UserMenu(userID);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void UserMenu(int userID)
        {
            while (true)
            {
                Console.WriteLine("\nUser Menu:");
                Console.WriteLine("1. Book Tickets");
                Console.WriteLine("2. Cancel Tickets");
                Console.WriteLine("3. Logout");

                Console.Write("Choose an option: ");
                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        BookTickets(userID);
                        break;
                    case 2:
                        CancelTickets(userID);
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private static void BookTickets(int userID)
        {
            try
            {
                ListTrains();

                Console.Write("Enter TrainID to book tickets: ");
                if (!int.TryParse(Console.ReadLine(), out int trainID))
                {
                    Console.WriteLine("Invalid input. Please enter a valid TrainID.");
                    return;
                }

                Console.Write("Enter number of seats to book (max 5): ");
                if (!int.TryParse(Console.ReadLine(), out int seats) || seats > 5 || seats < 1)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                    return;
                }

                int availableSeats = GetAvailableSeats(trainID);

                if (availableSeats < seats)
                {
                    Console.WriteLine("Not enough seats available.");
                    return;
                }

                ConfirmBooking(trainID, userID, seats);
                UpdateSeatAvailability(trainID, seats);

                int bookingID = GetLatestBookingID(userID, trainID);
                var userDetails = GetUserDetails(userID);
                var trainDetails = GetTrainDetails(trainID);
                var bookingDetails = GetBookingDetails(bookingID);

                PrintTicket(bookingID, userDetails, trainDetails, bookingDetails);
                Console.WriteLine("Booking confirmed!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void CancelTickets(int userID)
        {
            try
            {
                Console.Write("Enter BookingID to cancel: ");
                if (!int.TryParse(Console.ReadLine(), out int bookingID))
                {
                    Console.WriteLine("Invalid input. Please enter a valid BookingID.");
                    return;
                }

                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT TrainID, NumberOfSeats FROM Booking WHERE BookingID = @BookingID AND Status = 'Confirmed' AND UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@BookingID", bookingID);
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                Console.WriteLine("Booking not found or already cancelled.");
                                return;
                            }

                            int trainID = (int)reader["TrainID"];
                            int bookedSeats = (int)reader["NumberOfSeats"];
                            reader.Close();

                            Console.Write("Enter number of seats to cancel: ");
                            if (!int.TryParse(Console.ReadLine(), out int seatsToCancel) || seatsToCancel <= 0 || seatsToCancel > bookedSeats)
                            {
                                Console.WriteLine("Invalid number of seats.");
                                return;
                            }

                            using (var transaction = conn.BeginTransaction())
                            {
                                try
                                {
                                    using (var updateBookingCmd = new SqlCommand("UPDATE Booking SET NumberOfSeats = NumberOfSeats - @SeatsToCancel WHERE BookingID = @BookingID AND UserID = @UserID", conn, transaction))
                                    {
                                        updateBookingCmd.Parameters.AddWithValue("@SeatsToCancel", seatsToCancel);
                                        updateBookingCmd.Parameters.AddWithValue("@BookingID", bookingID);
                                        updateBookingCmd.Parameters.AddWithValue("@UserID", userID);
                                        int rowsAffected = updateBookingCmd.ExecuteNonQuery();

                                        if (rowsAffected == 0)
                                        {
                                            throw new Exception("Failed to update booking.");
                                        }

                                        using (var updateSeatsCmd = new SqlCommand("UPDATE Trains SET NoOfSeats = NoOfSeats + @SeatsToCancel WHERE TrainID = @TrainID", conn, transaction))
                                        {
                                            updateSeatsCmd.Parameters.AddWithValue("@SeatsToCancel", seatsToCancel);
                                            updateSeatsCmd.Parameters.AddWithValue("@TrainID", trainID);
                                            updateSeatsCmd.ExecuteNonQuery();
                                        }

                                        transaction.Commit();
                                        Console.WriteLine(" cancellation successful.");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();
                                    Console.WriteLine($"Error: {ex.Message}");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void ListTrains()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT * FROM Trains", conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            Console.WriteLine("Train List:");
                            while (reader.Read())
                            {
                                Console.WriteLine($"TrainID: {reader["TrainID"]}, TrainNo: {reader["TrainNo"]}, TrainName: {reader["TrainName"]}, From: {reader["FromStation"]}, To: {reader["ToStation"]}, Date: {reader["Date"]}, Price: {reader["Price"]}, Seats Available: {reader["NoOfSeats"]}, Status: {reader["Status"]}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static int GetAvailableSeats(int trainID)
        {
            try
            {
                string query = "SELECT NoOfSeats FROM Trains WHERE TrainID = @TrainID";
                SqlParameter[] parameters = {
                new SqlParameter("@TrainID", trainID)
            };
                return ExecuteScalarInt(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 0;
            }
        }

        private static void ConfirmBooking(int trainID, int userID, int numberOfSeats)
        {
            try
            {
                string query = "INSERT INTO Booking (TrainID, UserID, NumberOfSeats, Status) VALUES (@TrainID, @UserID, @NumberOfSeats, 'Confirmed')";
                SqlParameter[] parameters = {
                new SqlParameter("@TrainID", trainID),
                new SqlParameter("@UserID", userID),
                new SqlParameter("@NumberOfSeats", numberOfSeats)
            };
                ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void UpdateSeatAvailability(int trainID, int bookedSeats)
        {
            try
            {
                string query = "UPDATE Trains SET NoOfSeats = NoOfSeats - @BookedSeats WHERE TrainID = @TrainID";
                SqlParameter[] parameters = {
                new SqlParameter("@BookedSeats", bookedSeats),
                new SqlParameter("@TrainID", trainID)
            };
                ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static int GetLatestBookingID(int userID, int trainID)
        {
            try
            {
                string query = "SELECT TOP 1 BookingID FROM Booking WHERE UserID = @UserID AND TrainID = @TrainID ORDER BY BookingID DESC";
                SqlParameter[] parameters = {
                new SqlParameter("@UserID", userID),
                new SqlParameter("@TrainID", trainID)
            };
                return ExecuteScalarInt(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 0;
            }
        }

        private static (string FullName, string Email, string PhoneNumber) GetUserDetails(int userID)
        {
            try
            {
                string query = "SELECT FullName, Email, PhoneNumber FROM Users WHERE UserID = @UserID";
                SqlParameter[] parameters = {
                new SqlParameter("@UserID", userID)
            };
                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddRange(parameters);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return (
                                    reader["FullName"].ToString(),
                                    reader["Email"].ToString(),
                                    reader["PhoneNumber"].ToString()
                                );
                            }
                            throw new Exception("User not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw; // Re-throw to propagate the error if needed
            }
        }

        private static (string TrainNo, string TrainName, string FromStation, string ToStation, DateTime Date, decimal Price) GetTrainDetails(int trainID)
        {
            try
            {
                string query = "SELECT TrainNo, TrainName, FromStation, ToStation, Date, Price FROM Trains WHERE TrainID = @TrainID";
                SqlParameter[] parameters = {
                new SqlParameter("@TrainID", trainID)
            };
                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddRange(parameters);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return (
                                    reader["TrainNo"].ToString(),
                                    reader["TrainName"].ToString(),
                                    reader["FromStation"].ToString(),
                                    reader["ToStation"].ToString(),
                                    (DateTime)reader["Date"],
                                    (decimal)reader["Price"]
                                );
                            }
                            throw new Exception("Train not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw; // Re-throw to propagate the error if needed
            }
        }

        private static (int NumberOfSeats, string Status) GetBookingDetails(int bookingID)
        {
            try
            {
                string query = "SELECT NumberOfSeats, Status FROM Booking WHERE BookingID = @BookingID";
                SqlParameter[] parameters = {
                new SqlParameter("@BookingID", bookingID)
            };
                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddRange(parameters);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return (
                                    (int)reader["NumberOfSeats"],
                                    reader["Status"].ToString()
                                );
                            }
                            throw new Exception("Booking not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw; // Re-throw to propagate the error if needed
            }
        }

        private static void PrintTicket(int bookingID, (string FullName, string Email, string PhoneNumber) userDetails, (string TrainNo, string TrainName, string FromStation, string ToStation, DateTime Date, decimal Price) trainDetails, (int NumberOfSeats, string Status) bookingDetails)
        {
            Console.WriteLine("\nTicket Details:");
            Console.WriteLine($"BookingID: {bookingID}");
            Console.WriteLine($"Name: {userDetails.FullName}");
            Console.WriteLine($"Email: {userDetails.Email}");
            Console.WriteLine($"Phone: {userDetails.PhoneNumber}");
            Console.WriteLine($"Train No: {trainDetails.TrainNo}");
            Console.WriteLine($"Train Name: {trainDetails.TrainName}");
            Console.WriteLine($"From: {trainDetails.FromStation}");
            Console.WriteLine($"To: {trainDetails.ToStation}");
            Console.WriteLine($"Date: {trainDetails.Date}");
            Console.WriteLine($"Price: {trainDetails.Price}");
            Console.WriteLine($"Number of Seats: {bookingDetails.NumberOfSeats}");
            Console.WriteLine($"Status: {bookingDetails.Status}");
        }

        private static void AdminLogin()
        {
            Console.Write("Enter admin username: ");
            string adminUsername = Console.ReadLine();

            Console.Write("Enter admin password: ");
            string adminPassword = Console.ReadLine();

            // Replace this with actual admin credentials validation
            if (adminUsername == "ravi" && adminPassword == "ravi3424") // Simplistic approach for example
            {
                Console.WriteLine("Admin login successful!");
                AdminMenu();
            }
            else
            {
                Console.WriteLine("Invalid admin credentials.");
            }
        }

        private static void AdminMenu()
        {
            while (true)
            {
                Console.WriteLine("\nAdmin Menu:");
                Console.WriteLine("1. Add Train");
                Console.WriteLine("2. Remove Train");
                Console.WriteLine("3. Update Train Details");
                Console.WriteLine("4. View All Bookings");
                Console.WriteLine("5. Logout");

                Console.Write("Choose an option: ");
                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        AddTrain();
                        break;
                    case 2:
                        RemoveTrain();
                        break;
                    case 3:
                        UpdateTrainDetails();
                        break;
                    case 4:
                        ViewAllBookings();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private static void AddTrain()
        {
            Console.Write("Enter Train Number: ");
            string trainNo = Console.ReadLine();

            Console.Write("Enter Train Name: ");
            string trainName = Console.ReadLine();

            Console.Write("Enter From Station: ");
            string fromStation = Console.ReadLine();

            Console.Write("Enter To Station: ");
            string toStation = Console.ReadLine();

            Console.Write("Enter Date (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine("Invalid date format.");
                return;
            }

            Console.Write("Enter Price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid price.");
                return;
            }

            Console.Write("Enter Number of Seats: ");
            if (!int.TryParse(Console.ReadLine(), out int numberOfSeats))
            {
                Console.WriteLine("Invalid number of seats.");
                return;
            }

            string query = "INSERT INTO Trains (TrainNo, TrainName, FromStation, ToStation, Date, Price, NoOfSeats, Status) VALUES (@TrainNo, @TrainName, @FromStation, @ToStation, @Date, @Price, @NoOfSeats, 'Available')";
            SqlParameter[] parameters = {
            new SqlParameter("@TrainNo", trainNo),
            new SqlParameter("@TrainName", trainName),
            new SqlParameter("@FromStation", fromStation),
            new SqlParameter("@ToStation", toStation),
            new SqlParameter("@Date", date),
            new SqlParameter("@Price", price),
            new SqlParameter("@NoOfSeats", numberOfSeats)
        };

            ExecuteNonQuery(query, parameters);
            Console.WriteLine("Train added successfully.");
        }

        private static void RemoveTrain()
        {
            Console.Write("Enter TrainID to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int trainID))
            {
                Console.WriteLine("Invalid input. Please enter a valid TrainID.");
                return;
            }

            string query = "DELETE FROM Trains WHERE TrainID = @TrainID";
            SqlParameter[] parameters = {
            new SqlParameter("@TrainID", trainID)
        };

            ExecuteNonQuery(query, parameters);
            Console.WriteLine("Train removed successfully.");
        }

        private static void UpdateTrainDetails()
        {
            Console.Write("Enter TrainID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int trainID))
            {
                Console.WriteLine("Invalid input. Please enter a valid TrainID.");
                return;
            }

            Console.Write("Enter new Train Number: ");
            string trainNo = Console.ReadLine();

            Console.Write("Enter new Train Name: ");
            string trainName = Console.ReadLine();

            Console.Write("Enter new From Station: ");
            string fromStation = Console.ReadLine();

            Console.Write("Enter new To Station: ");
            string toStation = Console.ReadLine();

            Console.Write("Enter new Date (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine("Invalid date format.");
                return;
            }

            Console.Write("Enter new Price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid price.");
                return;
            }

            Console.Write("Enter new Number of Seats: ");
            if (!int.TryParse(Console.ReadLine(), out int numberOfSeats))
            {
                Console.WriteLine("Invalid number of seats.");
                return;
            }

            string query = "UPDATE Trains SET TrainNo = @TrainNo, TrainName = @TrainName, FromStation = @FromStation, ToStation = @ToStation, Date = @Date, Price = @Price, NoOfSeats = @NoOfSeats WHERE TrainID = @TrainID";
            SqlParameter[] parameters = {
            new SqlParameter("@TrainNo", trainNo),
            new SqlParameter("@TrainName", trainName),
            new SqlParameter("@FromStation", fromStation),
            new SqlParameter("@ToStation", toStation),
            new SqlParameter("@Date", date),
            new SqlParameter("@Price", price),
            new SqlParameter("@NoOfSeats", numberOfSeats),
            new SqlParameter("@TrainID", trainID)
        };

            ExecuteNonQuery(query, parameters);
            Console.WriteLine("Train details updated successfully.");
        }

        private static void ViewAllBookings()
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Booking";

                using (var cmd = new SqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("Booking List:");
                        while (reader.Read())
                        {
                            Console.WriteLine($"BookingID: {reader["BookingID"]}, TrainID: {reader["TrainID"]}, UserID: {reader["UserID"]}, NumberOfSeats: {reader["NumberOfSeats"]}, Status: {reader["Status"]}");
                        }
                    }
                }
            }
        }
        private static SqlConnection GetConnection()
        {
            try
            {
                return new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating SQL connection: {ex.Message}");
                throw;
            }
        }

        private static int ExecuteScalarInt(string query, SqlParameter[] parameters)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddRange(parameters);
                        object result = cmd.ExecuteScalar();
                        return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing scalar query: {ex.Message}");
                throw;
            }
        }

        private static void ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddRange(parameters);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing non-query: {ex.Message}");
                throw;
            }
        }
    }
}
