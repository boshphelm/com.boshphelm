using UnityEngine;

namespace Boshphelm.AreaInteractionSystem
{
    public class WaitingForReleaseSubState : BaseInteractionSubState
    {
        private ReleasedState _parentState;
        private float _waitingForReleaseTimer;
        private float _waitingForReleaseDuration;

        public WaitingForReleaseSubState(InteractionAreaManager manager, ReleasedState parentState, float waitingForReleaseDuration) : base(manager)
        {
            this._parentState = parentState;
            this._waitingForReleaseDuration = waitingForReleaseDuration;
        }

        public override void Enter()
        {
            _waitingForReleaseTimer = _waitingForReleaseDuration;
        }

        public override void Tick()
        {
            _waitingForReleaseTimer -= Time.deltaTime;
            if (_waitingForReleaseTimer <= 0)
            {
                _parentState.SwitchToDecreasingFillState();
            }
        }

        public override void Exit() { }


    }
}
