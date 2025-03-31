using Confluent.Kafka;
using CurrencyExchanger.Wrapper.Contracts;

namespace CurrencyExchanger.Kafka
{
    public class KafkaConsumer : BackgroundService
    {
        private readonly char _separator = ':';

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                _ = ConsumeAsync("fintrackcurrencyexchanger-topic", stoppingToken);
            }, stoppingToken);
        }

        public async Task ConsumeAsync(string topic, CancellationToken stoppingToken)
        {
            var wrapper = new FixerAPIWrapper();

            var config = new ConsumerConfig
            {
                GroupId = "fintrackConvertResponse",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            using var consumer = new ConsumerBuilder<string, string>(config).Build();
            consumer.Subscribe(topic);

            var count = 0;

            var _producer = new KafkaProducer();

            while (!stoppingToken.IsCancellationRequested)
            {
                var consumerResult = consumer.Consume(stoppingToken);

                var array = consumerResult.Value.Split(_separator);
                var valueResult = await wrapper.ConvertCurrency(array[0], array[1], array[2]);
                Console.WriteLine($"valueResult = {valueResult}");

                var resultValue = consumerResult.Message.Value;
                var resultKey = consumerResult.Message.Key;
                Console.WriteLine($"Key = {resultKey} \nValue = {resultValue}");
                await _producer.ProduceAsync("fintrack-topic", new Message<string, string>
                {
                    Key = DateTime.Now.ToString(),
                    Value = valueResult.ToString()
                });
            }

            consumer.Close();
        }
    }
}
