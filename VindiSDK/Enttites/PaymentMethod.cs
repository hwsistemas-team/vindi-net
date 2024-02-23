using System;

namespace Vindi.SDK.Enttites
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string PublicName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
    }
}
