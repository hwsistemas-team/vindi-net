using System;
using System.Collections.Generic;

namespace Vindi.SDK.Enttites
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Unit { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Invoice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public PricingSchema PricingSchema { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }

    public class WrapperProducts
    {
        public IEnumerable<Product> Products { get; set; }
    }

    public class WrapperProduct
    {
        public Product Product { get; set; }
    }
}