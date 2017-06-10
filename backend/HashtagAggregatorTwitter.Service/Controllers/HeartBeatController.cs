using System.Threading.Tasks;
using HashtagAggregatorTwitter.Contracts;
using HashtagAggregatorTwitter.Contracts.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HashtagAggregatorTwitter.Service.Controllers
{
    [Route("api/[controller]")]
    public class HeartBeatController : Controller
    {
        private readonly IBackgroundServiceWorker worker;

        public HeartBeatController(IBackgroundServiceWorker worker)
        {
            this.worker = worker;
        }

        [HttpGet("start/{hashtag:required}")]
        public async Task<IActionResult> Start(string hashtag)
        {
            var result = await worker.Start(hashtag);
            return Ok(result);
        }

        [HttpGet("stop/{hashtag:required}")]
        public IActionResult Stop(string hashtag)
        {
            worker.Stop(hashtag);
            return Ok();
        }
    }
}