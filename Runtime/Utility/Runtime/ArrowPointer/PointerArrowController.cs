using UnityEngine;

namespace Boshphelm.Utility
{
    public class PointerArrowController : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _timeToReachTarget = 3f;
        [SerializeField] private float _movementSpeed = 1f;
        [SerializeField] private float _distanceToReach = 5f;

        private bool _refreshed;
        private float _timer;

        public System.Action<PointerArrowController> onDestroying;

        private void Update()
        {
            if (_target == null)
            {
                if (!_refreshed) Refresh();
                return;
            }

            _timer += Time.deltaTime / _timeToReachTarget;

            Vector3 difference = _target.position - transform.position;

            float currentSqrDistance = Vector3.SqrMagnitude(difference);
            if (currentSqrDistance <= _distanceToReach)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 dir = difference.normalized;
            transform.position += dir * Time.deltaTime * _movementSpeed;
            transform.LookAt(_target);

            if (_timer >= 1f) Destroy(gameObject);
        }

        private void Refresh()
        {
            _timer = 0f;
            _target = null;
            _refreshed = true;
        }

        public void Setup(Transform target, float movementSpeed)
        {
            _target = target;
            _movementSpeed = movementSpeed;
        }

        public void ClearTarget()
        {
            Refresh();
        }

        private void OnDestroy()
        {
            onDestroying?.Invoke(this);
        }
    }
}