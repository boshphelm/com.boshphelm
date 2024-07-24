using System;
using Boshphelm.StateMachines;
using Lofelt.NiceVibrations;
using UnityEngine;

namespace Boshphelm.AreaInteractionSystem
{
    public class InteractionAreaManager : StateMachine
    {
        [SerializeField] private AreaFillManager _fillManager;
        [SerializeField] public ProgressType progressType;

        [Header("Interaction Settings")]
        public float InteractionCompleteDuration;
        public float WaitingForInteractDuration;
        public float WaitingForReleaseDuration;

        [Header("Events")]
        public Action onInteractionCompleted;
        public Action onInteractionStart;
        public Action onInteractionStop;

        private BaseInteractionState _currentState;
        private IAreaInteractible _currentInteractor;

        private InteractingState _interactingState;


        private void Awake()
        {
            _interactingState = new InteractingState(this, InteractionCompleteDuration);

            SwitchState(new InactiveState(this));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IAreaInteractible interactor)) return;
            _currentInteractor = interactor;

            SwitchState(_interactingState);

        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out IAreaInteractible interactor)) return;
            if (interactor != _currentInteractor) return;

            onInteractionStop?.Invoke();


            SwitchState(new ReleasedState(this, _interactingState.InteractionTimer, WaitingForReleaseDuration));

        }
        public void SetFillAmount(float fillAmount)
        {
            _fillManager.UpdateFillAmount(fillAmount);
        }
        public void SetDurationProvider(IInteractionDurationProvider durationProvider)
        {
            InteractionCompleteDuration = durationProvider.GetInteractionCompleteDuration();
            WaitingForReleaseDuration = durationProvider.GetWaitingForReleaseDuration();
            WaitingForInteractDuration = durationProvider.GetWaitingForInteractDuration();

            _currentState.Reset();
        }




    }
    public enum ProgressType
    {
        CompleteOrCancel,
        Progressive
    }
}
