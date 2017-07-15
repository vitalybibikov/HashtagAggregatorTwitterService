using System.Collections.Generic;
using Hangfire;
using Hangfire.Storage;
using HashtagAggregator.Service.Contracts;
using System.Linq;
using System;

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
            foreach (var recurringJob in jobs)
            {
                RecurringJob.RemoveIfExists(recurringJob.Id);
            }
        }
    }
}