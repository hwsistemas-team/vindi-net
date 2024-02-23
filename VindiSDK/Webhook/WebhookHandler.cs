using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vindi.SDK.Enttites;
using Vindi.SDK.Json;

namespace Vindi.SDK.Webhook
{
    public class WebhookHandler
    {
        private Dictionary<string, List<WebhookHandlerInternal>> _handlers;

        public WebhookHandler()
        {
            _handlers = new Dictionary<string, List<WebhookHandlerInternal>>();
        }

        public WebhookHandler OnSubscriptionCreated(Func<WebhookHandlerEvent<Subscription>, Task> handler)
        {
            return AddListenerEvent(EventType.SubscriptionCreated, (oEvent) =>
            {
                return handler(MakeTypedEvent<WrapperSubscription, Subscription>(oEvent, result => result.Subscription));
            });
        }

        public WebhookHandler OnSubscriptionCanceled(Func<WebhookHandlerEvent<Subscription>, Task> handler)
        {
            return AddListenerEvent(EventType.SubscriptionCanceled, (oEvent) =>
            {
                return handler(MakeTypedEvent<WrapperSubscription, Subscription>(oEvent, result => result.Subscription));
            });
        }

        public WebhookHandler OnSubscriptionReactivated(Func<WebhookHandlerEvent<Subscription>, Task> handler)
        {
            return AddListenerEvent(EventType.SubscriptionReactivated, (oEvent) =>
            {
                return handler(MakeTypedEvent<WrapperSubscription, Subscription>(oEvent, result => result.Subscription));
            });
        }

        public WebhookHandler OnBillCreated(Func<WebhookHandlerEvent<Bill>, Task> handler)
        {
            return AddListenerEvent(EventType.BillCreated, (oEvent) =>
            {
                return handler(MakeTypedEvent<WrapperBill, Bill>(oEvent, result => result.Bill));
            });
        }

        public WebhookHandler OnBillCanceled(Func<WebhookHandlerEvent<Bill>, Task> handler)
        {
            return AddListenerEvent(EventType.BillCanceled, (oEvent) =>
            {
                return handler(MakeTypedEvent<WrapperBill, Bill>(oEvent, result => result.Bill));
            });
        }

        public WebhookHandler OnBillPaid(Func<WebhookHandlerEvent<Bill>, Task> handler)
        {
            return AddListenerEvent(EventType.BillPaid, (oEvent) =>
            {
                return handler(MakeTypedEvent<WrapperBill, Bill>(oEvent, result => result.Bill));
            });
        }

        public WebhookHandler OnBillSeen(Func<WebhookHandlerEvent<Bill>, Task> handler)
        {
            return AddListenerEvent(EventType.BillSeen, (oEvent) =>
            {
                return handler(MakeTypedEvent<WrapperBill, Bill>(oEvent, result => result.Bill));
            });
        }

        public WebhookHandler OnChargeCanceled(Func<WebhookHandlerEvent<BillCharge>, Task> handler)
        {
            return AddListenerEvent(EventType.ChargeCanceled, (oEvent) =>
            {
                return handler(MakeTypedEvent<WrapperBillCharge, BillCharge>(oEvent, result => result.Charge));
            });
        }

        public WebhookHandler OnChargeCreated(Func<WebhookHandlerEvent<BillCharge>, Task> handler)
        {
            return AddListenerEvent(EventType.ChargeCreated, (oEvent) =>
            {
                return handler(MakeTypedEvent<WrapperBillCharge, BillCharge>(oEvent, result => result.Charge));
            });
        }

        public WebhookHandler OnChargeRefunded(Func<WebhookHandlerEvent<BillCharge>, Task> handler)
        {
            return AddListenerEvent(EventType.ChargeRefunded, (oEvent) =>
            {
                return handler(MakeTypedEvent<WrapperBillCharge, BillCharge>(oEvent, result => result.Charge));
            });
        }

        public WebhookHandler OnChargeRejected(Func<WebhookHandlerEvent<BillCharge>, Task> handler)
        {
            return AddListenerEvent(EventType.ChargeRejected, (oEvent) =>
            {
                return handler(MakeTypedEvent<WrapperBillCharge, BillCharge>(oEvent, result => result.Charge));
            });
        }

        public WebhookHandler OnPeriodCreated(Func<WebhookHandlerEvent<Period>, Task> handler)
        {
            return AddListenerEvent(EventType.PeriodCreated, (oEvent) =>
            {
                return handler(MakeTypedEvent<WrapperPeriod, Period>(oEvent, result => result.Period));
            });
        }

        public WebhookHandler OnAnyEvent(Func<WebhookHandlerEventAny, Task> handler)
        {
            return AddListenerEvent("any", (oEvent) =>
            {
                return handler(new WebhookHandlerEventAny(oEvent.Data)
                {
                    Type = oEvent.Type,
                    CreatedAt = oEvent.CreatedAt
                });
            });
        }

        public async Task Analyze(string jsonEvent)
        {
            var oEvent = CustomJsonSerializer.Deserialize<WrapperWebhookEvent>(jsonEvent)?.Event;

            if (oEvent == null)
                throw new ArgumentException($"Invalid json object {nameof(WrapperWebhookEvent)}");

            var handlers = new List<WebhookHandlerInternal>();

            if (_handlers.TryGetValue("any", out var handlersSelecteds))
                handlers.AddRange(handlersSelecteds);

            if (_handlers.TryGetValue(oEvent.Type, out handlersSelecteds))
                handlers.AddRange(handlersSelecteds);

            foreach(var handler in handlers)
            {
                await handler(oEvent);
            }
        }

        private WebhookHandler AddListenerEvent(string eventType, WebhookHandlerInternal handler)
        {
            if (string.IsNullOrEmpty(eventType))
                throw new ArgumentNullException(nameof(eventType));

            if (!_handlers.ContainsKey(eventType))
                _handlers.Add(eventType, new List<WebhookHandlerInternal>());

            _handlers[eventType].Add(handler);

            return this;
        }

        private WebhookHandlerEvent<TData> MakeTypedEvent<TWrapper, TData>(WebhookEvent oEvent, Func<TWrapper, TData> unwrapData)
        {
            var dataWrapped = CustomJsonSerializer.Deserialize<TWrapper>(oEvent.Data.ToString());

            return new WebhookHandlerEvent<TData>
            {
                Type = oEvent.Type,
                CreatedAt = oEvent.CreatedAt,
                Data = unwrapData(dataWrapped)
            };
        }
    }

    public class WebhookHandlerEventBase
    {
        public string Type { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }

    public class WebhookHandlerEvent<TEventData> : WebhookHandlerEventBase
    {
        public TEventData Data { get; set; }
    }

    public class WebhookHandlerEventAny : WebhookHandlerEventBase
    {
        private object _data { get; set; }

        public WebhookHandlerEventAny(object data)
        {
            _data = data;
        }

        public TEventData ParseData<TEventData>()
        {
            return CustomJsonSerializer.Deserialize<TEventData>(_data.ToString());
        }
    }

    delegate Task WebhookHandlerInternal(WebhookEvent eventInfo);
}
