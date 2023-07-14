using System;
using System.Reflection.Metadata;
using MySql.Data.MySqlClient;
using FreeERP.Model.Tickets;
using FreeERP.Utils;

namespace FreeERP.Model
{
    public class Sale
    {

        public Sale()
        {
        }
        public List<SaleTicket> Tickets()
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
                    string query = "SELECT * FROM Ticket WHERE type=\"Sale\"";

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

                            tickets.Add(new SaleTicket(Convert.ToString(ticket_id), Convert.ToString(user_id), date_created, content!, product!, status!));
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

