using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OffScreenIndicator
{
    public class PointerIndicator : MonoBehaviour
    {
        [SerializeField] private Image _arrowImage;
        [SerializeField] private Image _indicatorImage;
        [SerializeField] private Image _targetImage;

        [SerializeField] private RectTransform[] _innerObjects;
        [SerializeField] private TextMeshProUGUI _distanceText;


        private Quaternion[] _innerObjectInitialRotations;

        private void Awake()
        {
            _innerObjectInitialRotations = new Quaternion[_innerObjects.Length];

            for (int i = 0; i < _innerObjects.Length; i++)
            {
                _innerObjectInitialRotations[i] = _innerObjects[i].rotation;
            }
        }

        public void SetImageColor(Color color)
        {
            _indicatorImage.color = color;
            _arrowImage.color = color;
        }

        public void SetTargetImageSprite(Sprite sprite)
        {
            _targetImage.sprite = sprite;
        }

        public void SetTargetImageSize(Vector2 sizeDelta)
        {
            _targetImage.rectTransform.sizeDelta = sizeDelta;
        }

        public void SetDistanceText(float value)
        {
            _distanceText.text = value >= 0 ? Mathf.Floor(value) + " m" : "";
        }

        public void SetTextRotation(Quaternion rotation)
        {
            _distanceText.rectTransform.rotation = rotation;
        }

        public void Activate(bool value)
        {
            gameObject.SetActive(value);
        }

        public void RefreshInnerObjectRotations()
        {
            for (int i = 0; i < _innerObjects.Length; i++)
            {
                _innerObjects[i].rotation = _innerObjectInitialRotations[i];
            }
        }
    }
}