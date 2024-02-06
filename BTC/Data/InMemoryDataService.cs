namespace BTC.Data
{
    public class InMemoryDataService : IDataService
    {
        public IDictionary<string, IList<IDictionary<string, object>>> Data { get; set; } = new Dictionary<string, IList<IDictionary<string, object>>> ();


        public Task SaveData(string sourceName, IDictionary<string, object> values)
        {
            if (Data.ContainsKey(sourceName))
            {
                Data[sourceName].Add(values);
            }
            else
            {
                Data.Add(sourceName, new List<IDictionary<string, object>>() { values});
            }

            return Task.CompletedTask;
        }

        public Task<IEnumerable<IDictionary<string, object>>> GetAllData()
        {
            var result = new List<IDictionary<string, object>>();
            foreach (var data in Data)
            {
                result.AddRange(data.Value);
            }

            return Task.FromResult<IEnumerable<IDictionary<string, object>>>(result);
        }

        public Task<IDictionary<string, object>> GetLastPrices(string sourceName)
        {
            if (!Data.ContainsKey(sourceName))
            {
                return default;
            }
            var data = Data[sourceName];

            var result = data.OrderByDescending(m => m["timestamp"]).FirstOrDefault();

            return Task.FromResult(result);
        }


    }
}
