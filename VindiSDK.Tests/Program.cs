using Microsoft.Extensions.Configuration;
using Vindi.SDK.Enttites;
using Vindi.SDK.Services;
using Vindi.SDK.Webhook;

var config = new ConfigurationBuilder()
    .AddJsonFile("settings.json")
    .Build();

await TestVindiService();
await TestVindiWebhook();

async Task TestVindiService()
{
    var vindi = new VindiService(config["VindiApiUrl"], config["VindiApiKey"]);

    // var result0 = await vindi.Customers.CreateAsync(new Customer
    // {
    //     Name = "Test 001",
    //     RegistryCode = "00011122233",
    //     Phones = new List<Phone>
    //     {
    //         new Phone
    //         {
    //             PhoneType = PhoneType.Mobile,
    //             Number = "00911223344"
    //         }
    //     }
    // });

    // var result1 = await vindi.Bills.FindAsync(new VindRequestParams<Bill>(1, 24)
    //         .Filters(x => x.SubscriptionId == 100 && !(x.Id == 5))
    //         .OrderByDescending(x => x.CreatedAt));

    var customersResult = await vindi.Customers.FindAsync(new VindRequestParams<Customer>(1, 10));

    Console.WriteLine($"Total customers : " + customersResult.Total);
    Console.WriteLine($"Rate Limit      : " + customersResult.Headers.RateLimitLimit);
    Console.WriteLine($"Rate Remaining  : " + customersResult.Headers.RateLimitRemaining);
    Console.WriteLine($"Rate Reset      : " + customersResult.Headers.RateLimitReset);
    Console.WriteLine($"Retry After     : " + customersResult.Headers.RetryAfter);
    Console.WriteLine($"First customer  : " + customersResult.Data.FirstOrDefault()?.Name);
}

async Task TestVindiWebhook()
{
    var webhookHanlder = new WebhookHandler();

    webhookHanlder.OnSubscriptionCreated((xEvent) =>
    {
        Console.WriteLine(xEvent.Type);
        Console.WriteLine(xEvent.CreatedAt);
        Console.WriteLine(xEvent.Data.Id);
        Console.WriteLine(xEvent.Data.Status);

        return Task.CompletedTask;
    });

    webhookHanlder.OnAnyEvent((xEvent) =>
    {
        Console.WriteLine(xEvent.Type);
        Console.WriteLine(xEvent.CreatedAt);

        if (xEvent.Type == EventType.SubscriptionCreated)
        {
            var sub = xEvent.ParseData<WrapperSubscription>().Subscription;
            Console.WriteLine(sub.Id);
            Console.WriteLine(sub.Status);
        }

        return Task.CompletedTask;
    });

    var webhookEventJson = """
    {
        "event":{
            "type":"subscription_created",
            "created_at":"2024-02-24T15:00:00.000-03:00",
            "data":{
                "subscription":{
                    "id": 100,
                    "status": "active"
                }
            }
        }
    }
    """;

    await webhookHanlder.Analyze(webhookEventJson);
}
