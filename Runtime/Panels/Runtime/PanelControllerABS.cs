using UnityEngine;

namespace Boshphelm.Panel
{
    public abstract class PanelControllerABS : MonoBehaviour
    {        
        [SerializeField] private GameObject _panelGO;
        public virtual void Open()
        { 
            _panelGO.SetActive(true);
        }
        public virtual void Close()
        {
            _panelGO.SetActive(false);
        }
    }
}
