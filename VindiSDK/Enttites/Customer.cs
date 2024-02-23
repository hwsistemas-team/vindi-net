using System;
using System.Collections.Generic;

namespace Vindi.SDK.Enttites
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string RegistryCode { get; set; }
        public string Code { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public Address Address { get; set; }
        public IList<Phone> Phones { get; set; }
    }

    public class WrapperCustomers
    {
        public IEnumerable<Customer> Customers { get; set; }
    }

    public class WrapperCustomer
    {
        public Customer Customer { get; set; }
    }
}
