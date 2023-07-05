using System;
namespace FreeERP.Model.Tickets
{
    public class Ticket
    {
        public string? user_id { get; set; }
        public string? date_created { get; set; }
        public TicketType type;
        public Ticket()
        {
        }
    }
    public enum TicketType
    {
        CustomerSuccess,
        Sale,
        Engineer
    }
}

