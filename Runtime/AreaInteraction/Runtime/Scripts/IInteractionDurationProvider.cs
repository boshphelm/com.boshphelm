using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.AreaInteractionSystem
{
    public interface IInteractionDurationProvider
    {
        float GetInteractionCompleteDuration();
        float GetWaitingForReleaseDuration();
        float GetWaitingForInteractDuration();
    }
}
