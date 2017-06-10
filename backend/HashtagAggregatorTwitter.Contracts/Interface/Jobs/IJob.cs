using System;
using System.Threading.Tasks;
using HashtagAggregator.Core.Contracts.Interface.Cqrs.Command;
using HashtagAggregator.Shared.Common.Infrastructure;

namespace HashtagAggregatorTwitter.Contracts.Interface.Jobs
{
    public interface IJob
    {
        string JobId { get; }

        int Interval { get; set; }

        HashTagWord Tag { get; set; }

        Task<ICommandResult> Execute();
    }
}