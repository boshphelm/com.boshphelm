using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;

namespace Boshphelm.Tutorial
{
    public class TutorialFadeManager : MonoBehaviour
    {
        public FadeCanvasClickListener fadeCanvasClickListener;
        public TutorialHintManager tutorialHintManager;

        [SerializeField] private Transform _fadeCanvas;
        [SerializeField] private Image _fadeImage;
        [SerializeField] private Transform _fadeTransform;
        [SerializeField] private GameObject _guider;

        public void EnableFade()
        {
            SetFadeStatus(true);
        }

        public void DisableFade()
        {
            SetFadeStatus(false);
        }

        private void SetFadeStatus(bool status)
        {
            _fadeCanvas.gameObject.SetActive(status);
        }

        public void SetFadeColorAlpha(float alpha, float duration)
        {
            Debug.Log("SET ALPHA ");
            Color _color = _fadeImage.color;
            Color _newColor = new Color(_color.a, _color.g, _color.b, alpha);

            //_fadeImage.DOColor(_newColor, duration);
        }

        public void TransferObjectToFade(RectTransform obj)
        {
            obj.SetParent(_fadeTransform);
        }

        public void SetClickListenerStatus(bool status)
        {
            fadeCanvasClickListener.gameObject.SetActive(status);
        }

        public void AddClickListener(Action _action)
        {
            fadeCanvasClickListener.onPointerDown += _action;
        }

        public void RemoveClickListener(Action _action)
        {
            fadeCanvasClickListener.onPointerDown -= _action;
        }

        public void SetGuiderStatus(bool status)
        {
            _guider.SetActive(status);
        }
    }
}