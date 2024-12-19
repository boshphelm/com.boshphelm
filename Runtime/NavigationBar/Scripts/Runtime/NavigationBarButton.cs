using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Boshphelm.NavigationBars
{
    [RequireComponent(typeof(NavigationButtonStateMachine))]
    public class NavigationBarButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private bool _growToLeft;
        public bool GrowToLeft => _growToLeft;


        private int _pageIndex;
        public int PageIndex => _pageIndex;
        private NavigationButtonStateMachine _stateMachine;

        public Action<NavigationBarButton, int> OnButtonClicked = (_, _) => { };

        private Vector2 _minAnchor;
        public Vector2 MinAnchor => _minAnchor;
        private Vector2 _maxAnchor;
        public Vector2 MaxAnchor => _maxAnchor;

        public void Initialize(int pageIndex)
        {
            _pageIndex = pageIndex;

            _stateMachine = GetComponent<NavigationButtonStateMachine>();
            _button.onClick.AddListener(ButtonClicked);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ButtonClicked);
        }

        private void ButtonClicked()
        {
            OnButtonClicked(this, _pageIndex);
        }

        public void SetPressed()
        {
            //Debug.Log("PRESSED : " + gameObject, gameObject);
            _stateMachine.Pressed(_minAnchor, _maxAnchor);
        }

        public void SetNotPressed()
        {
            //Debug.Log("NOT PRESSED : " + gameObject, gameObject);
            _stateMachine.NotPressed(_minAnchor, _maxAnchor);
        }

        public void SetNotPressedDirectly()
        {
            _stateMachine.SetNotPressedDirectly(_minAnchor, _maxAnchor);
        }

        public void SetPressedDirectly()
        {
            _stateMachine.SetPressedDirectly(_minAnchor, _maxAnchor);
        }

        public void UpdateAnchors(Vector2 minAnchor, Vector2 maxAnchor)
        {
            _minAnchor = minAnchor;
            _maxAnchor = maxAnchor;
        }
    }


}
