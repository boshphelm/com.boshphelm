using UnityEngine;

namespace Boshphelm.Utility
{
    public class TargetMark : MonoBehaviour
    {
        [SerializeField] private Transform _markTransform;

        private GameObject _markGO;
        private ITarget _target;
        private Transform _targetTransform;

        private void Awake()
        {
            _markGO = _markTransform.gameObject;
        }

        public void SetTarget(ITarget target)
        {
            if (_target == target) return;

            _target = target;
            _targetTransform = _target.FeetPoint;
            _markGO.SetActive(true);
        }

        private void Update()
        {
            if (_target == null) return;
            if (_targetTransform == null)
            {
                Refresh();
                return;
            }

            _markTransform.position = _targetTransform.position;
            // TODO: Rotate it ?
        }

        public void Refresh()
        {
            _target = null;
            _targetTransform = null;
            _markGO.SetActive(false);
        }
    }
}