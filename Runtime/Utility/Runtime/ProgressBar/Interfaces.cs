using UnityEngine;
 
namespace Boshphelm.Utility
{  
    public interface IProgressFormatter
    {
        string Format(float current, float target);
    }

    public interface IProgressAnimator
    {
        void Animate(float fromValue, float toValue);
        void AnimateColor(Color fromColor, Color toColor);
    }    
}
