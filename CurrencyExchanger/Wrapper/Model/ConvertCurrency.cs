namespace CurrencyExchanger.Wrapper.Model
{
    public class ConvertCurrency
    {
        public bool Success { get; set; }
        public Query Query { get; set; }
        public Info Info { get; set; }
        public DateTime Date { get; set; }
        public string Result { get; set; }
    }

    public class Query
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Amount { get; set; }
    }

    public class Info
    {
        public string Timestamp { get; set; }
        public string Rate { get; set; }
    }
}
