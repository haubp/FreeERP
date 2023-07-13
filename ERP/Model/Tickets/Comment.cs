using MySql.Data.MySqlClient;

namespace FreeERP.Model.Tickets
{
    public class CommentFactory
    {
        public static string CreateComment(string ticket_id, string user_id, string content)
        {
            Comment comment = new(ticket_id, user_id, content);
            return comment.SaveToDB();
        }
        public static List<string> QueryCommentByTicketId(string ticket_id)
        {
            List<string> comments = new List<string>();

            string connectionString = "Server=localhost;Database=freeerp;Uid=root;";
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

                            comments.Add($"{user_id} : {content}");
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
        public string? TicketID { get; set; }
        public string? UserID { get; set; }
        public string? Content { get; set; }
    }

    public class Comment
    {
        public string UserID { get; set; }
        public string Content { get; set; }
        public string TicketID { get; set; }
        public Comment(string ticket_id, string user_id, string content)
        {
            UserID = user_id;
            Content = content;
            TicketID = ticket_id;
        }
        public string SaveToDB()
        {
            string connectionString = "Server=localhost;Database=freeerp;Uid=root;";
            string dbError = "";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = string.Format($"INSERT INTO Comment " +
                        $"(date_created, ticket_id, user_id, content) " +
                        "values (CURDATE(), {0}, \"{1}\", \"{2}\", \"Sale\")", Convert.ToInt32(TicketID), Convert.ToInt32(UserID), Content);

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
