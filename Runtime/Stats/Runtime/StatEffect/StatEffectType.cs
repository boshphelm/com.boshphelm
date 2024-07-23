using UnityEngine;

namespace Boshphelm.Stats
{
    [CreateAssetMenu(fileName = "StatEffectType", menuName = "Boshphelm/Stat/Effect/StatEffectType", order = 0)]
    public class StatEffectType : ScriptableObject
    {
        public Sprite Image;
        public Sprite TextImage;
    }
}