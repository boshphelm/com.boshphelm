using System.Collections;
using UnityEngine;

namespace Boshphelm.Panel
{
    public class CompletePanelController : PanelBase
    {
        [SerializeField] private float _delayComplete;
        [SerializeField] private CompleteCommandHandler _completeCommandHandler;

        public override void Open()
        {
            StartCoroutine(DelayOpen());
        }

        private IEnumerator DelayOpen()
        {
            yield return new WaitForSeconds(_delayComplete);
            base.Open();
            _completeCommandHandler.ExecuteCommands();
        }

        public void ClickNextButton()
        {
            // Price levelTotalLoot = _levelFlowOrganizer.GetLevelTotalLoot(_currencyDetails);
            // _wallet.Add(levelTotalLoot);
            //_onSave.RaiseEvent();
        }
    }
}
