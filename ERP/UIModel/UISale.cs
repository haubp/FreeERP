using FreeERP.Model.Tickets;
using MySql.Data.MySqlClient;
using System;
namespace FreeERP.UIModel
{
    public class UISale
    {
        public UISale()
        {
            
        }
        public List<UISaleTicket> Tickets()
        {
            List<UISaleTicket> tickets = new();

            string connectionString = "Server=localhost;Database=freeerp;Uid=root;";
            string dbError;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Execute your database operations here
                    string query = "SELECT * FROM SaleTicket";

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

                            tickets.Add(new UISaleTicket(ticket_id, user_id, date_created, product, content));
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

