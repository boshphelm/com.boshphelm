using System;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.NavigationBar
{
    public class NavigationBar : MonoBehaviour
    {
        [SerializeField] private NavigationBarButton[] _buttons;
        [SerializeField] private int _initialSelectedButtonIndex = 0;
        [SerializeField] private ImageRipple _imageRipple;

        private NavigationBarButton _currentButton;

        public readonly Action<NavigationBarButton, int> OnSelectedButtonChanged = (_, _) => { };

        private NavigationBarDynamicButtonAnchorCalculator _dynamicButtonAnchorCalculator;

        private void Start()
        {
            _dynamicButtonAnchorCalculator = new NavigationBarDynamicButtonAnchorCalculator(_buttons);

            InitializeButtons();
        }

        private void InitializeButtons()
        {
            var selectedButton = _buttons[Mathf.Clamp(_initialSelectedButtonIndex, 0, _buttons.Length - 1)];
            _dynamicButtonAnchorCalculator.UpdateButtonAnchors(selectedButton);

            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].Initialize(i);
                _buttons[i].OnButtonClicked += HandleNavigationButtonClick;
                if (selectedButton != _buttons[i]) _buttons[i].SetNotPressedDirectly();
            }

            SetSelectedButton(_initialSelectedButtonIndex, true);
        }
        private void HandleNavigationButtonClick(NavigationBarButton navigationBarButton, int pageIndex)
        {
            SetSelectedButton(navigationBarButton);
        }

        private void SetSelectedButton(int buttonIndex, bool setDirectly = false)
        {
            _initialSelectedButtonIndex = Mathf.Clamp(buttonIndex, 0, _buttons.Length - 1);

            SetSelectedButton(_buttons[buttonIndex], setDirectly);
        }

        private void SetSelectedButton(NavigationBarButton selectedNavigationBarButton, bool setDirectly = false)
        {
            if (_currentButton == selectedNavigationBarButton) return;

            _dynamicButtonAnchorCalculator.UpdateButtonAnchors(selectedNavigationBarButton);

            for (int i = 0; i < _buttons.Length; i++)
            {
                if (_buttons[i] == selectedNavigationBarButton) continue;

                if (setDirectly)
                {
                    _buttons[i].SetNotPressedDirectly();
                }
                else
                {
                    _buttons[i].SetNotPressed();
                }
            }

            _currentButton = selectedNavigationBarButton;
            if (setDirectly)
            {
                _currentButton.SetPressedDirectly();
            }
            else
            {
                _currentButton.SetPressed();
            }

            OnSelectedButtonChanged.Invoke(_currentButton, _currentButton.PageIndex);

            if (!setDirectly) _imageRipple.Trigger();
        }
    }
}
