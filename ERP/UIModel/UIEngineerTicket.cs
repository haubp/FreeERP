﻿namespace FreeERP.UIModel
{
    public class UIEngineerTicket
    {
        public Int64 TicketID { get; set; }
        public Int64 UserID { get; set; }
        public DateTime DateCreated { get; set; }
        public string Content { get; set; }
        public UIEngineerTicket(Int64 ticketId,  Int64 userID, DateTime dateCreated, string content)
        {
            TicketID = ticketId;
            UserID = userID;
            DateCreated = dateCreated;
            Content = content;
        }
    }
}