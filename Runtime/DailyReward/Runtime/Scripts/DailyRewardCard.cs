using UnityEngine;
using UnityEngine.UI;
using TMPro; 

namespace Boshphelm.DailyRewards
{
    public enum DailyRewardCardStatus
    {
        Available,
        Locked,
        Claimed
    }  

    public class DailyRewardCard : MonoBehaviour
    { 
        [Header("General")]
        [SerializeField] private Image _rewardIcon;
        [SerializeField] private TextMeshProUGUI _rewardAmountText;
        [SerializeField] private TextMeshProUGUI _dayText;
        [SerializeField] private Button _claimButton;

        [Header("Status Object Groups")]
        [SerializeField] private GameObject _availableStateObjects;
        [SerializeField] private GameObject _lockedStateObjects;
        [SerializeField] private GameObject _claimedStateObjects; 

        private DailyReward _rewardData;
        private System.Action<int> _onClaimCallback;
        private int _dayIndex;
        private DailyRewardCardStatus _currentStatus;

        private void Awake()
        {
            // Tüm state object'lerini başlangıçta kapalı tut
            SetAllStateObjectsActive(false);
        }

        public void Initialize(DailyReward rewardData, int dayIndex, System.Action<int> onClaimCallback)
        {
            _rewardData = rewardData;
            _dayIndex = dayIndex;
            _onClaimCallback = onClaimCallback;

            SetupUI();
            SetupButton();
        }

        private void SetupUI()
        {
            _dayText.text = $"Day {_dayIndex + 1}";
  
            _rewardIcon.sprite = _rewardData.Reward.CurrencyDetails.Icon;
            _rewardAmountText.text = _rewardData.Reward.Amount.ToString(); 
        }

        private void SetupButton()
        {
            _claimButton.onClick.AddListener(() => OnClaimButtonClicked());
        }

        public void UpdateState(bool canClaim, bool isClaimed)
        {
            // Önceki duruma ait objeleri kapat
            SetAllStateObjectsActive(false);

            // Yeni durumu belirle ve ayarla
            DailyRewardCardStatus newStatus;
            if (isClaimed) newStatus = DailyRewardCardStatus.Claimed; 
            else if (canClaim) newStatus = DailyRewardCardStatus.Available; 
            else newStatus = DailyRewardCardStatus.Locked; 

            // Durum değişti mi kontrol et
            if (_currentStatus != newStatus)
            {
                _currentStatus = newStatus;
                UpdateStatusObjects(newStatus);
            }

            // Button interactable durumunu güncelle
            _claimButton.interactable = canClaim;
        }

        private void UpdateStatusObjects(DailyRewardCardStatus status)
        {
            switch (status)
            {
                case DailyRewardCardStatus.Available:
                    _availableStateObjects.SetActive(true); 
                    break;

                case DailyRewardCardStatus.Locked:
                    _lockedStateObjects.SetActive(true); 
                    break;

                case DailyRewardCardStatus.Claimed:
                    _claimedStateObjects.SetActive(true); 
                    break;
            }
        }

        private void SetAllStateObjectsActive(bool active)
        {
            if (_availableStateObjects != null) _availableStateObjects.SetActive(active);
            if (_lockedStateObjects != null) _lockedStateObjects.SetActive(active);
            if (_claimedStateObjects != null) _claimedStateObjects.SetActive(active);
        }

        private void OnClaimButtonClicked()
        {
            _onClaimCallback?.Invoke(_dayIndex);
        }

        public void PlayClaimAnimation()
        {  

        } 
    }
}