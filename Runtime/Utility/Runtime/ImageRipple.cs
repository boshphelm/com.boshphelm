using UnityEngine;
using UnityEngine.UI;

namespace Boshphelm.Utility
{
    [RequireComponent(typeof(Image))]
    public class ImageRipple : MonoBehaviour
    {
        public bool unscaledTime = false;
        public float speed;
        public float maxSize;
        public Color startColor;
        public Color transitionColor;
        private Image _colorImg;

        private float _timer;

        private void Awake()
        {
            _colorImg = GetComponent<Image>();
            _colorImg.raycastTarget = false;
            _colorImg.color = new Color(startColor.r, startColor.g, startColor.b, startColor.a);
        }

        public void Trigger()
        {
            //Debug.Log("TRIGGERING");
            gameObject.SetActive(true);
            _timer = 0f;
            _colorImg.color = new Color(startColor.r, startColor.g, startColor.b, startColor.a);
        }

        private void Update()
        {
            if (unscaledTime)
            {
                _timer += Time.unscaledDeltaTime * speed;

                //transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(maxSize, maxSize, maxSize), _timer);
                _colorImg.color = Color.Lerp(_colorImg.color, new Color(transitionColor.r, transitionColor.g, transitionColor.b, transitionColor.a), _timer);

                if (_timer >= 1f)
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                _timer += Time.deltaTime * speed;

                //transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(maxSize, maxSize, maxSize), _timer);
                _colorImg.color = Color.Lerp(_colorImg.color, new Color(transitionColor.r, transitionColor.g, transitionColor.b, transitionColor.a), _timer);

                if (_timer >= 1f)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
