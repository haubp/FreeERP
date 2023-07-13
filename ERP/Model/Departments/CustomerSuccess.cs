using System;
using FreeERP.Model.Tickets;
using MySql.Data.MySqlClient;

namespace FreeERP.Model
{
    public class CustomerSuccess
    {
        public CustomerSuccess()
        {
        }
        public List<CustomerSuccessTicket> Tickets()
        {
            List<CustomerSuccessTicket> tickets = new();

            string connectionString = "Server=localhost;Database=freeerp;Uid=root;";
            string dbError;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                { 
                    connection.Open();

                    // Execute your database operations here
                    string query = "SELECT * FROM Ticket WHERE type=\"CS\"";

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

                            tickets.Add(new CustomerSuccessTicket(Convert.ToString(ticket_id), Convert.ToString(user_id), date_created, content, status));
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

