using System;

namespace Boshphelm.Utility
{
    public interface ISnap
    {
        int SnappedIndex { get; }
        event Action<int> OnSnappedIndexUpdated;
    }
}