using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage.Queue;

namespace HashtagAggregatorTwitter.Contracts.Queues
{
    public interface IAzureQueueInitializer
    {
        CloudQueue Queue { get; }
    }
}
