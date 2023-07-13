using System;
namespace FreeERP.Model.Tickets
{
    public abstract class Ticket
    {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public TicketType type { get; set; }
        public string Status { get; set; }
        public Ticket(string id, TicketType t, DateTime dt, string userID, string content)
        {
            ID = id;
            type = t;
            UserID = userID;
            Content = content;
            DateCreated = dt;
            Status = "Open";
        }
        public Ticket(string id, TicketType t, DateTime dt, string userID, string content, string status)
        {
            ID = id;
            type = t;
            UserID = userID;
            Content = content;
            DateCreated = dt;
            Status = status;
        }
        public abstract string SaveToDB();
    }
    public enum TicketType
    {
        CustomerSuccess,
        Sale,
        Engineer
    }
    public enum TicketStatus
    {
        Open,
        InProgress,
        Pending,
        Resolve
    }
}

