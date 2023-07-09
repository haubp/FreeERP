using System;
using MySql.Data.MySqlClient;

namespace FreeERP.Model.Tickets
{
    public class EngineerTicket : Ticket
    {
        public EngineerTicket(string userID, string content) : base(TicketType.Engineer, userID, content)
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

                    string query = String.Format($"INSERT INTO EngineerTicket " +
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

