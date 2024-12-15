using UnityEngine;
using UnityEngine.UI;
namespace Boshphelm.Utility
{
    public class ImageAlphaUpdater
    {
        private Image _image;

        private float _initialAlpha;
        private float _targetAlpha;

        public ImageAlphaUpdater(Image image, float initialAlpha, float targetAlpha)
        {
            _image = image;
            _initialAlpha = initialAlpha;
            _targetAlpha = targetAlpha;
        }

        public ImageAlphaUpdater(Image image, float targetAlpha)
        {
            _image = image;
            _initialAlpha = image.color.a;
            _targetAlpha = targetAlpha;
        }

        public void SetAlphaRate(float rate)
        {
            var color = _image.color;
            color.a = Mathf.Lerp(_initialAlpha, _targetAlpha, rate);
            //Debug.Log("COLOR ALPHA : " + color.a, _image.gameObject);
            _image.color = color;
        }
    }
}
