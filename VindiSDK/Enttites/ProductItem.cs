using System;
using System.Collections.Generic;

namespace Vindi.SDK.Enttites
{
    public class ProductItem
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string Status { get; set; }
        public int? Cycles { get; set; }
        public int? Quantity { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public ProductView1 Product { get; set; }
        public PricingSchema PricingSchema { get; set; }
        public IList<Discount> Discounts { get; set; }
    }

    public class WrapperProductItem
    {
        public ProductItem ProductItem { get; set; }
    }
}
