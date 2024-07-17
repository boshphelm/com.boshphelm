using UnityEngine;

namespace boshphelm.Utility
{
    public class TransformLooker : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private bool _lockY;

        private void Update()
        {
            if (_target == null) return;

            Vector3 targetVector = _target.position;
            if (_lockY) targetVector.y = transform.position.y;

            transform.LookAt(targetVector);
        }

        public void Setup(Transform target, bool lockY)
        {
            if (target == null) return;

            _target = target;
            _lockY = lockY;
        }
    }
}