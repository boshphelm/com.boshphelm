using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.GameEvents
{
    public interface IObserver
    {
        void OnNotify(IGameEvent gameEvent);
    }
}
