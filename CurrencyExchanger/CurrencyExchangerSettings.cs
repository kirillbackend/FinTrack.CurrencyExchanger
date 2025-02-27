namespace CurrencyExchanger
{
    public class CurrencyExchangerSettings
    {
        public KafkaData Kafka { get; set; }

        public class KafkaData
        {
            public string BootstrapServers { get; set; }
            public string ConvertResponseGroupId { get; set; }
            public string ConvertRequestGroupId { get; set; }
            public string FinTrackTopic { get; set; }
            public string FinTrackCurrencyExchanger { get; set; }
        }
    }
}
