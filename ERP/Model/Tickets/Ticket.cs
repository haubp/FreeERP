using System;
namespace FreeERP.Model.Tickets
{
    public abstract class Ticket
    {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public TicketType Type { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public Ticket(string id, TicketType t, DateTime dt, string userID, string content, string priority)
        {
            ID = id;
            Type = t;
            UserID = userID;
            Content = content;
            DateCreated = dt;
            Status = "Open";
            Priority = priority;
        }
        public Ticket(string id, TicketType t, DateTime dt, string userID, string content, string status, string priority)
        {
            ID = id;
            Type = t;
            UserID = userID;
            Content = content;
            DateCreated = dt;
            Status = status;
            Priority = priority;
        }
        public abstract string SaveToDB();
    }
    public enum TicketType
    {
        CustomerSuccess,
        Sale,
        Engineer,
        Customer
    }
    public enum TicketStatus
    {
        Open,
        InProgress,
        Pending,
        Resolve
    }
}

