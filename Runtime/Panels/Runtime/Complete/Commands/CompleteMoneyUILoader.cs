using Boshphelm.Commands;
using Boshphelm.Currencies;
//using Boshphelm.Level; 
using TMPro;
using UnityEngine;

namespace Boshphelm.Panel
{
    public class CompleteMoneyUILoader : Command
    {
        [SerializeField] private GameObject _rewardGO;
        [SerializeField] private CurrencyData _currencyData;
        //[SerializeField] private LevelFlowOrganizer _levelFlowOrganizer;
        [SerializeField] private TextMeshProUGUI _moneyText; 

        public override void StartCommand()
        {
            _rewardGO.SetActive(true);
            _moneyText.text = "0";
            SetAmountText();
            CompleteCommand();
        }

        public override void ResetCommand()
        {
            _rewardGO.SetActive(false);
        }

        private void SetAmountText()
        {
           /*  Price levelTotalLoot = _levelFlowOrganizer.GetLevelTotalLoot(_currencyData);
            if (levelTotalLoot == null) return;

            _moneyText.ValueIncreaser(levelTotalLoot.Amount, 0, 1, Ease.OutQuad, true, null, null, 1);  */
        }
    }
}