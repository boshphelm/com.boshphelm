using Boshphelm.StateMachines;
using Lofelt.NiceVibrations;
using UnityEngine;

namespace Boshphelm.AreaInteractionSystem
{
    public class ReleasedState : BaseSubStateExecuterInteractionState
    {
        private WaitingForReleaseSubState _waitingForReleaseState;
        private DecreasingFillSubState _decreasingFillState;
        private SwitchToInactiveStateSubState _inactiveState;

        public ReleasedState(InteractionAreaManager manager, float interactionTimerStart, float waitingForReleaseDuration)
            : base(manager)
        {
            _waitingForReleaseState = new WaitingForReleaseSubState(manager, this, waitingForReleaseDuration);
            _decreasingFillState = new DecreasingFillSubState(manager, this, interactionTimerStart);
            _inactiveState = new SwitchToInactiveStateSubState(manager);

            SwitchSubState(_waitingForReleaseState);
        }

        public override void Enter()
        {
            currentSubState.Enter();

            HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
        }

        public override void Tick()
        {
            currentSubState.Tick();
        }

        public override void Exit()
        {
            currentSubState.Exit();
        }

        public override void Reset()
        {
        }
        public override string GetName() => "Released";

        public void SwitchSubState(BaseInteractionSubState newSubState)
        {
            currentSubState?.Exit();
            currentSubState = newSubState;
            currentSubState.Enter();
        }

        public void SwitchToDecreasingFillState()
        {
            SwitchSubState(_decreasingFillState);
        }

        public void SwitchToInactiveState()
        {
            SwitchSubState(_inactiveState);
        }
    }
}
