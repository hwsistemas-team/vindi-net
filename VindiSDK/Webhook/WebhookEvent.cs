using System;

namespace Vindi.SDK.Webhook
{
    public class WebhookEvent
    {
        public string Type { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public object Data { get; set; }
    }

    class WrapperWebhookEvent
    {
        public WebhookEvent Event { get; set; }
    }
}
