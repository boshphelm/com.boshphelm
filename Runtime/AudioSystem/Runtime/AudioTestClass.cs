using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.AudioSystem
{
    public class AudioTestClass : MonoBehaviour
    {
        [SerializeField] private SoundData soundData;
        private SoundBuilder _soundBuilder;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SoundManager.Instance.CreateSoundBuilder().WithRandomPitch().WithPosition(transform.position).Play(soundData);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                for (int i = 0; i < 2; i++)
                    SoundManager.Instance.CreateSoundBuilder().WithRandomPitch().WithPosition(transform.position).Play(soundData);

            if (Input.GetKeyDown(KeyCode.Alpha3))
                for (int i = 0; i < 3; i++)
                    SoundManager.Instance.CreateSoundBuilder().WithRandomPitch().WithPosition(transform.position).Play(soundData);
        }
    }
}
