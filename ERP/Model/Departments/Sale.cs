using System;
using System.Reflection.Metadata;
using MySql.Data.MySqlClient;

namespace FreeERP.Model
{
    public class Sale
    {

        public Sale()
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
                    string query = "SELECT * FROM SaleTicket";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int user_id = reader.GetInt32("user_id");
                            var date_created = reader.GetMySqlDateTime("date_created");
                            string product = reader.GetString("product");
                            string content = reader.GetString("content");

                            string ticket = String.Format("User: {0}, date created: {1}, product: {2}, content: {3}",
                                user_id, date_created, product, content);

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

