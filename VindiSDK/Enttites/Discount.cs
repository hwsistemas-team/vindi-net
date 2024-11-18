using System;

namespace Vindi.SDK.Enttites
{
    public class Discount
    {
        public int Id { get; set; }
        public string DiscountType { get; set; }
        public double? Percentage { get; set; }
        public double Amount { get; set; }
        public int? Quantity { get; set; }
        public int? Cycles { get; set; }
        public int? ProductItemId { get; set; }
        public string Status { get; set; }
        public DiscountProductItem ProductItem { get; set; }
    }

    public class DiscountProductItem
    {
        public int Id { get; set; }
        public DiscountProduct Product { get; set; }
    }

    public class DiscountProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public object Code { get; set; }
    }

    public class WrapperDiscount
    {
        public Discount Discount { get; set; }
    }
}