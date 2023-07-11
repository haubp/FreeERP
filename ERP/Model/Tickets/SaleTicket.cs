using System;
using System.Net;
using System.Reflection.Metadata;
using MySql.Data.MySqlClient;

namespace FreeERP.Model.Tickets
{
    public class SaleTicketFactory
    {
        static public SaleTicket? QueryTicketById(string ticketID)
        {
            string connectionString = "Server=localhost;Database=freeerp;Uid=root;";
            string dbError = "";
            Int32 user_id = 0;
            string content = "";
            string product = "";
            DateTime dateCreated = DateTime.Now;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = String.Format($"SELECT * FROM SaleTicket " +
                        "WHERE ticket_id = {0}", ticketID);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user_id = reader.GetInt32("user_id");
                            product = reader.GetString("product");
                            content = reader.GetString("content");
                            dateCreated = reader.GetDateTime("date_created");
                        }
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    dbError = ex.Message;
                }
            }

            if (dbError != "")
                return null;

            return new SaleTicket(Convert.ToString(user_id), dateCreated, content, product);
        }

        static public string UpdateTicketStatusById(string ticketID, string status)
        {
            string connectionString = "Server=localhost;Database=freeerp;Uid=root;";
            string dbError = "";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = String.Format($"UPDATE SaleTicket SET status = {0} " +
                        "WHERE ticket_id = {1}", status , ticketID);

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
    }
    public class SaleTicket : Ticket
    {
        public string Product;
        public SaleTicket(string userID, DateTime dt, string content, string product) : base(TicketType.Sale, dt, userID, content)
        {
            Product = product;
        }
        public override string SaveToDB()
        {
            string connectionString = "Server=localhost;Database=freeerp;Uid=root;";

            string dbError = "";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = String.Format($"INSERT INTO SaleTicket " +
                        $"(date_created, user_id, content, product) " +
                        "values (CURDATE(), {0}, \"{1}\", \"{2}\")", Convert.ToInt32(UserID), Content, Product);
                    
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
    }
}
