using MySql.Data.MySqlClient;
using System;
namespace FreeERP.UIModel
{
    public class UIEngineer
    {
        public UIEngineer()
        {
        }
        public List<UIEngineerTicket> Tickets()
        {
            List<UIEngineerTicket> tickets = new ();

            string connectionString = "Server=localhost;Database=freeerp;Uid=root;";
            string dbError;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Execute your database operations here
                    string query = "SELECT * FROM EngineerTicket";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int ticket_id = reader.GetInt32("ticket_id");
                            int user_id = reader.GetInt32("user_id");
                            var date_created = reader.GetDateTime("date_created");
                            string content = reader.GetString("content");

                            tickets.Add(new UIEngineerTicket(ticket_id, user_id, date_created, content));
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

