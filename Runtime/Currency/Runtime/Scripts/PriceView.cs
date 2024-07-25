using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Boshphelm.Currencies
{
    public class PriceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Image _currencyIcon;

        public void SetPrice(Price price)
        {
            _priceText.text = price.Amount.ToString();
            _currencyIcon.sprite = price.CurrencyDetails.Icon;
        }
    }
}