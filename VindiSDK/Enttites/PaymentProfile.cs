using System;
using System.Collections.Generic;

namespace Vindi.SDK.Enttites
{
    public class PaymentProfile
    {
        public int Id { get; set; }
        public string HolderName { get; set; }
        public string RegistryCode { get; set; }
        public string BankBranch { get; set; }
        public string BankAccount { get; set; }
        public string CardExpiration { get; set; }
        public string CardNumber { get; set; }
        public string CardNumberFirstSix { get; set; }
        public string CardNumberLastFour { get; set; }
        public string CardCvv { get; set; }
        public string PaymentMethodCode { get; set; }
        public string PaymentCompanyCode { get; set; }
        public string Token { get; set; }
        public int? CustomerId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public PaymentCompany PaymentCompany { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }

    public class WrapperPaymentProfile
    {
        public PaymentProfile PaymentProfile;
    }

    public class WrapperPaymentProfiles
    {
        public IEnumerable<PaymentProfile> PaymentProfiles;
    }
}
