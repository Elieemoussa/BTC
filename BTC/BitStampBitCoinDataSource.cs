using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BTC
{
    public class BitStampBitCoinDataSource : IBitCoinDataSource
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly SourceConfig _sourceConfig;

        public BitStampBitCoinDataSource(IHttpClientFactory httpClientFactory, IOptionsMonitor<SourceConfigs> sourceConfigs)
        {
            this.httpClientFactory = httpClientFactory;
            if (!sourceConfigs.CurrentValue.Sources.Any(m => m.Name == SourceName))
            {
                throw new ArgumentNullException(nameof(sourceConfigs));
            }
            else
            {
                _sourceConfig = sourceConfigs.CurrentValue.Sources.Single(m => m.Name == SourceName);
            }

        }
        public string SourceName
        {
            get => "BitStamp";
        }

        public async Task<IDictionary<string, object>> GetData()
        {
            using(var httpClient = httpClientFactory.CreateClient())
            {
                var result = await httpClient.GetAsync(_sourceConfig.ApiUrl);
                result.EnsureSuccessStatusCode();
                string responseBody = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IDictionary<string, object>>(responseBody);
            }
        }
    }
}
