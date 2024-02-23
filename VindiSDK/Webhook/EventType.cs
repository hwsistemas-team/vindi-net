
namespace Vindi.SDK.Webhook
{
    public static class EventType
    {
        public const string SubscriptionCanceled = "subscription_canceled";
        public const string SubscriptionCreated = "subscription_created";
        public const string SubscriptionReactivated = "subscription_reactivated";
        public const string ChargeCreated = "charge_created";
        public const string ChargeRefunded = "charge_refunded";
        public const string ChargeCanceled = "charge_canceled";
        public const string ChargeRejected = "charge_rejected";
        public const string BillCreated = "bill_created";
        public const string BillPaid = "bill_paid";
        public const string BillCanceled = "bill_canceled";
        public const string BillSeen = "bill_seen";
        public const string PeriodCreated = "period_created";
        public const string IssueCreated = "issue_created";
        public const string PaymentProfileCreated = "payment_profile_created";
        public const string MessageSeen = "message_seen";
        public const string Test = "test";
    }
}
