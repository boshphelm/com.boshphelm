using UnityEngine;

namespace OffScreenIndicator
{
    [DefaultExecutionOrder(0)]
    public class IndicatorTarget : MonoBehaviour
    {
        [SerializeField] private Sprite _targetSprite;
        [SerializeField] private Vector2 _targetSize;
        [SerializeField] private Color _targetPointerColor = Color.red;
        [SerializeField] private bool _needPointerIndicator = true;
        [SerializeField] private bool _needDistanceText = true;

        [HideInInspector] public PointerIndicator indicator;

        public Color TargetPointerColor => _targetPointerColor;
        public Sprite TargetSprite => _targetSprite;
        public bool NeedPointerIndicator => _needPointerIndicator;
        public bool NeedDistanceText => _needDistanceText;
        public Vector2 TargetSize => _targetSize;

        private void OnEnable()
        {
            OffScreenIndicator.ChangeTargetState(this, true);
        }

        private void OnDisable()
        {
            OffScreenIndicator.ChangeTargetState(this, false);
        }


        public float GetDistanceFromCamera(Vector3 cameraPosition)
        {
            return Vector3.Distance(cameraPosition, transform.position);
        }
    }
}
