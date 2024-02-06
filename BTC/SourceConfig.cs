namespace BTC
{
    public class SourceConfig
    {
        public string Name { get; set; }
        public string ApiUrl { get; set; }
    }

    public class SourceConfigs
    {
        public IList<SourceConfig> Sources { get; set; }
    }
}
