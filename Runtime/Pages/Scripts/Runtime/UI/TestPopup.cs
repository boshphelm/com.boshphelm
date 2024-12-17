using UnityEngine;

namespace Boshphelm.Pages
{
    public class TestPopup : MonoBehaviour, IPopup
    {
        public void Open()
        {
            gameObject.SetActive(true);
        }
        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
