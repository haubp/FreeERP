using System;
using System.Net;
using MySql.Data.MySqlClient;

namespace FreeERP.Model.Tickets
{
    public class SaleTicket : Ticket
    {
        public string Product;
        public SaleTicket(string userID, string content, string product) : base(TicketType.Sale, userID, content)
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
