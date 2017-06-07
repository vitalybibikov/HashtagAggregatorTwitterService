using System;
using Hangfire;
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

        [HttpGet("start/{hashtag:required}")]
        public IActionResult Start(string hashtag)
        {
            worker.Start(hashtag);
            return Ok();
        }

        [HttpGet("stop/{hashtag:required}")]
        public IActionResult Stop(string hashtag)
        {
            worker.Stop(hashtag);
            return Ok();
        }
    }
}