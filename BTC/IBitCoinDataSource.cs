namespace BTC
{
    public interface IBitCoinDataSource
    {
        string SourceName { get; }
        Task<IDictionary<string, object>> GetData();
    }
}
