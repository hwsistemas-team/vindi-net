using System;
using System.Collections.Generic;

namespace Vindi.SDK.Enttites
{
    public class PricingSchema
    {
        public int Id { get; set; }
        public string ShortFormat { get; set; }
        public double? Price { get; set; }
        public double? MinimumPrice { get; set; }
        public string SchemaType { get; set; }
        public IList<PricingRanx> PricingRanges { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
    }
}
