using System;
using System.Net;
using MySql.Data.MySqlClient;

namespace FreeERP.Model.Tickets
{
    public class SaleTicket : Ticket
    {
        public SaleTicket(string userID, string content) : base(TicketType.Sale, userID, content)
        {
        }
        public override string SaveToDB()
        {
            string connectionString = "server=localhost:3306;database=freeerp;user=root";

            string dbError = "";

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
                    dbError = ex.Message;
                }
            }

            return dbError;
        }
    }
}
