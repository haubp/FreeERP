using System;
using FreeERP.Model.Tickets;
using FreeERP.Utils;
using MySql.Data.MySqlClient;

namespace FreeERP.Model
{
    public class Engineer
    {
        public string UserID { get; set; }
        public Engineer(string userId)
        {
            UserID = userId;
        }
        public List<SaleTicket> MonitoringTickets()
        {
            List<SaleTicket> tickets = new();

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
                            string product = reader.GetString("product");
                            string content = reader.GetString("content");
                            string status = reader.GetString("status");
                            string priority = reader.GetString("priority");

                            tickets.Add(new SaleTicket(Convert.ToString(ticket_id), Convert.ToString(user_id), date_created, content, product, status, priority));
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
        public List<EngineerTicket> Tickets()
        {
            List<EngineerTicket> tickets = new();

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
                    string query = "SELECT * FROM Ticket WHERE type=\"Engineer\"";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int ticket_id = reader.GetInt32("ticket_id");
                            int user_id = reader.GetInt32("user_id");
                            var date_created = reader.GetDateTime("date_created");
                            string content = reader.GetString("content");
                            string status = reader.GetString("status");

                            tickets.Add(new EngineerTicket(Convert.ToString(ticket_id), Convert.ToString(user_id), date_created, content, status));
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

