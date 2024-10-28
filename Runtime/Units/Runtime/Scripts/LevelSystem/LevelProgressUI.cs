using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace Boshphelm.Units.Level.UI
{
    public class LevelProgressUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private LevelablePlayer _player;
        [SerializeField] private Image _fillImage;
        [SerializeField] private TextMeshProUGUI _levelText;
        
        [Header("Animation")]
        [SerializeField] private float _updateDuration = 0.5f;
        [SerializeField] private Ease _updateEase = Ease.OutCubic;
        
        private void Update()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (_player == null) return;

            // Smooth fill bar update
            _fillImage.DOFillAmount(_player.GetProgressToNextLevel(), _updateDuration)
                .SetEase(_updateEase);

            // Update level text
            _levelText.text = _player.GetLevelProgressText();
        }
    }
}
