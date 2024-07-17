using UnityEngine;

namespace boshphelm.Utility
{
    public class TransformFollower : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3 _offset = new Vector3(0f, 0f, 0f);

        private bool _active;

        public void Setup(Transform transformToFollow)
        {
            _transform = transformToFollow;
        }

        public void Activate()
        {
            _active = true;
        }

        public void Deactivate()
        {
            _active = false;
        }

        private void LateUpdate()
        {
            if (!_active) return;


            transform.position = _transform.position + _offset;
        }
    }
}