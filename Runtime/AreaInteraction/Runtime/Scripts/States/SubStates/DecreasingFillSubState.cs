using UnityEngine;

namespace Boshphelm.AreaInteractionSystem
{
    public class DecreasingFillSubState : BaseInteractionSubState
    {
        private ReleasedState _parentState;
        private float _interactionTimerStart;

        public DecreasingFillSubState(InteractionAreaManager manager, ReleasedState parentState, float interactionTimerStart)
            : base(manager)
        {
            this._parentState = parentState;
            this._interactionTimerStart = interactionTimerStart;
        }

        public override void Enter() { }

        public override void Tick()
        {
            _interactionTimerStart -= Time.deltaTime;
            manager.SetFillAmount(_interactionTimerStart / manager.InteractionCompleteDuration);

            if (_interactionTimerStart <= 0)
            {
                _parentState.SwitchToInactiveState();
            }
        }

        public override void Exit() { }


    }
}
