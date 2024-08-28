using UnityEngine;

namespace Boshphelm.Units
{
    [CreateAssetMenu(menuName = "Boshphelm/Unit/Spawn/UnitWaveInformationContainer")]
    public class UnitWaveInformationContainer : ScriptableObject
    {
        public UnitWaveInformation[] UnitWaveInformationList;
    }
}
