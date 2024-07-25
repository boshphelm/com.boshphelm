
using Boshphelm.Utility;
using Lofelt.NiceVibrations;

namespace Boshphelm.VibrationSystem
{
    public class VibrationManager : PersistentSingleton<VibrationManager>
    {
        public void PlayHaptic(HapticPatterns.PresetType presetType)
        {
            HapticPatterns.PlayPreset(presetType);
        }

        public void PlayConstant(float amplitude, float frequency, float duration)
        {
            HapticPatterns.PlayConstant(amplitude, frequency, duration);
        }
        public void StopHaptic()
        {
            HapticController.Stop();
        }

    }
}

