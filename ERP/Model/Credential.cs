using System;
using FreeERP.Model.Tickets;
using FreeERP.Utils;
using MySql.Data.MySqlClient;

namespace FreeERP.Model
{
    public class Credential
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public AccountType Type;
        public Credential(string userName, string password, string type)
        {
            UserName = userName;
            Password = password;
            switch (type)
            {
                case "Customer":
                    Type = AccountType.Customer;
                    break;
                case "Sale":
                    Type = AccountType.Sale;
                    break;
                case "CustomerSuccess":
                    Type = AccountType.CustomerSuccess;
                    break;
                case "Engineer":
                    Type = AccountType.Engineer;
                    break;
            }
        }
        public Credential(string userID)
        {
            QueryUserFromUserId(userID);
        }
        public string CreateUser()
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

                    string type = "";
                    switch (Type)
                    {
                        case AccountType.Customer:
                            type = "Customer";
                            break;
                        case AccountType.Sale:
                            type = "Sale";
                            break;
                        case AccountType.CustomerSuccess:
                            type = "CustomerSuccess";
                            break;
                        case AccountType.Engineer:
                            type = "Engineer";
                            break;
                    }

                    // Execute your database operations here
                    string query = $"INSERT INTO User (username, password, type) " +
                        $"VALUES (\"{UserName}\", \"{Password}\", \"{type}\")";

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
        public string QueryUserIdFromUserNameAndPassword()
        {
            string dbConfigFilePath = DB.GetDBConfig();
            string connectionString = string.Empty;
            if (System.IO.File.Exists(dbConfigFilePath))
            {
                connectionString = System.IO.File.ReadAllText(dbConfigFilePath);
            }
            string dbError = "";

            string user_id = "";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Execute your database operations here
                    string query = $"SELECT * FROM User WHERE username=\"{UserName}\" AND password=\"{Password}\"";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user_id = reader.GetString("user_id");
                            string type = reader.GetString("type");
                            switch(type) {
                                case "Customer":
                                    Type = AccountType.Customer;
                                    break;
                                case "Sale":
                                    Type = AccountType.Sale;
                                    break;
                                case "CustomerSuccess":
                                    Type = AccountType.CustomerSuccess;
                                    break;
                                case "Engineer":
                                    Type = AccountType.Engineer;
                                    break;
                            }
                        }
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    dbError = ex.Message;
                }
            }

            return dbError != "" ? dbError : user_id;
        }
        public void QueryUserFromUserId(string user_id)
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

                    // Execute your database operations here
                    string query = $"SELECT * FROM User WHERE user_id={user_id}";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserName = reader.GetString("username");
                            Password = reader.GetString("password");
                            string type = reader.GetString("type");
                            switch (type)
                            {
                                case "Customer":
                                    Type = AccountType.Customer;
                                    break;
                                case "Sale":
                                    Type = AccountType.Sale;
                                    break;
                                case "CustomerSuccess":
                                    Type = AccountType.CustomerSuccess;
                                    break;
                                case "Engineer":
                                    Type = AccountType.Engineer;
                                    break;
                            }
                        }
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    dbError = ex.Message;
                }
            }
        }
    }

    public enum AccountType
    {
        Customer,
        CustomerSuccess,
        Sale,
        Engineer
    }
}
