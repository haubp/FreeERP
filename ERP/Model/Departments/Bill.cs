using System;
using FreeERP.Utils;
using MySql.Data.MySqlClient;

namespace FreeERP.Model
{
    public class BillFactory {
        public static Bill QueryBillById(string billId) {
            string dbConfigFilePath = DB.GetDBConfig();
            string connectionString = string.Empty;
            if (System.IO.File.Exists(dbConfigFilePath))
            {
                connectionString = System.IO.File.ReadAllText(dbConfigFilePath);
            }
            string dbError;
            string content = "";
            DateTime dateCreated;
            int amount = 0;
            int userId = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = $"SELECT * FROM Bill WHERE bill_id=\"{billId}\"";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            content = reader.GetString("content");
                            dateCreated = reader.GetDateTime("date_created");
                            amount = reader.GetInt32("amount");
                            userId = reader.GetInt32("user_id");
                        }
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    dbError = ex.Message;
                }
            }

            Bill bill = new Bill(Convert.ToString(userId), content, amount);
            bill.ID = Convert.ToInt32(billId);

            return bill;
        }
        public static string Pay(string billId)
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

                    string query = $"UPDATE Bill SET status=\"PAID\" WHERE bill_id=\"{billId}\"";

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
	public class Bill
	{
		public string UserID { get; set; }
		public string Content { get; set; }
		public int Amount { get; set; }
        public int ID { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
		public Bill(string userId, string content, int amount)
		{
			UserID = userId;
			Content = content;
			Amount = amount;
		}
        public string SaveToDB()
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

                    string query = String.Format($"INSERT INTO Bill " +
                        $"(date_created, status, user_id, content, amount) " +
                        "values (CURDATE(), \"OPEN\", {0}, \"{1}\", \"{2}\")", Convert.ToInt32(UserID), Content, Amount);

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

