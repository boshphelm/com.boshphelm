using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using DG.Tweening;

namespace Boshphelm.Utility
{
    public class ProgressBar : MonoBehaviour
    {
        [Header("UI References")]
        [Tooltip("Ana progress bar'in Slider component'i")]
        [SerializeField] private Slider mainSlider;

        [Tooltip("Ghost bar için Slider component'i")]
        [SerializeField] private Slider ghostSlider;

        [Tooltip("Değerlerin gösterileceği TextMeshPro component'i")]
        [SerializeField] private TextMeshProUGUI displayText;

        [Header("Animation Settings")]
        [Tooltip("Ana bar'in değer değişimlerinin animate edilip edilmeyeceği")]
        [SerializeField] private bool useSmoothing = false;

        [Tooltip("Ana bar'in animasyon süresi (saniye)")]
        [Range(0.1f, 2f)]
        [SerializeField] private float mainAnimationDuration = 0.5f;

        [Tooltip("Animasyon eğrisi tipi")]
        [SerializeField] private Ease mainAnimationEase = Ease.OutCubic;

        [Header("Ghost Bar Settings")]
        [Tooltip("Ghost bar kullanilsin mi?")]
        [SerializeField] private bool useGhostBar = true;

        [Tooltip("Ghost bar'in ana bar'i takip etme gecikmesi (saniye)")]
        [Range(0.1f, 2f)]
        [SerializeField] private float ghostDelay = 0.5f;

        [Tooltip("Ghost bar'in hareket süresi (saniye)")]
        [Range(0.1f, 2f)]
        [SerializeField] private float ghostDuration = 1f;

        [Tooltip("Ghost bar'in hareket eğrisi")]
        [SerializeField] private Ease ghostEase = Ease.InOutCubic;

        [Tooltip("Ghost bar'in rengi")]
        [SerializeField] private Color ghostColor = new Color(1f, 1f, 1f, 0.5f);

        [Header("Format Settings")]
        [SerializeField] private FormatSettings formatSettings = new FormatSettings();

        private float currentValue = 0f;
        private float maxValue = 100f;
        private Tweener mainTween;
        private Tweener ghostTween;
        private IProgressFormatter formatter;

        private void Awake()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Ana slider'i ayarla
            if (mainSlider != null)
            {
                mainSlider.minValue = 0f;
                mainSlider.maxValue = 1f;
            }

            // Ghost slider'i ayarla
            if (ghostSlider != null && useGhostBar)
            {
                ghostSlider.minValue = 0f;
                ghostSlider.maxValue = 1f;
                SetGhostColor(ghostColor);
            }

            formatter = new ProgressFormatter(formatSettings);
            UpdateProgress(currentValue, true);
        }

        public void SetValue(float value)
        { 
            currentValue = Mathf.Clamp(value, 0f, maxValue);
            UpdateProgress(currentValue);
        }

        public void SetMaxValue(float max)
        {
            maxValue = Mathf.Max(0f, max);
            currentValue = Mathf.Min(currentValue, maxValue);
            UpdateProgress(currentValue);
        }

        private void UpdateProgress(float value, bool immediate = false)
        {
            float normalizedProgress = maxValue > 0 ? value / maxValue : 0;

            // Ana bar'i güncelle
            if (mainSlider != null)
            {
                mainTween?.Kill();
                if (useSmoothing && !immediate)
                {
                    mainTween = mainSlider.DOValue(normalizedProgress, mainAnimationDuration)
                        .SetEase(mainAnimationEase);
                }
                else
                {
                    mainSlider.value = normalizedProgress;
                }
            }

            // Ghost bar'i güncelle
            if (ghostSlider != null && useGhostBar)
            {
                ghostTween?.Kill();
                if (immediate)
                {
                    ghostSlider.value = normalizedProgress;
                }
                else
                {
                    float currentGhostValue = ghostSlider.value;
                    // Eğer değer azaliyorsa ghost bar gecikmeli takip eder
                    if (normalizedProgress < currentGhostValue)
                    {
                        ghostTween = ghostSlider.DOValue(normalizedProgress, ghostDuration)
                            .SetDelay(ghostDelay)
                            .SetEase(ghostEase);
                    }
                    // Değer artiyorsa ghost bar hemen takip eder
                    else
                    {
                        ghostSlider.value = normalizedProgress;
                    }
                }
            }

            UpdateText(value);
        }

        private void UpdateText(float value)
        {
            if (displayText != null && formatter != null)
            {
                displayText.text = formatter.Format(value, maxValue);
            }
        }

        public void SetMainColor(Color color)
        {
            if (mainSlider?.fillRect != null)
            {
                var fillImage = mainSlider.fillRect.GetComponent<Image>();
                if (fillImage != null)
                {
                    if (useSmoothing)
                    {
                        fillImage.DOColor(color, mainAnimationDuration);
                    }
                    else
                    {
                        fillImage.color = color;
                    }
                }
            }
        }

        public void SetGhostColor(Color color)
        {
            if (ghostSlider?.fillRect != null)
            {
                var fillImage = ghostSlider.fillRect.GetComponent<Image>();
                if (fillImage != null)
                {
                    fillImage.color = color;
                }
            }
        }

        private void OnValidate()
        {
            // Inspector'da değerler değiştiğinde ghost bar rengini güncelle
            if (Application.isPlaying && ghostSlider != null && useGhostBar)
            {
                SetGhostColor(ghostColor);
            }
        }
    }
}