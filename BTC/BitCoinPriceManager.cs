using BTC.Data;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata.Ecma335;

namespace BTC
{
    public class BitCoinPriceManager
    {
        private readonly SourceConfigs _sourceConfigs;
        private readonly IDictionary<string, IBitCoinDataSource> _bitCoinDataSources;
        private readonly IDataService _dataService;

        public BitCoinPriceManager(IOptionsMonitor<SourceConfigs> sourceConfigs, IEnumerable<IBitCoinDataSource> bitCoinDataSources, IDataService dataService)
        {
            this._sourceConfigs = sourceConfigs.CurrentValue;
            if (bitCoinDataSources != null)
            {
                _bitCoinDataSources = bitCoinDataSources.ToDictionary(m => m.SourceName);
            }
            else
            {
                _bitCoinDataSources = new Dictionary<string, IBitCoinDataSource>();
            }
            _dataService = dataService;
        }

        public IEnumerable<SourceConfig> GetSources()
        {
            return _sourceConfigs.Sources;
        }

        public async Task<IDictionary<string, object>> GetBitCoinPrice(string sourceName)
        {
            if (!_bitCoinDataSources.ContainsKey(sourceName))
            {
                throw new NotImplementedException($"Source Name {sourceName} provided is not implemented");
            }
            var data = await _bitCoinDataSources[sourceName].GetData();

            await _dataService.SaveData(sourceName, data);

            return data;
        }

        public async Task<IEnumerable<IDictionary<string,object>>> GetBitCoinPriceHistroy()
        {
            return await _dataService.GetAllData();
        }

        public async Task<IDictionary<string, object>> GetLastPrices(string sourceName)
        {
            return await _dataService.GetLastPrices(sourceName);
        }

    }
}
