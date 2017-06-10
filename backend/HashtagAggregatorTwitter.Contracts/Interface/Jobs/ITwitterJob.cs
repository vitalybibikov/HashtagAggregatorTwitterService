using System.Threading.Tasks;
using HashtagAggregator.Core.Contracts.Interface.Cqrs.Command;

namespace HashtagAggregatorTwitter.Contracts.Interface.Jobs
{
    public interface ITwitterJob: IJob
    {
        Task<ICommandResult> Execute(TwitterJobTask task);
    }
}
