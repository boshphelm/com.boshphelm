using UnityEngine;

namespace Boshphelm.AreaInteractionSystem
{
    public interface IAreaInteractible
    {
        public InteractionTransferRole GetMyInteractionRole();
        Transform GetInteractionLootTransferAnimationPosition();

    }
    public enum InteractionTransferRole
    {
        Player, Other

    }
}
