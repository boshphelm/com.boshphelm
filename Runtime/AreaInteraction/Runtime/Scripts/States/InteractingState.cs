using Lofelt.NiceVibrations;
using UnityEngine;

namespace Boshphelm.AreaInteractionSystem
{
    public class InteractingState : BaseInteractionState
    {
        private float _interactionCompleteDuration;

        public InteractingState(InteractionAreaManager manager, float _interactionCompleteDuration) : base(manager)
        {
            this._interactionCompleteDuration = _interactionCompleteDuration;
        }

        public override void Enter()
        {
            if (manager.progressType == ProgressType.CompleteOrCancel) InteractionTimer = 0;
            manager.onInteractionStart?.Invoke();

            HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
        }

        public override void Tick()
        {
            InteractionTimer += Time.deltaTime;

            if (InteractionTimer >= _interactionCompleteDuration)
            {
                manager.SwitchState(new CompletedState(manager));
            }

            manager.SetFillAmount(InteractionTimer / manager.InteractionCompleteDuration);
        }

        public override void Exit()
        {
        }

        public override void Reset()
        {
        }
    }
}
