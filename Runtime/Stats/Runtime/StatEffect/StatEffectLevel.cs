using System.Collections.Generic;

namespace Boshphelm.Stats
{
    [System.Serializable]
    public class StatEffectLevel<T> where T : StatType
    {
        public int Level;
        public List<StatEffect<T>> StatEffects;
    }
}