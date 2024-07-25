using UnityEngine;
using UnityEngine.UI;

namespace Boshphelm.Shops
{
    public class ShopItemUpgradeView : MonoBehaviour
    {
        [SerializeField] private Image[] _upgradeLevelImages;
        [SerializeField] private Sprite _reachedImg;
        [SerializeField] private Sprite _notReachedImg;

        public void RefreshView(int upgradeLevel)
        {
            //Debug.Log("UPGRADE LEVEL : " + upgradeLevel);
            for (int i = 0; i < _upgradeLevelImages.Length; i++)
            {
                bool reached = upgradeLevel > i;
                _upgradeLevelImages[i].sprite = reached ? _reachedImg : _notReachedImg;
            }
        }
    }
}