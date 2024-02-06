namespace BTC.Data
{
    public interface IDataService
    {
        Task SaveData(string sourceName, IDictionary<string, object> data);

        Task<IEnumerable<IDictionary<string, object>>> GetAllData();

        Task<IDictionary<string, object>> GetLastPrices(string sourceName);
    }
}
