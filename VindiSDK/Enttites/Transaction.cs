using System;

namespace Vindi.SDK.Enttites
{
    public class Transaction
    {
        public int Id { get; set; }
        public string TransactionType { get; set; }
        public string Status { get; set; }
        public double? Amount { get; set; }
        public int? Installments { get; set; }
        public string GatewayMessage { get; set; }
        public string GatewayResponseCode { get; set; }
        public string GatewayAuthorization { get; set; }
        public string GatewayTransactionId { get; set; }
        public object GatewayResponseFields { get; set; }
        public int? FraudDetectorScore { get; set; }
        public string FraudDetectorStatus { get; set; }
        public string FraudDetectorId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public Gateway Gateway { get; set; }
        public PaymentProfile PaymentProfile { get; set; }
    }
}
