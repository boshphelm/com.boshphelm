using Boshphelm.ItemSlot;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Boshphelm.Sample.SimpleItemAndSlot
{
    public class SimpleItemSlotUIView : ItemSlotUIViewBase<SimpleItem, SimpleItemSlot>
    {
        [SerializeField] private TextMeshProUGUI _itemNameText;
        [SerializeField] private TextMeshProUGUI _itemQuantityText;
        [SerializeField] private Image _itemIcon;

        protected override void UpdateUI(ItemSlotBase<SimpleItem> slot, SimpleItem item)
        {
            if (item == null)
            {
                ClearUI();
                return;
            }

            _itemNameText.text = item.ItemDetail.DisplayName;
            _itemQuantityText.text = item.Quantity.ToString();

            _itemIcon.sprite = item.ItemDetail.Icon;
            _itemIcon.enabled = true;
        }

        protected override void ClearUI()
        {
            _itemNameText.text = string.Empty;
            _itemQuantityText.text = string.Empty;

            _itemIcon.sprite = null;
            _itemIcon.enabled = false;
        }
    }
}
