using UnityEngine; 
using DG.Tweening;
using System;

namespace Boshphelm.ProgressBar
{
    public class GradientHealthBar : MonoBehaviour
    {
        [SerializeField] private BaseProgressBar _baseProgressBar;
        [Header("Gradient Settings")]
        [SerializeField] private Color fullColor = Color.yellow;
        [SerializeField] private Color midColor = new Color(1f, 0.5f, 0f); // Orange
        [SerializeField] private Color lowColor = Color.red;
        
        [SerializeField] private float midThreshold = 0.5f;
        [SerializeField] private float lowThreshold = 0.25f;
        public Action<float> onValueChanged;

        private void OnEnable() 
        {
            _baseProgressBar.onValueChanged += UpdateHealthColor;
        }

        private void OnDisable() 
        {
            _baseProgressBar.onValueChanged -= UpdateHealthColor;
        }
        
        private void UpdateHealthColor(float percentage)
        {
            Color targetColor;

            if (percentage > midThreshold)
            { 
                float t = (percentage - midThreshold) / (1f - midThreshold);
                targetColor = Color.Lerp(midColor, fullColor, t);
            }
            else if (percentage > lowThreshold)
            { 
                float t = (percentage - lowThreshold) / (midThreshold - lowThreshold);
                targetColor = Color.Lerp(lowColor, midColor, t);
            }
            else
            {
                targetColor = lowColor;
            }
 
            _baseProgressBar.SetColor(targetColor);
        }
    }
}
