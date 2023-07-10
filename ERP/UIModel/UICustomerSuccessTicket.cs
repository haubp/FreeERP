namespace FreeERP.UIModel
{
    public class UICustomerSuccessTicket
    {
        public Int64 TicketID { get; set; }
        public Int64 UserID { get; set; }
        public DateTime DateCreated { get; set; }
        public string Content { get; set; }
        public UICustomerSuccessTicket(long ticketID, long userID, DateTime dateCreated, string content)
        {
            TicketID = ticketID;
            UserID = userID;
            DateCreated = dateCreated;
            Content = content;
        }
    }
}
