using System;
namespace FreeERP.Model
{
    public class Customer
    {
        public string UserID { get; set; }
        public Customer(string userID)
        {
            UserID = userID;
        }
        public List<string> Tickets()
        {
            // Query ticket created by that user
            return new List<string>();
        }
    }
}
