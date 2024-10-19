using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Boshphelm.Currencies
{
    public class FloatingCurrencyNotification : MonoBehaviour
    {
        [SerializeField] private Image _currencyIcon;
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private CanvasGroup _canvasGroup;

        public void SetNotification(Sprite icon, int amount)
        {
            _currencyIcon.sprite = icon;
            _amountText.text = $"+{amount}";
        }

        public void ResetPosition()
        {
            transform.localPosition = Vector3.zero;
            _canvasGroup.alpha = 1f;
        }

        public CanvasGroup CanvasGroup => _canvasGroup;
    }
}
