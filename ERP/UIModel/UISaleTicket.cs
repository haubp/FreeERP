namespace FreeERP.UIModel
{
    public class UISaleTicket
    {
        public Int64 TicketID { get; set; }
        public Int64 UserID { get; set; }
        public string Product { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public UISaleTicket(Int64 ticketId, long userID, DateTime dateCreated, string product, string content)
        {
            TicketID = ticketId;
            UserID = userID;
            Product = product;
            Content = content;
            DateCreated = dateCreated;
        }
    }
}
