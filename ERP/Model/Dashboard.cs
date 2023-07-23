using System;
using FreeERP.Model.Tickets;
using FreeERP.Utils;
using MySql.Data.MySqlClient;

namespace FreeERP.Model
{
    public class DashBoardData
    {
        public int TotalTickets { get; set; }
        public int TotalBills { get; set; }
        public int TotalCustomers { get; set; }
        public DashBoardData(int totalTickets, int totalBills, int totalCustomers)
        {
            TotalTickets = totalTickets;
            TotalBills = totalBills;
            TotalCustomers = totalCustomers;
        }
    }
	public class Dashboard
	{
		public Dashboard()
		{
		}
        public static int QueryTotalTickets()
        {

            int totalTickets = 0;

            string dbConfigFilePath = DB.GetDBConfig();
            string connectionString = string.Empty;
            if (System.IO.File.Exists(dbConfigFilePath))
            {
                connectionString = System.IO.File.ReadAllText(dbConfigFilePath);
            }
            string dbError;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Execute your database operations here
                    string query = "SELECT COUNT(*) as total FROM Ticket";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            totalTickets = reader.GetInt32("total");
                        }
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    dbError = ex.Message;
                }
            }

            return totalTickets;
        }
        public static int QueryTotalBills()
        {

            int totalBills = 0;

            string dbConfigFilePath = DB.GetDBConfig();
            string connectionString = string.Empty;
            if (System.IO.File.Exists(dbConfigFilePath))
            {
                connectionString = System.IO.File.ReadAllText(dbConfigFilePath);
            }
            string dbError;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Execute your database operations here
                    string query = "SELECT COUNT(*) as total FROM Bill";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            totalBills = reader.GetInt32("total");
                        }
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    dbError = ex.Message;
                }
            }

            return totalBills;
        }
        public static int QueryTotalCustomers()
        {

            int totalCustomers = 0;

            string dbConfigFilePath = DB.GetDBConfig();
            string connectionString = string.Empty;
            if (System.IO.File.Exists(dbConfigFilePath))
            {
                connectionString = System.IO.File.ReadAllText(dbConfigFilePath);
            }
            string dbError;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Execute your database operations here
                    string query = "SELECT COUNT(*) as total FROM User WHERE type=\"Customer\"";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            totalCustomers = reader.GetInt32("total");
                        }
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    dbError = ex.Message;
                }
            }

            return totalCustomers;
        }
    }
}
