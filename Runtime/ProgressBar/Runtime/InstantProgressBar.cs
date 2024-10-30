using UnityEngine;

namespace Boshphelm.ProgressBar
{
    public class InstantProgressBar : BaseProgressBar
    {
        public override void SetColor(Color targetColor)
        {
            fillImage.color = targetColor;        
        }

        protected override void UpdateVisuals()
        {
            fillImage.fillAmount = currentFillAmount;
        }
    }
}
 
