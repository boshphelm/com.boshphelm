using Boshphelm.Currencies;
using Boshphelm.Wallets;
using UnityEngine;

namespace Boshphelm.AreaInteractionSystem
{
    public class AreaInteracter : MonoBehaviour, IAreaInteractible
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private InteractionTransferRole _interactionRole;
        [SerializeField] private Transform _lootTransferAnimationPosition;

        public InteractionTransferRole GetMyInteractionRole() => _interactionRole;
        private void Start()
        {
            _wallet = FindObjectOfType<Wallet>();
        }
        public bool AddPrice(Price price)
        {
            _wallet.AddCurrency(price.CurrencyDetails, price.Amount);
            return true;
        }
        public bool RemovePrice(Price price)
        {
            bool canPay = _wallet.HaveEnoughCurrency(price.CurrencyDetails, price.Amount);
            if (!canPay) return false;

            _wallet.RemoveCurrency(price.CurrencyDetails, price.Amount);
            return true;
        }
        public Transform GetInteractionLootTransferAnimationPosition() => _lootTransferAnimationPosition;
        public void OnEnterInteraction()
        {
        }
        public void OnExitInteraction()
        {
        }

    }
}
