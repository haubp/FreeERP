using System;
using FreeERP.Utils;
using MySql.Data.MySqlClient;

namespace FreeERP.Model.Tickets
{
    public class CustomerSuccessTicketFactory
    {
        public static CustomerSuccessTicket CreateUIModelCustomerSuccessTicket(long ticketId, long userId, DateTime dateCreated, string content, string status)
        {
            return new CustomerSuccessTicket(Convert.ToString(ticketId), Convert.ToString(userId), dateCreated, content, status);
        }
        static public CustomerSuccessTicket? QueryTicketById(string ticketID)
        {
            string dbConfigFilePath = DB.GetDBConfig();
            string connectionString = string.Empty;
            if (System.IO.File.Exists(dbConfigFilePath))
            {
                connectionString = System.IO.File.ReadAllText(dbConfigFilePath);
            }
            string dbError = "";
            Int32 user_id = 0;
            string content = "";
            DateTime dateCreated = DateTime.Now;
            string status = "";
            Int32 ticket_id = 0;
            string priority = "";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = String.Format($"SELECT * FROM Ticket " +
                        "WHERE type=\"CS\" AND ticket_id = {0}", ticketID);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ticket_id = reader.GetInt32("ticket_id");
                            user_id = reader.GetInt32("user_id");
                            content = reader.GetString("content");
                            dateCreated = reader.GetDateTime("date_created");
                            status = reader.GetString("status");
                            priority = reader.GetString("priority");
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

            return new CustomerSuccessTicket(Convert.ToString(ticket_id), Convert.ToString(user_id), dateCreated, content, status, priority);
        }

        static public string UpdateTicketStatusById(string ticketID, string status)
        {
            string dbConfigFilePath = DB.GetDBConfig();
            string connectionString = string.Empty;
            if (System.IO.File.Exists(dbConfigFilePath))
            {
                connectionString = System.IO.File.ReadAllText(dbConfigFilePath);
            }
            string dbError = "";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = String.Format($"UPDATE Ticket " +
                        "SET status = \"{0}\" " +
                        "WHERE type=\"CS\" AND ticket_id = {1}", status, ticketID);

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

    public class CustomerSuccessTicketPostData {
        public string? Status { get; set; }
    }

    public class CustomerSuccessTicket : Ticket
    {
        public CustomerSuccessTicket(string id, string userID, DateTime dt, string content, string priority) : base(id, TicketType.CustomerSuccess, dt, userID, content, priority)
        {
        }
        public CustomerSuccessTicket(string id, string userID, DateTime dt, string content, string status, string priority) : base(id, TicketType.CustomerSuccess, dt, userID, content, status, priority)
        {
        }
        public override string SaveToDB()
        {
            string dbConfigFilePath = DB.GetDBConfig();
            string connectionString = string.Empty;
            if (System.IO.File.Exists(dbConfigFilePath))
            {
                connectionString = System.IO.File.ReadAllText(dbConfigFilePath);
            }

            string dbError = "";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = String.Format($"INSERT INTO Ticket " +
                        $"(date_created, user_id, content, status, product, type, priority) " +
                        "values (CURDATE(), {0}, \"{1}\", \"{2}\", \"\", \"CS\", \"{3}\")", UserID, Content, Status, Priority);

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

