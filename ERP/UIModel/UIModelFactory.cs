namespace FreeERP.UIModel
{
    public class UIModelFactory
    {
        public static UISaleTicket CreateUIModelSaleTicket(Int64 ticketId, Int64 userId, DateTime dateCreated, string product, string content)
        {
            return new UISaleTicket(ticketId, userId, dateCreated, product, content);
        }
        public static UIEngineerTicket CreateUIModelEngineerTicket(Int64 ticketId, Int64 userId, DateTime dateCreated, string content)
        {
            return new UIEngineerTicket(ticketId, userId, dateCreated, content);
        }
        public static UICustomerSuccessTicket CreateUIModelCustomerSuccessTicket(Int64 ticketId, Int64 userId, DateTime dateCreated, string content)
        {
            return new UICustomerSuccessTicket(ticketId, userId, dateCreated, content);
        }
    }
}
