using System.Collections.Generic;
using UnityEngine;

namespace boshphelm.Utility
{
    public class PointerArrowSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _pointerArrowPrefab;
        [SerializeField] private float _pointerArrowSpawnTime;
        [SerializeField] private float _pointerArrowSpeed = 5f;

        private float _timer;

        private List<PointerArrowController> _pointerArrows = new List<PointerArrowController>();
        private bool _active;


        public void Setup(Transform target)
        {
            _target = target;
        }

        public void Activate()
        {
            _active = true;
        }

        private void Update()
        {
            if (!_active) return;

            _timer += Time.deltaTime;
            if (_timer >= _pointerArrowSpawnTime)
            {
                _timer = 0f;
                SpawnArrow();
            }
        }

        public void SpawnArrow()
        {
            GameObject newArrowGO = Instantiate(_pointerArrowPrefab, _parent);

            PointerArrowController arrowController = newArrowGO.GetComponent<PointerArrowController>();
            if (arrowController == null) return;

            SetAndRegisterPointerArrow(arrowController);
        }

        private void SetAndRegisterPointerArrow(PointerArrowController arrowController)
        {
            arrowController.onDestroying += OnArrowDestroy;
            arrowController.Setup(_target, _pointerArrowSpeed);
            _pointerArrows.Add(arrowController);
        }

        private void OnArrowDestroy(PointerArrowController pointerArrowController)
        {
            _pointerArrows.Remove(pointerArrowController);
        }

        public void OnDestroy()
        {
            Deactivate();
        }

        public void Deactivate()
        {
            for (int i = 0; i < _pointerArrows.Count; i++) Destroy(_pointerArrows[i].gameObject);
            _target = null;
            _active = false;
        }
    }
}