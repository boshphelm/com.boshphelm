using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace boshphelm.Utility
{
    public class CharacterFootstepParticle : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _footstepParticleLeft;
        [SerializeField] private ParticleSystem _footstepParticleRight;
        private Transform _rightParticleParent;
        private Transform _leftParticleParent;
        private void Awake()
        {

            _rightParticleParent = _footstepParticleRight.transform.parent;
            _leftParticleParent = _footstepParticleLeft.transform.parent;
        }
        public void PlayRight()
        {
            _footstepParticleRight.transform.SetParent(_rightParticleParent);
            _footstepParticleRight.transform.localPosition = Vector3.zero;
            _footstepParticleRight.transform.SetParent(null);

            _footstepParticleRight.Play();
        }

        public void PlayLeft()
        {
            _footstepParticleLeft.transform.SetParent(_leftParticleParent);
            _footstepParticleLeft.transform.localPosition = Vector3.zero;
            _footstepParticleLeft.transform.SetParent(null);

            _footstepParticleLeft.Play();
        }
    }
}
