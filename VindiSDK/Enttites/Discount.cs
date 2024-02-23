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
    }
}