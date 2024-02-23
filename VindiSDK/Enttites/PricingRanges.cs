using System;

namespace Vindi.SDK.Enttites
{
    public class PricingRanx
    {
        public int Id { get; set; }
        public int? StartQuantity { get; set; }
        public int? EndQuantity { get; set; }
        public double? Price { get; set; }
        public double? OveragePrice { get; set; }
    }
}
