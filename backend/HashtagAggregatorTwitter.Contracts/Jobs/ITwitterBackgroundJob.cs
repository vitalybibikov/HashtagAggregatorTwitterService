using System;
using System.Threading.Tasks;
using HashtagAggregator.Core.Contracts.Interface.Cqrs.Command;
using HashtagAggregator.Shared.Common.Infrastructure;

namespace HashtagAggregatorTwitter.Contracts.Jobs
{
    public interface ITwitterBackgroundJob
    {
        Task<ICommandResult> Execute(HashTagWord hashTag, TimeSpan interval);
    }
}