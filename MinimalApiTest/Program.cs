using Confluent.Kafka;

WebApplication.CreateBuilder(args);

string Latency(string value)
{
    var createdAt = new DateTime(long.Parse(value.Split('-')[1]));
    var now = DateTime.Now;

    return $"{(now - createdAt).TotalMilliseconds}ms";
}

var conf = new ConsumerConfig
{
    GroupId = "dispatcher",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

var c = new ConsumerBuilder<Ignore, string>(conf).Build();
c.Subscribe("dispatch-provider");

while (true)
{
    var cr = c.Consume(new CancellationTokenSource().Token);
    Console.WriteLine($"Consumed message '{cr.Value}' after: '{Latency(cr.Value)}'.");
}