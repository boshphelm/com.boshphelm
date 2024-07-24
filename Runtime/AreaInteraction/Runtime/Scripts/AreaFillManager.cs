
using UnityEngine;
using UnityEngine.UI;

namespace Boshphelm.AreaInteractionSystem
{
    public class AreaFillManager : MonoBehaviour
    {
        [Header("Fill Settings")]
        public Image fillImage;
        public Color startColor = Color.gray;
        public Color endColor = Color.green;


        public void UpdateFillAmount(float fillAmount)
        {
            if (fillImage == null) return;

            fillImage.fillAmount = fillAmount;
            fillImage.color = Color.Lerp(startColor, endColor, fillAmount);
        }
    }
}
