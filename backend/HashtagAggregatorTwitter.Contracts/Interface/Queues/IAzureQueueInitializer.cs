using Microsoft.WindowsAzure.Storage.Queue;

namespace HashtagAggregatorTwitter.Contracts.Interface.Queues
{
    public interface IAzureQueueInitializer
    {
        CloudQueue Queue { get; }
    }
}
