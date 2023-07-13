using System;
using System.Net;
using System.Reflection.Metadata;
using MySql.Data.MySqlClient;

namespace FreeERP.Model.Tickets
{
    public class SaleTicketFactory
    {
        public static SaleTicket CreateUIModelSaleTicket(long ticketId, long userId, DateTime dateCreated, string product, string content, string status)
        {
            return new SaleTicket(Convert.ToString(ticketId), Convert.ToString(userId), dateCreated, product, content, status);
        }
        static public SaleTicket? QueryTicketById(string ticketID)
        {
            string connectionString = "Server=localhost;Database=freeerp;Uid=root;";
            string dbError = "";
            Int32 user_id = 0;
            string content = "";
            string product = "";
            DateTime dateCreated = DateTime.Now;
            string status = "";
            Int32 ticket_id = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = String.Format($"SELECT * FROM Ticket " +
                        "WHERE type=\"Sale\" AND ticket_id = {0}", ticketID);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ticket_id = reader.GetInt32("ticket_id");
                            user_id = reader.GetInt32("user_id");
                            product = reader.GetString("product");
                            content = reader.GetString("content");
                            dateCreated = reader.GetDateTime("date_created");
                            status = reader.GetString("status");
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

            return new SaleTicket(Convert.ToString(ticket_id), Convert.ToString(user_id), dateCreated, content, product, status);
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

                    string query = String.Format($"UPDATE Ticket " +
                        "SET status = \"{0}\" WHERE type=\"Sale\" AND ticket_id = {1}", status , ticketID);

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

    public class SaleTicketPostData {
        public string? status { get; set; }
    }

    public class SaleTicket : Ticket
    {
        public string Product;
        public SaleTicket(string id, string userID, DateTime dt, string content, string product) : base(id, TicketType.Sale, dt, userID, content)
        {
            Product = product;
        }
        public SaleTicket(string id, string userID, DateTime dt, string content, string product, string status) : base(id, TicketType.Sale, dt, userID, content, status)
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

                    string query = String.Format($"INSERT INTO Ticket " +
                        $"(date_created, user_id, content, product, status, type) " +
                        "values (CURDATE(), {0}, \"{1}\", \"{2}\", \"{3}\", \"Sale\")", Convert.ToInt32(UserID), Content, Product, Status);
                    
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
