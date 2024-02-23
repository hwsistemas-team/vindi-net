using System;
using System.Collections.Generic;

namespace Vindi.SDK.Enttites
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTimeOffset? StartAt { get; set; }
        public DateTimeOffset? EndAt { get; set; }
        public DateTimeOffset? NextBillingAt { get; set; }
        public DateTimeOffset? OverdueSince { get; set; }
        public string Code { get; set; }
        public DateTimeOffset? CancelAt { get; set; }
        public string Interval { get; set; }
        public int? IntervalCount { get; set; }
        public string BillingTriggerType { get; set; }
        public int? BillingTriggerDay { get; set; }
        public int? BillingCycles { get; set; }
        public int? Installments { get; set; }
        public int? PlanId { get; set; }
        public int? CustomerId { get; set; }
        public string PaymentMethodCode { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public Customer Customer { get; set; }
        public Plan Plan { get; set; }
        public IEnumerable<ProductItem> ProductItems { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Period CurrentPeriod { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public PaymentProfile PaymentProfile { get; set; }
    }

    public class WrapperSubscription
    {
        public Subscription Subscription { get; set; }
    }

    public class WrapperSubscriptions
    {
        public IEnumerable<Subscription> Subscriptions { get; set; }
    }
}
