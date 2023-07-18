using System;
using FreeERP.Model.Tickets;
using FreeERP.Utils;
using MySql.Data.MySqlClient;

namespace FreeERP.Model
{
    public class Customer
    {
        public string UserID { get; set; }
        public static string UpdateCustomerTier(string customerId, string tier) {
            string dbConfigFilePath = DB.GetDBConfig();
            string connectionString = string.Empty;
            if (System.IO.File.Exists(dbConfigFilePath))
            {
                connectionString = System.IO.File.ReadAllText(dbConfigFilePath);
            }
            string dbError = "";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Execute your database operations here
                    string query = $"UPDATE User SET tier=\"{tier}\" WHERE user_id=\"{customerId}\"";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.ExecuteReader();

                    connection.Close();
                }
                catch (Exception ex)
                {
                    dbError = ex.Message;
                }
            }
            return dbError;
        }
        public Customer(string userID)
        {
            UserID = userID;
        }
        public string QueryTier() {
            string tier = "";

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
                    string query = $"SELECT * FROM User WHERE user_id=\"{UserID}\"";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tier = reader.GetString("tier");
                        }
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    dbError = ex.Message;
                }
            }

            return tier;
        }
        public List<CustomerTicket> Tickets()
        {
            List<CustomerTicket> tickets = new();

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
                    string query = $"SELECT * FROM Ticket WHERE user_id=\"{UserID}\"";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int ticket_id = reader.GetInt32("ticket_id");
                            int user_id = reader.GetInt32("user_id");
                            var date_created = reader.GetDateTime("date_created");
                            string? product = reader.GetString("product");
                            string? content = reader.GetString("content");
                            string? status = reader.GetString("status");

                            tickets.Add(new CustomerTicket(Convert.ToString(ticket_id), Convert.ToString(user_id), date_created, content!, product!, status!));
                        }
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    dbError = ex.Message;
                }
            }

            return tickets;
        }
    }
}
