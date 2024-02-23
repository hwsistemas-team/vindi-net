using System;
using System.Collections.Generic;

namespace Vindi.SDK.Enttites
{
    public class Period
    {
        public int Id { get; set; }
        public DateTimeOffset? BillingAt { get; set; }
        public int? Cycle { get; set; }
        public DateTimeOffset? StartAt { get; set; }
        public DateTimeOffset? EndAt { get; set; }
        public int? Duration { get; set; }
        public int? SubscriptionId { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public Customer Customer { get; set; }
        public Subscription Subscription { get; set; }
        public IEnumerable<Usage> Usages { get; set; }
    }

    public class WrapperPeriod
    {
        public Period Period { get; set; }
    }

    public class WrapperPeriods
    {
        public IEnumerable<Period> Periods { get; set; }
    }
}
