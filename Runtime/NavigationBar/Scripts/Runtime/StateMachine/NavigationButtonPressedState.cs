using Boshphelm.Utility;
using UnityEngine;
using UnityEngine.UI;
namespace Boshphelm.NavigationBar
{
    public class NavigationButtonPressedState : NavigationButtonState
    {
        private readonly AnchorUpdater _iconAnchorUpdater;
        private readonly AnchorUpdater _navigationBarButtonAnchorUpdater;
        private readonly ScaleUpdater _textScaleUpdater;
        private readonly ImageAlphaUpdater _backgroundAlphaUpdater;

        private float _timer = 0f;
        private bool _active;

        private const float _timeToChange = .1f;

        public System.Action OnComplete = () => { };

        public NavigationButtonPressedState(NavigationButtonStateMachine stateMachine, Transform textTransform, AnchorUpdater iconAnchorUpdater, RectTransform target, Image pressedBackgroundImage, Vector2 targetMinAnchor, Vector2 targetMaxAnchor, System.Action onComplete) : base(stateMachine)
        {
            _iconAnchorUpdater = iconAnchorUpdater;
            _textScaleUpdater = new ScaleUpdater(textTransform, Vector3.one);
            _navigationBarButtonAnchorUpdater = new AnchorUpdater(target, targetMinAnchor, targetMaxAnchor);
            _backgroundAlphaUpdater = new ImageAlphaUpdater(pressedBackgroundImage, 1f);

            OnComplete += onComplete;
        }
        public override void Enter()
        {
            _active = true;
            _timer = 0f;
            //Debug.Log("PRESSED BUTTON : " + stateMachine.gameObject, stateMachine.gameObject);
        }
        public override void Exit()
        {
            _active = false;
            //Debug.Log("PRESSED EXIT : " + stateMachine.gameObject, stateMachine.gameObject);
        }
        public override void Tick()
        {
            if (!_active) return;

            _timer += Time.deltaTime / _timeToChange;

            _navigationBarButtonAnchorUpdater.SetAnchorRate(_timer);
            _iconAnchorUpdater.SetAnchorRate(_timer);
            _textScaleUpdater.SetScaleRate(_timer);
            _backgroundAlphaUpdater.SetAlphaRate(_timer);

            //Debug.Log("TARGET ANCHOR MIN : " + _target.anchorMin + ", TARGET ANCHOR MAX : " + _target.anchorMax, stateMachine.gameObject);

            if (_timer >= 1f)
            {
                Complete();
            }
        }

        private void Complete()
        {
            _active = false;
            OnComplete.Invoke();
        }
    }
}
