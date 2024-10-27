using UnityEngine;

namespace Boshphelm.Panel
{
    public abstract class PanelBase : MonoBehaviour, IPanel
    {
        [SerializeField] protected GameObject panelObject; 

        protected bool isOpen;
        public bool IsOpen => isOpen; 

        public virtual void Open()
        {
            panelObject.SetActive(true);
            isOpen = true;
        }

        public virtual void Close()
        {
            // TODO: Trigger Close Animation ??
            OnCloseAnimationComplete();
        }

        protected virtual void OnCloseAnimationComplete()
        {
            panelObject.SetActive(false);
            isOpen = false;
        }
    }
}
