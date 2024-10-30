using System;
using UnityEngine;
using UnityEngine.UI;

namespace Boshphelm.ProgressBar
{
    public abstract class BaseProgressBar : MonoBehaviour, IProgressBar
    {
        [Header("UI References")]
        [SerializeField] protected Image fillImage; 
        protected float currentFillAmount; 
        
        public Action<float> onValueChanged; 

        public virtual void SetProgress(float progress)
        {
            currentFillAmount = Mathf.Clamp01(progress);
            UpdateVisuals();
            onValueChanged?.Invoke(currentFillAmount);
        } 
        public abstract void SetColor(Color targetColor);

        protected abstract void UpdateVisuals();

    }
}
 
