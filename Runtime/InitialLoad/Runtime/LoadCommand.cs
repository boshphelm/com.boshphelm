using Boshphelm.Commands;

namespace Boshphelm.InitialLoad
{
    public abstract class LoadCommand : Command
    {
        public abstract float PercentageComplete { get; }
    }
}