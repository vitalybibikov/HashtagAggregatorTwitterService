using System.Threading.Tasks;
using HashtagAggregator.Core.Contracts.Interface.Cqrs.Command;

namespace HashtagAggregatorTwitter.Contracts.Interface.Jobs
{
    public interface IJobManager
    {
        ICommandResult AddJob(IJob job);

        ICommandResult DeleteJob(IJob job);

        ICommandResult ReconfigureJob(IJob job);

        Task<ICommandResult> StartNow(IJob job);
    }
}