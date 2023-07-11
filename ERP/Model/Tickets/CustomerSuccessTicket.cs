using System;
using MySql.Data.MySqlClient;

namespace FreeERP.Model.Tickets
{
    public class CustomerSuccessTicketFactory
    {
        static public CustomerSuccessTicket? QueryTicketById(string ticketID)
        {
            string connectionString = "Server=localhost;Database=freeerp;Uid=root;";
            string dbError = "";
            Int32 user_id = 0;
            string content = "";
            DateTime dateCreated = DateTime.Now;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = String.Format($"SELECT * FROM CustomerSuccessTicket " +
                        "WHERE ticket_id = {0}", ticketID);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user_id = reader.GetInt32("user_id");
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

            return new CustomerSuccessTicket(Convert.ToString(user_id), dateCreated, content);
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

                    string query = String.Format($"UPDATE CustomerSuccessTicket SET status = {0} " +
                        "WHERE ticket_id = {1}", status, ticketID);

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
    public class CustomerSuccessTicket : Ticket
    {
        public CustomerSuccessTicket(string userID, DateTime dt, string content) : base(TicketType.CustomerSuccess, dt, userID, content)
        {
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

                    string query = String.Format($"INSERT INTO CustomerSuccessTicket " +
                        $"(date_created, user_id, content) " +
                        "values (CURDATE(), {0}, \"{1}\")", Convert.ToInt32(UserID), Content);

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

