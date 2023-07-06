using System;
namespace FreeERP.Model
{
    public class Credential
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public AccountType type;
        public Credential(string userName, string password)
        {
            UserName = userName;
            Password = password;
            CreateUser();
        }
        public Credential(string userID)
        {
            QueryUser();
        }
        public bool CreateUser()
        {
            type = AccountType.Customer;
            return true;
        }
        public string QueryUser()
        {
            type = AccountType.Customer;
            return "";
        }
    }

    public enum AccountType
    {
        Customer,
        CustomerSuccess,
        Sale,
        Engineer
    }
}
