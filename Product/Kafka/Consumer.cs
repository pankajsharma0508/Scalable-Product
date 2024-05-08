using Confluent.Kafka;

namespace Payment.Kafka
{
    public class Consumer: BackgroundService
    {
        const string topic = "scaleble-kafka-topic";
        IConfiguration configuration;

        public Consumer()
        {
            configuration = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory())
         .AddIniFile("client.properties", false)
         .Build();
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Run(() =>
                {
                    configuration["group.id"] = "csharp-group-1";
                    configuration["auto.offset.reset"] = "earliest";
                    
                    // creates a new consumer instance 
                    using (var consumer = new ConsumerBuilder<string, string>(configuration.AsEnumerable()).Build())
                    {
                        consumer.Subscribe(topic);
                        while (true)
                        {
                            // consumes messages from the subscribed topic and prints them to the console
                            var cr = consumer.Consume();
                            Console.WriteLine($"Consumed event from topic {topic}: key = {cr.Message.Key,-10} value = {cr.Message.Value}");
                        }
                    }
                });
            }
        }
    }
}
