using System;

namespace HashtagAggregatorTwitter.Contracts.Interface
{
    public interface IFreezeImmutable
    {
        bool Freezed { get; }

        void Freeze();
    }
}
