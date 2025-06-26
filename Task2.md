# 🚀 Task 2: Conceptual Kafka Integration for Product Creation

## 📖 Scenario

When a new product is successfully created in the API, a message should be published to a Kafka topic to notify other services.

---

## ⚙️ 1. Kafka Producer Configuration

In a real .NET application, you would use the [Confluent.Kafka](https://github.com/confluentinc/confluent-kafka-dotnet) NuGet package.  
Here’s how you would configure a Kafka producer:

```csharp
var config = new ProducerConfig { BootstrapServers = "localhost:9092" }; var producer = new ProducerBuilder<Null, string>(config).Build();
```

---

## 📝 2. Message Structure

The message published to Kafka should represent the product in **JSON** format for interoperability and ease of use:

```csharp
{ "id": 1, "name": "Laptop", "description": "A fast laptop", "price": 1200.00 }
```

---

## 💻 3. Publishing to Kafka (C# Example)

Below is a conceptual example of how you would serialize the product and publish it to a Kafka topic named `products-created`:


```csharp
using Confluent.Kafka; using System.Text.Json; using ProductsAPI.Models;

public class ProductPublisher { private readonly IProducer<Null, string> _producer;
public ProductPublisher(IProducer<Null, string> producer)
{
    _producer = producer;
}

public async Task PublishProductCreatedAsync(Product product)
{
    string message = JsonSerializer.Serialize(product);

    try
    {
        var result = await _producer.ProduceAsync(
            "products-created",
            new Message<Null, string> { Value = message }
        );
        // Optionally log result
    }
    catch (ProduceException<Null, string> ex)
    {
        // Handle error, e.g., log and implement retry logic
        int retries = 3;
        while (retries-- > 0)
        {
            try
            {
                await _producer.ProduceAsync("products-created", new Message<Null, string> { Value = message });
                break;
            }
            catch
            {
                if (retries == 0) 
				throw;
				
                await Task.Delay(500);
            }
        }
    }
}
```

---

## 🛡️ 4. Error Handling & Retry

- Use `try/catch` around `ProduceAsync`.
- Log all failures for monitoring and alerting.

---

## 📦 5. Serialization Format

- **JSON** is chosen for its simplicity, human readability, and broad support across platforms.
- For stricter schema enforcement and better performance, formats like **Avro** or **Protobuf** can be considered in larger systems.

---

## 🗂️ 6. Topic Partitioning

- Partitioning can be based on the product ID to ensure even distribution and ordering guarantees for the same product.
- Example: Use `product.Id.ToString()` as the message key:

```csharp
await _producer.ProduceAsync( "products-created", new Message<string, string> { Key = product.Id.ToString(), Value = message } );
```

- 
This ensures all messages for the same product go to the same partition, preserving order.

---

## 📝 Summary

- **Configure** the Kafka producer using Confluent.Kafka.
- **Serialize** product data to JSON.
- **Publish** to a topic (e.g., `products-created`) with error handling and retry logic.
- **Partition** by product ID for ordering.
- **JSON** is used for interoperability; consider Avro/Protobuf for larger systems.

> ⚠️ *This is a conceptual guide. Actual implementation would require proper dependency injection, configuration, and robust error handling in production.*