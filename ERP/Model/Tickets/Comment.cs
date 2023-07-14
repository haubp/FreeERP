using FreeERP.Utils;
using MySql.Data.MySqlClient;

namespace FreeERP.Model.Tickets
{
    public class CommentFactory
    {
        public static string CreateComment(Int32 ticket_id, Int32 user_id, string content)
        {
            Comment comment = new(ticket_id, user_id, content);
            return comment.SaveToDB();
        }
        public static List<string> QueryCommentByTicketId(Int32 ticket_id)
        {
            List<string> comments = new List<string>();

            string dbConfigFilePath = DB.GetDBConfig();
            string connectionString = string.Empty;
            if (System.IO.File.Exists(dbConfigFilePath))
            {
                connectionString = System.IO.File.ReadAllText(dbConfigFilePath);
            }
            string dbError;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT * FROM Comment WHERE ticket_id = \"{ticket_id}\"";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string user_id = reader.GetString("user_id");
                            string content = reader.GetString("content");
                            DateTime d = reader.GetDateTime("date_created");

                            String str = $"<div class=\"flex m-4\">" +
                                            $"<div class=\"w-8 h-8 bg-gray-200 rounded-full flex items-center justify-center\">\r\n " +
                                               $" <span class=\"text-gray-700 text-xl font-bold\">{user_id}</span>\r\n" +
                                            $"</div>" +
                                            $"<span class=\"ml-4 text-gray-500\">{d.ToString()}</span>" +
                                         "</div>" +
                                         $"<div class=\"ml-8 text-xl text-gray-800\">{content}</div>";

                            comments.Add(str);
                        }
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    dbError = ex.Message;
                }

                return comments;
            }
        }
    }
    public class CommentPostData
    {
        public Int32 TicketID { get; set; } = 0;
        public Int32 UserID { get; set; } = 0;
        public string Content { get; set; } = "";
    }

    public class Comment
    {
        public Int32 UserID { get; set; }
        public string Content { get; set; }
        public Int32 TicketID { get; set; }
        public Comment(Int32 ticket_id, Int32 user_id, string content)
        {
            UserID = user_id;
            Content = content;
            TicketID = ticket_id;
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

                    string query = string.Format($"INSERT INTO Comment " +
                        $"(date_created, ticket_id, user_id, content) " +
                        "values (CURDATE(), {0}, {1}, \"{2}\")", TicketID, UserID, Content);

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
