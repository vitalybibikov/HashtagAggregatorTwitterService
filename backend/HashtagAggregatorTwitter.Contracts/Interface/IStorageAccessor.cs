using System.Collections.Generic;
using Hangfire.Storage;

namespace HashtagAggregatorTwitter.Contracts.Interface
{
    public interface IStorageAccessor
    {
        List<RecurringJobDto> GetJobsList();
    }
}
