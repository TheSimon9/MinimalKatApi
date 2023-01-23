using Confluent.Kafka;

WebApplication.CreateBuilder(args);

var conf = new ConsumerConfig
{
    GroupId = "group-name",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

var c = new ConsumerBuilder<Ignore, string>(conf).Build();
c.Subscribe("test");

while (true)
{
    var cr = c.Consume(new CancellationTokenSource().Token);
    Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
}