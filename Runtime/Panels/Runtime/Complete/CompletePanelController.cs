using System.Collections;
using Boshphelm.Currencies;
using Boshphelm.Utility;
using Boshphelm.Wallets;
using TMPro;
using UnityEngine;

namespace Boshphelm.Panel
{
    public class CompletePanelController : PanelControllerABS
    {
        [SerializeField] private float _delayComplete;
        [Header("Currency Data")] [SerializeField] private CurrencyDataSO _currencyData;

        [Header("Scripts")] [SerializeField] private Wallet _wallet;

        // [SerializeField] private LevelFlowOrganizer _levelFlowOrganizer; TODO: Level System
        // [SerializeField] private LevelController _levelController;
        [SerializeField] private CompleteCommandHandler _completeCommandHandler;
        [Header("Components")] [SerializeField] private TextMeshProUGUI _levelText;

        [Header("Broadcasting")] [SerializeField] private VoidEventChannel _onSave;

        public override void Open()
        {
            // _levelText.text = "Level " + (_levelController.LevelIndex + 1).ToString();
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
            _onSave.RaiseEvent();
        }
    }
}
