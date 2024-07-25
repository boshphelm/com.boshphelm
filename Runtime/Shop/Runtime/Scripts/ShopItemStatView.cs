using Boshphelm.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Boshphelm.Shops
{
    public class ShopItemStatView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _statNameText;
        [SerializeField] private Image _statIcon;
        [SerializeField] private TextMeshProUGUI _statValueText;

        public void RefreshView(ItemStat stat, int itemLevel)
        {
            _statNameText.text = stat?.itemStatSO.DisplayName;
            _statIcon.sprite = stat.itemStatSO.Icon;
            //Debug.Log("STAT : " + stat.DisplayName + ", ITEM LEVEL : " + itemLevel);

            float statValue = stat.Values[itemLevel];
            float diff = statValue % 1;
            _statValueText.text = diff == 0 ? ((int)statValue).ToString() : statValue.ToString("0.00");
        }
    }
}