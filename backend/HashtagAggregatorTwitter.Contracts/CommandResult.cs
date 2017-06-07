using System;
using HashtagAggregator.Core.Contracts.Interface.Cqrs.Command;

namespace HashtagAggregatorTwitter.Models
{
    public class CommandResult: ICommandResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
