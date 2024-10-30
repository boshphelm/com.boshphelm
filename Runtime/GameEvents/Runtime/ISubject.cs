namespace Boshphelm.GameEvents
{
    public interface ISubject
    {
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers(IGameEvent gameEvent);
    }
}
