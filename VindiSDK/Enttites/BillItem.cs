using System;

namespace Vindi.SDK.Enttites
{
    public class BillItem
    {
        public int Id { get; set; }
        public double? Amount { get; set; }
        public int? Quantity { get; set; }
        public int? PricingRangeId { get; set; }
        public string Description { get; set; }
        public PricingSchema PricingSchema { get; set; }
        public ProductView1 Product { get; set; }
        public ProductItem ProductItem { get; set; }
        public Discount Discount { get; set; }
    }
}
