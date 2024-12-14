using UnityEngine;
using UnityEngine.UI;
namespace Boshphelm.Utility
{
    public class ImageColorUpdater
    {
        private readonly Image _image;

        private readonly Color _initialColor;
        private readonly Color _finalColor;

        public ImageColorUpdater(Image image, Color initialColor, Color finalColor)
        {
            _image = image;
            _initialColor = initialColor;
            _finalColor = finalColor;
        }

        public ImageColorUpdater(Image image, Color finalColor)
        {
            _image = image;
            _initialColor = _image.color;
            _finalColor = finalColor;
        }

        public void SetColorRate(float rate)
        {
            _image.color = Color.Lerp(_initialColor, _finalColor, rate);
        }
    }
}
