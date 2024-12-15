using Boshphelm.NavigationBars;
using Boshphelm.Pages;
using Boshphelm.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Boshphelm.PageNavigation
{
    public class PageNavigationManager : MonoBehaviour
    {
        [Title("Essentials", TitleAlignment = TitleAlignments.Centered)]
        [SerializeField] private PageManager _pageManager;
        [SerializeField] private NavigationBar _navigationBar;

        [PropertySpace(10f)]
        [Title("Ripple", TitleAlignment = TitleAlignments.Centered)]
        [SerializeField] private bool _useRipple;

        [ShowIf("_useRipple")]
        [SerializeField] private ImageRipple _imageRipple;

        private void Start()
        {
            _navigationBar.OnSelectedButtonChanged += OnPageChanged;
            _pageManager.NavigateTo(_navigationBar.SelectedButtonIndex);
        }

        private void OnPageChanged(NavigationBarButton navigationBarButton, int pageIndex)
        {
            _pageManager.NavigateTo(pageIndex);

            if (_useRipple) _imageRipple.Trigger();
        }

        private void OnDestroy()
        {
            _navigationBar.OnSelectedButtonChanged -= OnPageChanged;
        }
    }
}
