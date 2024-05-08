using Confluent.Kafka;

namespace Payment.Kafka
{
    public static class Producer
    {
        const string topic = "scaleble-kafka-topic";
        static IConfiguration configuration;

        static Producer()
        {
            configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddIniFile("client.properties", false)
            .Build();
        }

        public static void ProduceMessage()
        {
            using (var producer = new ProducerBuilder<string, string>(configuration.AsEnumerable()).Build())
            {
                producer.Produce(topic, new Message<string, string> { Key = "key", Value = "value" },
                  (deliveryReport) =>
                  {
                      if (deliveryReport.Error.Code != ErrorCode.NoError)
                      {
                          Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                      }
                      else
                      {
                          Console.WriteLine($"Produced event to topic {topic}: key = {deliveryReport.Message.Key,-10} value = {deliveryReport.Message.Value}");
                      }
                  }
                );
                producer.Flush(TimeSpan.FromSeconds(10));
            }
        }
    }
}
