using System;

namespace Vindi.SDK.Enttites
{
    public class BillCharge
    {
        public int Id { get; set; }
        public double? Amount { get; set; }
        public string Status { get; set; }
        public DateTimeOffset? DueAt { get; set; }
        public DateTimeOffset? PaidAt { get; set; }
        public int? Installments { get; set; }
        public int? AttemptCount { get; set; }
        public DateTimeOffset? NextAttempt { get; set; }
        public string PrintUrl { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Transaction LastTransaction { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Bill Bill { get; set; }
        public Customer Customer { get; set; }
    }

    public class WrapperBillCharge
    {
        public BillCharge Charge { get; set; }
    }
}
