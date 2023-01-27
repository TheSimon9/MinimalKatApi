using Confluent.Kafka;

WebApplication.CreateBuilder(args);

double Latency(string dateSent)
{
    var createdAt = new DateTime(long.Parse(dateSent));
    var now = DateTime.Now;

    return (now - createdAt).TotalMilliseconds;
}

double Percentile(double percentile, List<double> values)
{
    values = values.OrderBy(x=>x).ToList();
    var k = percentile / 100 * values.Count;

    if (values.Count <= 1)
        return values.First();
    
    if (k % 1 == 0)
        return (values.ElementAt((int)k-1) + values.ElementAt((int)k)) / 2;
    
    return Math.Ceiling(values.ElementAt((int)k-1));
}

var conf = new ConsumerConfig
{
    GroupId = "dispatcher",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

var c = new ConsumerBuilder<Ignore, string>(conf).Build();
c.Subscribe("dispatch-provider");

var latencyList = new List<double>();
while (true)
{

    var cr = c.Consume(new CancellationTokenSource().Token);
    var messageNumber = cr.Value.Split("-")[0];
    var messageDateSent = cr.Value.Split("-")[1];

    var latency = Latency(messageDateSent);
    
    latencyList.Add(latency);
    
    Console.WriteLine($"Consumed message '{messageNumber}' - latency: '{latency}' - percentile50: '{Percentile(50,latencyList)} - percentile90: '{Percentile(90,latencyList)}'");
}