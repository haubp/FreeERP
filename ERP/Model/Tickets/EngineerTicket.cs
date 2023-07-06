using System;
namespace FreeERP.Model.Tickets
{
    public class EngineerTicket : Ticket
    {
        public EngineerTicket(string userID, string content) : base(TicketType.Engineer, userID, content)
        {
        }
        public override bool SaveToDB()
        {
            string connectionString = "server=your_server;database=your_database;user=your_username;password=your_password";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Execute your database operations here
                    string query = "SELECT * FROM your_table";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Access data from the reader
                            int id = reader.GetInt32("id");
                            string name = reader.GetString("name");

                            // Do something with the retrieved data
                        }
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occurred during the database operation
                    return BadRequest(ex.Message);
                }
            }

            return true;
        }
    }
}

