using System;

namespace HashtagAggregatorTwitter.Contracts
{
    public interface IBackgroundServiceWorker
    {
        void Start(string tag);

        void Stop(string tag);
    }
}