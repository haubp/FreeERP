using System;
using MySql.Data.MySqlClient;

namespace FreeERP.Model
{
    public class Engineer
    {
        public Engineer()
        {
        }
        public List<string> Tickets()
        {
            List<string> tickets = new List<string>();

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
                            int user_id = reader.GetInt32("user_id");
                            var date_created = reader.GetMySqlDateTime("date_created");
                            string content = reader.GetString("content");

                            string ticket = String.Format("User: {0}, date created: {1}, content: {2}",
                                user_id, date_created, content);

                            tickets.Add(ticket);
                        }
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    dbError = ex.Message;
                    tickets.Add(dbError);
                }
            }

            return tickets;
        }
    }
}

