using System;
namespace FreeERP.Model
{
    public class Customer
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string Domain { get; set; }
        public string PhoneNumber { get; set; }
        public Customer(string name, string address, string companyName, string domain, string phoneNumber)
        {
            Name = name;
            Address = address;
            CompanyName = companyName;
            Domain = domain;
            PhoneNumber = phoneNumber;
        }
        public bool SaveToDB()
        {

        }
    }
}
