using System;
using System.Collections.Generic;

namespace Vindi.SDK.Enttites
{
    public class Bill
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public double? Amount { get; set; }
        public int? Installments { get; set; }
        public string Status { get; set; }
        public string SeenAt { get; set; }
        public DateTimeOffset? BillingAt { get; set; }
        public DateTimeOffset? DueAt { get; set; }
        public string Url { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public IList<BillItem> BillItems { get; set; }
        public IList<BillCharge> Charges { get; set; }
        public Customer Customer { get; set; }
        public Period Period { get; set; }
        public Subscription Subscription { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public PaymentProfile PaymentProfile { get; set; }
        public object PaymentCondition { get; set; }


        public int? SubscriptionId { get; set; }
        public int? CustomerId { get; set; }
        public int? PaymentMethodId { get; set; }
        public int? PeriodId { get; set; }
    }

    public class WrapperBill
    {
        public Bill Bill { get; set; }
    }

    public class WrapperBills
    {
        public Bill[] Bills { get; set; }
    }
}
