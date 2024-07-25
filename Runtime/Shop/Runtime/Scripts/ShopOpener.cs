
using Boshphelm.Buildings;
using Boshphelm.Shops;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Buildings
{
    public abstract class ShopOpener : MonoBehaviour
    {
        protected virtual void OpenShop()
        {
            EventBus<ShopOpenEvent>.Raise(new ShopOpenEvent());
        }
    }
}

public class ShopInteractionOpener : ShopOpener//, IInteractionDurationProvider
{ 
/*     [SerializeField] private FillAndCompleteOnceArea _fillAndCompleteOnceArea;

    [Header("Interaction Settings")]
    [SerializeField] private float _interactionCompleteDuration;
    [SerializeField] private float _waitingForReleaseDuration;

    private float _waitingForInteractDuration;

    public FillAndCompleteOnceArea FillAndCompleteOnceArea => _fillAndCompleteOnceArea;

    private void Start()
    {
        _fillAndCompleteOnceArea.SetDurationProvider(this);
    }
    private void OnEnable()
    {
        _fillAndCompleteOnceArea.onInteractionCompleted += OpenShop;
    }
    private void OnDisable()
    {
        _fillAndCompleteOnceArea.onInteractionCompleted -= OpenShop;
    }        
    [SerializeField] private float _fillResetSpeed;

    public float GetInteractionCompleteDuration() => _interactionCompleteDuration;


    public float GetWaitingForReleaseDuration() => _waitingForReleaseDuration;


    public float GetFillResetSpeed() => _fillResetSpeed;


    public float GetWaitingForInteractDuration() => _waitingForInteractDuration; */

}