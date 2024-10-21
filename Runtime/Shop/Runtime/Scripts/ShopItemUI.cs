using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Boshphelm.Shops
{
    public class ShopItemUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Button _selectButton;

        private ShopItem _shopItem;
        private System.Action<ShopItem> _onSelectCallback;

        public void SetupItem(ShopItem item, System.Action<ShopItem> onSelect)
        {
            _shopItem = item;
            _onSelectCallback = onSelect;

            _nameText.text = item.ItemDetail.DisplayName;
            _iconImage.sprite = item.ItemDetail.ShopUI.Icon; // Assuming ItemDetail has an icon

            _selectButton.onClick.AddListener(OnSelectClicked);
        }

        private void OnSelectClicked()
        {
            Debug.Log("dice select");
            _onSelectCallback?.Invoke(_shopItem);
        }
    }
}