# VINDI SDK .NET

Biblioteca .NET para acesso a API de recorrência da VINDI. Projeto direcionado para .NET Standard 2.0, compatível com .NET Core e .NET Framework.

## Exemplo de uso

Faça referência ao projeto (pacote, dlls ou direto para o código fonte).

```Csharp
var vindi = new VindiService("VindiApiUrl", "VindiApiKey");

var customerQueryParams = new VindRequestParams<Customer>(page: 1, perPage: 10)
    .Filters(x => x.Status == ResourseStatus.Active)
    .OrderByDescending(x => x.Name);

var customersResult = await vindi.Customers.FindAsync(customerQueryParams);
Console.WriteLine(customersResult.Data.FirstOrDefault()?.name);

var productsResult = await vindi.Products.FindAsync(new VindRequestParams<Product>(1, 10));
Console.WriteLine(productsResult.Data.FirstOrDefault()?.name);
```

## Parâmetros de consulta (paginação, filtros e ordenação)

Os filtros podem ser definidos de duas formas:
 - Usando expressões: Crie os filtros de forma tipada, evitando erros de digitação;
 - Usando texto: Crie os filtros como string. **Recomendado somente para operações não suportadas por expressões**.


Exemplos:

```csharp
new VindRequestParams<Customer>(1, 10)
    .Filters(x => x.Status == ResourseStatus.Active)
    .OrderByDescending(x => x.Name);

new VindRequestParams<Customer>(1, 10).Filters(x => x.Code == "myCustomerCode");
new VindRequestParams<Customer>(1, 10).Filters(x => x.Name.Contains("test") && x.Status == ResourseStatus.Active);
new VindRequestParams<Customer>(1, 10).RawFilters("name:\"test\" status=\"active\"");

new VindRequestParams<Customer>(1, 10).OrderByDescending(x => x.Name);
new VindRequestParams<Customer>(1, 10).OrderByAscending(x => x.Name);
```

## Web hooks

Crie um endpoint para receber as chamadas http da VINDI e use a classe `WebhookHandler` para tratar o eventos. Exemplo:

```csharp
var webhookHanlder = new WebhookHandler();

webhookHanlder.OnSubscriptionCreated(xEvent =>
{
    Console.WriteLine(xEvent.Type);
    Console.WriteLine(xEvent.CreatedAt);
    Console.WriteLine(xEvent.Data.Id);
    Console.WriteLine(xEvent.Data.Status);

    return Task.CompletedTask;
});

webhookHanlder.OnAnyEvent(xEvent =>
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
```

## App Teste

Requisitos:
 - .NET 8 SDK

Para executar o app de teste crie um arquivo `VindiSDK.Tests/settings.json` e configue o acesso a API da VINDI:

```json
{
    "VindiApiUrl": "https://sandbox-app.vindi.com.br/api/v1",
    "VindiApiKey": "API-PRIVATE-KEY"
}
```