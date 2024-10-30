namespace Boshphelm.GameEvents
{
    public interface IObserver
    {
        void OnNotify(IGameEvent gameEvent);
    }
}
