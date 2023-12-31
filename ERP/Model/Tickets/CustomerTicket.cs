﻿using System;
using System.Net;
using System.Reflection.Metadata;
using MySql.Data.MySqlClient;
using FreeERP.Utils;

namespace FreeERP.Model.Tickets
{
    public class CustomerTicketFactory
    {
        static public CustomerTicket? QueryTicketById(string ticketID)
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
            string product = "";
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
                        "WHERE type=\"Customer\" AND ticket_id = {0}", ticketID);

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

            return new CustomerTicket(Convert.ToString(ticket_id), Convert.ToString(user_id), dateCreated, content, product, status, priority);
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
                        "SET status = \"{0}\" WHERE type=\"Customer\" AND ticket_id = {1}", status, ticketID);

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

    public class CustomerTicketPostData
    {
        public string? Status { get; set; }
    }

    public class CustomerTicket : Ticket
    {
        public string Product;
        public CustomerTicket(string id, string userID, DateTime dt, string content, string product, string priority) : base(id, TicketType.Sale, dt, userID, content, priority)
        {
            Product = product;
        }
        public CustomerTicket(string id, string userID, DateTime dt, string content, string product, string status, string priority) : base(id, TicketType.Sale, dt, userID, content, status, priority)
        {
            Product = product;
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
                        $"(date_created, user_id, content, product, status, type, priority) " +
                        "values (CURDATE(), {0}, \"{1}\", \"{2}\", \"{3}\", \"Customer\", \"{4}\")", Convert.ToInt32(UserID), Content, Product, Status, Priority);

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
