using UnityEngine;

namespace Boshphelm.Panel
{
    public class FailPanelController : PanelBase
    {
        [SerializeField] private FailCommandHandler _failCommandHandler;
        public override void Open()
        {
            base.Open();
            _failCommandHandler.ExecuteCommands();
        }
    }
}
