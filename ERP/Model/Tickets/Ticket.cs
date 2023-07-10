using System;
namespace FreeERP.Model.Tickets
{
    public abstract class Ticket
    {
        public string UserID { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public TicketType type { get; set; }
        public Ticket(TicketType t, DateTime dt, string userID, string content)
        {
            type = t;
            UserID = userID;
            Content = content;
            DateCreated = dt;
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

