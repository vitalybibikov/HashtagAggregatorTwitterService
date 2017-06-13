using System.Collections.Generic;

using Hangfire;
using Hangfire.Storage;
using HashtagAggregator.Service.Contracts;

namespace HahtagAggregatorTwitter.Storage
{
    public class HangfireStorageAccessor : IStorageAccessor
    {
        public List<RecurringJobDto> GetJobsList()
        {
            return JobStorage.Current.GetConnection().GetRecurringJobs();
        }

        public void CancelRecurringJobs()
        {
            var jobs = JobStorage.Current.GetConnection().GetRecurringJobs();
            jobs.Clear();
        }
    }
}