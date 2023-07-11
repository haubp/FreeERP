namespace FreeERP.UIModel
{
    public class UIModelFactory
    {
        public static UISaleTicket CreateUIModelSaleTicket(Int64 ticketId, Int64 userId, DateTime dateCreated, string product, string content, string status)
        {
            return new UISaleTicket(ticketId, userId, dateCreated, product, content, status);
        }
        public static UIEngineerTicket CreateUIModelEngineerTicket(Int64 ticketId, Int64 userId, DateTime dateCreated, string content, string status)
        {
            return new UIEngineerTicket(ticketId, userId, dateCreated, content, status);
        }
        public static UICustomerSuccessTicket CreateUIModelCustomerSuccessTicket(Int64 ticketId, Int64 userId, DateTime dateCreated, string content, string status)
        {
            return new UICustomerSuccessTicket(ticketId, userId, dateCreated, content, status);
        }
    }
}
