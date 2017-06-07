using System;
using HashtagAggregatorTwitter.Contracts;
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

        [HttpGet("start")]
        public IActionResult Get(string hashtag)
        {
            worker.Start();
            return Ok();
        }
    }
}