using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace BTC.Controllers
{
    [ApiController]
    [Route("api/prices/[controller]")]
    public class BitCoinPriceController : ControllerBase
    {
        private readonly BitCoinPriceManager bitCoinPriceManager;

        public BitCoinPriceController(BitCoinPriceManager bitCoinPriceManager)
        {
            this.bitCoinPriceManager = bitCoinPriceManager;
        }
        [HttpGet]
        [Route("sources")]
        public async Task<IActionResult> GetSources()
        {
            var result = bitCoinPriceManager.GetSources();

            return Ok(result);
        }

        [HttpGet]
     
        public async Task<IActionResult> GetPrice([FromQuery] string sourceName)
        {
            var result = await bitCoinPriceManager.GetBitCoinPrice(sourceName);

            return Ok(result);
        }

        [HttpGet]
        [Route("history")]
        public async Task<IActionResult> GetHistory()
        {
            var result = await bitCoinPriceManager.GetBitCoinPriceHistroy();

            return Ok(result);
        }

        [HttpGet]
        [Route("last")]
        public async Task<IActionResult> GetLastPrices([FromQuery] string sourceName)
        {
            var result = await bitCoinPriceManager.GetLastPrices(sourceName);

            return Ok(result);
        }
    }
}
