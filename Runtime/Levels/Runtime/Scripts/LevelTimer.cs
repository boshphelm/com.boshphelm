using Boshphelm.Utility;

namespace Boshphelm.Levels
{
    public class LevelTimer : ITimer
    {
        public float Time { get; private set; }

        public readonly System.Action<float> OnTick = _ => { };

        public void Tick(float deltaTime)
        {
            Time += deltaTime;
            OnTick.Invoke(Time);
        }

        public void Reset()
        {
            Time = 0;
        }
    }
}
