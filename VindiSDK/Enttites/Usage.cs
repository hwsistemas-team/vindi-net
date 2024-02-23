using System;
using System.Collections.Generic;

namespace Vindi.SDK.Enttites
{
    public class Usage
    {
        public int Id { get; set; }
        public int? Quantity { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public ProductItem ProductItem { get; set; }
        public Bill Bill { get; set; }
        public int? PeriodId { get; set; }
        public int? ProductId { get; set; }
        public int? ProductItemId { get; set; }
    }

    public class WrapperUsage
    {
        public Usage Usage { get; set; }
    }

    public class WrapperUsages
    {
        public IEnumerable<Usage> Usages { get; set; }
    }
}
