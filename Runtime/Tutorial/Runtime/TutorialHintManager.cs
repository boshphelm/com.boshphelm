using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Boshphelm.Tutorial
{
    public class TutorialHintManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _hintText;
        private Tween _hintTextTween;

        public Action onHintTextAnimationStart;
        public Action onHintTextAnimationComplete;

        public void HintText(string hintText)
        { 
            _hintText.text = "";
            
            _hintTextTween = _hintText.DOText(hintText, 1f)
            .OnStart(() => onHintTextAnimationStart?.Invoke())
            .SetDelay(1)
            .SetUpdate(true)
            .SetEase(Ease.Linear)
            .OnComplete(() => onHintTextAnimationComplete?.Invoke()); 
        }

        public void SkipHintAnimation()
        {
            if (_hintTextTween == null || !_hintTextTween.IsPlaying()) return;

            _hintTextTween.Complete();
        }
        
        public void SetHintTextStatus(bool status)
        {
            _hintText.gameObject.SetActive(status);
        }

    }
}
